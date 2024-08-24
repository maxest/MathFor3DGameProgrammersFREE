using UnityEngine;

public class CircleImplicit : MonoBehaviour
{
	public float line_x1 = -3.0f;
	public float line_y1 = 3.0f;
	public float line_x2 = 3.0f;
	public float line_y2 = -3.0f;

	public float a = 0.0f;
	public float b = 0.0f;
	public float r = 1.0f;

	void Update()
	{
		Zenon.DrawRect(Zenon.GetCanvasWidth(), Zenon.GetCanvasHeight(), 10000, Color.white);
		Zenon.DrawCoordSystem(true, 15.0f, 10.0f, 1.0f, 0.05f, 10001);

		Zenon.DrawSegment(line_x1, line_y1, line_x2, line_y2, 0.05f, 10002, Color.red);
		Zenon.DrawCircleWireframe(a, b, r, 0.05f, 10003, Color.blue);

		Vector2 line_p1 = new Vector2(line_x1, line_y1);
		Vector2 line_p2 = new Vector2(line_x2, line_y2);
		Vector2 line_v = line_p2 - line_p1;

		// https://www.wolframalpha.com/input?i=%28x_0+%2B+X+*+t+-+a%29%5E2+%2B+%28y_0+%2B+Y+*+t+-+b%29%5E2+%3D+r%5E2%2C+find+t
		float x0 = line_p1.x;
		float y0 = line_p1.y;
		float X = line_v.x;
		float Y = line_v.y;
		float delta = 8.0f*b*X*(Y*(a - x0) + X*y0) - 4.0f*(Y*(a - x0) + X*y0)*(Y*(a - x0) + X*y0) - 4.0f*b*b*X*X + 4.0f*r*r*(X*X + Y*Y);
		float t1 = 1.0f / (X*X + Y*Y) * (-0.5f*Mathf.Sqrt(delta) + a*X + b*Y - x0*X - y0*Y);
		float t2 = 1.0f / (X*X + Y*Y) * (0.5f*Mathf.Sqrt(delta) + a*X + b*Y - x0*X - y0*Y);

		Vector2 p1 = line_p1 + line_v * t1;
		Vector2 p2 = line_p1 + line_v * t2;

		Zenon.DrawCircle(p1.x, p1.y, 0.1f, 10004, Zenon.ColorGreen075);
		Zenon.DrawCircle(p2.x, p2.y, 0.1f, 10004, Zenon.ColorGreen075);

		Zenon.DrawTextWithBackground("t1: " + t1 + ",    t2: " + t2, -Zenon.GetCanvasWidth() * 0.5f + 0.1f, Zenon.GetCanvasHeight() * 0.5f - 0.1f, 0.01f, Zenon.HoriAlignment.Left, Zenon.VertAlignment.Top, 10004, Color.black, Color.white);
	}
}
