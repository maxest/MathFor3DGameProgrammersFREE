using UnityEngine;

public class CirclePointsGeneration : MonoBehaviour
{
	public enum GenerationType
	{
		StandardCircle,
		StandardDisk,
		GoldenSpiralDisk
	}

	public GenerationType generationType = GenerationType.StandardCircle;
	public float radius = 3.0f;
	public float angleOffset = 0.0f;

	void Update()
	{
		Zenon.DrawRect(Zenon.GetCanvasWidth(), Zenon.GetCanvasHeight(), 10000, Color.white);
		Zenon.DrawCoordSystem(true, 15.0f, 10.0f, 1.0f, 0.05f, 10001);

		if (generationType == GenerationType.StandardCircle)
		{
			StandardCirclePointsGeneration(radius, angleOffset);
		}
		else if (generationType == GenerationType.StandardDisk)
		{
			StandardDiskPointsGeneration(radius, angleOffset);
		}
		else if (generationType == GenerationType.GoldenSpiralDisk)
		{
			GoldenSpiralDiskPointsGeneration(radius, angleOffset, 64);
		}
	}

	private void StandardCirclePointsGeneration(float radius, float angleOffset)
	{
		for (float angle = 0.0f; angle < 2.0f * Mathf.PI; angle += 0.25f)
		{
			PolarToCartesian(out float x, out float y, radius, angle + angleOffset);
			Zenon.DrawCircle(x, y, 0.1f, 10002, Color.red);
		}
	}

	private void StandardDiskPointsGeneration(float radius, float angleOffset)
	{
		for (float angle = 0.0f; angle < 2.0f * Mathf.PI; angle += 0.5f)
		{
			for (float r = radius; r >= 0.0f; r -= 0.5f)
			{
				PolarToCartesian(out float x, out float y, r, angle + angleOffset);
				Zenon.DrawCircle(x, y, 0.1f, 10002, Color.red);
			}
		}
	}

	// https://stackoverflow.com/questions/9600801/evenly-distributing-n-points-on-a-sphere
	// https://www.shadertoy.com/view/XtXXDN
	private void GoldenSpiralDiskPointsGeneration(float radius, float angleOffset, float n)
	{
		for (int i = 0; i < n; i++)
		{
			float idx = (float)i + 0.5f;

			float r = radius * Mathf.Sqrt(idx / n);
			float angle = Mathf.PI * (1.0f + Mathf.Sqrt(5.0f)) * idx;

			PolarToCartesian(out float x, out float y, r, angle + angleOffset);
			Zenon.DrawCircle(x, y, 0.1f, 10002, Color.red);
		}
	}

	private void PolarToCartesian(out float x, out float y, float radius, float angle)
	{
		x = radius * Mathf.Cos(angle);
		y = radius * Mathf.Sin(angle);
	}
}
