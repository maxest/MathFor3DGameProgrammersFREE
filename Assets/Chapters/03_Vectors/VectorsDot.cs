using UnityEngine;

public class VectorsDot : MonoBehaviour
{
	public Vector2 v1 = new Vector2(3.0f, 0.0f);
	public Vector2 v2 = new Vector2(0.0f, 2.0f);

	public bool normalized = false;

	void Update()
	{
		Vector2 v1_normalized = v1.normalized;
		Vector2 v2_normalized = v2.normalized;

		float dot_unnormalized = Vector2.Dot(v1, v2);
		float dot_normalized = Vector2.Dot(v1_normalized, v2_normalized);
		float theta = Mathf.Acos(dot_normalized) * Mathf.Rad2Deg;

		//

		Zenon.CanvasWidth = 16.0f;
		Zenon.DrawRect(Zenon.GetCanvasWidth(), Zenon.GetCanvasHeight(), 10000, Color.white);
		Zenon.DrawCoordSystem(true, 15.0f, 10.0f, 1.0f, 0.05f, 10001);

		if (!normalized)
		{
			Zenon.DrawAxis(0.0f, 0.0f, v1.x, v1.y, 0.1f, 10002, Color.red);
			Zenon.DrawAxis(0.0f, 0.0f, v2.x, v2.y, 0.1f, 10002, Zenon.ColorGreen075);
			Zenon.DrawTextWithBackground("dot product: " + dot_unnormalized, -Zenon.GetCanvasWidth() * 0.5f + 0.1f, Zenon.GetCanvasHeight() * 0.5f - 0.1f, 0.007f, Zenon.HoriAlignment.Left, Zenon.VertAlignment.Top, 10003, Color.black, Color.white);
		}
		else
		{
			Zenon.DrawAxis(0.0f, 0.0f, v1_normalized.x, v1_normalized.y, 0.1f, 10002, Color.red);
			Zenon.DrawAxis(0.0f, 0.0f, v2_normalized.x, v2_normalized.y, 0.1f, 10002, Zenon.ColorGreen075);
			Zenon.DrawTextWithBackground("dot product: " + dot_normalized, -Zenon.GetCanvasWidth() * 0.5f + 0.1f, Zenon.GetCanvasHeight() * 0.5f - 0.1f, 0.007f, Zenon.HoriAlignment.Left, Zenon.VertAlignment.Top, 10003, Color.black, Color.white);
		}
		Zenon.DrawTextWithBackground("theta: " + theta, -Zenon.GetCanvasWidth() * 0.5f + 0.1f, Zenon.GetCanvasHeight() * 0.5f - 1.0f, 0.007f, Zenon.HoriAlignment.Left, Zenon.VertAlignment.Top, 10003, Color.black, Color.white);
	}
}
