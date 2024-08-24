using UnityEngine;

public class VectorsLerp : MonoBehaviour
{
	public enum ViewMode
	{
		Axes,
		Points
	}

	public ViewMode viewMode = ViewMode.Axes;
	public Vector2 v1 = new Vector2(-3.0f, 2.0f);
	public Vector2 v2 = new Vector2(3.0f, 2.0f);
	public float t = 0.5f;

	void Update()
	{
		Vector2 v3 = Vector2.Lerp(v1, v2, t);

	//	v3 = v1 + (v2 - v1)*t;

	//	v3 = v1*(1.0f - t) + v2*t;

		//

		Zenon.CanvasWidth = 16.0f;
		Zenon.DrawRect(Zenon.GetCanvasWidth(), Zenon.GetCanvasHeight(), 10000, Color.white);
		Zenon.DrawCoordSystem(true, 15.0f, 10.0f, 1.0f, 0.05f, 10001);

		if (viewMode == ViewMode.Axes)
		{
			Zenon.DrawAxis(0.0f, 0.0f, v1.x, v1.y, 0.1f, 10002, Color.red);
			Zenon.DrawAxis(0.0f, 0.0f, v2.x, v2.y, 0.1f, 10003, Zenon.ColorGreen075);
			Zenon.DrawAxis(0.0f, 0.0f, v3.x, v3.y, 0.1f, 10004, Color.blue);
		}
		else if (viewMode == ViewMode.Points)
		{
			Zenon.DrawCircle(v1.x, v1.y, 0.1f, 10002, Color.red);
			Zenon.DrawCircle(v2.x, v2.y, 0.1f, 10003, Zenon.ColorGreen075);
			Zenon.DrawCircle(v3.x, v3.y, 0.1f, 10004, Color.blue);
		}
	}
}
