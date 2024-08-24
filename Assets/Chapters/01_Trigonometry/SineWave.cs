using UnityEngine;

public class SineWave : MonoBehaviour
{
	[System.Serializable]
	public struct SineWaveDesc
	{
		public float amplitude;
		public float argScale;
		public float argOffset;
	}

	public bool drawSineWavesIndividually = true;
	public bool drawSineWavesSum = false;
	public SineWaveDesc[] sineWaves;

	public bool showScene = false;
	public GameObject lightGO = null;
	public float lightArg = 0.0f;
	public float lightArgSpeed = 0.0f;

	void Update()
	{
		int currentSineWaveIndex;

		float SineWaveFunction(float x)
		{
			float amplitude = sineWaves[currentSineWaveIndex].amplitude;
			float argScale = sineWaves[currentSineWaveIndex].argScale;
			float argOffset = sineWaves[currentSineWaveIndex].argOffset;

			return amplitude * Mathf.Sin(argScale * x + argOffset);
		}

		float SineWaveFunctionSum(float x)
		{
			float y = 0.0f;

			for (int i = 0; i < sineWaves.Length; i++)
			{
				currentSineWaveIndex = i;
				y += SineWaveFunction(x);
			}

			return y;
		}

		//

		if (!showScene)
		{
			Zenon.DrawRect(Zenon.GetCanvasWidth(), Zenon.GetCanvasHeight(), 10000, Color.white);
		}
		Zenon.DrawCoordSystem(showScene ? false : true, 15.0f, 10.0f, 1.0f, 0.05f, 10001);

		if (drawSineWavesIndividually)
		{
			for (int i = 0; i < sineWaves.Length; i++)
			{
				currentSineWaveIndex = i;
				Zenon.DrawFunction(SineWaveFunction, -10.0f, 10.0f, 0.1f, 5.0f, 0.05f, 10002, Color.red);
			}
		}

		if (drawSineWavesSum)
		{
			Zenon.DrawFunction(SineWaveFunctionSum, -10.0f, 10.0f, 0.1f, 5.0f, 0.05f, 10003, Color.blue);
		}

		void DrawAngle(string text, float x)
		{
			Zenon.DrawSegment(x, -0.2f, x, 0.2f, 0.1f, 10001, Color.black);
			Zenon.DrawText(text, x, -0.25f, 0.007f, Zenon.HoriAlignment.Center, Zenon.VertAlignment.Top, 10001, Color.black);
		}

		DrawAngle("45°", Mathf.PI / 4.0f);
		DrawAngle("90°", Mathf.PI / 2.0f);
		DrawAngle("180°", Mathf.PI);
		DrawAngle("360°", 2.0f * Mathf.PI);

		//

		if (showScene)
		{
			lightArg += Time.deltaTime * lightArgSpeed;

			Zenon.DrawSegmentDashed(lightArg, -5.0f, lightArg, 5.0f, 0.1f, 0.1f, 10004, Color.yellow);

			lightGO.GetComponent<Light>().intensity = SineWaveFunctionSum(lightArg);
		}
	}
}
