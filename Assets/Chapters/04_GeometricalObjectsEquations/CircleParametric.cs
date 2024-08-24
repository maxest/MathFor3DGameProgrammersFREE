using UnityEngine;

public class CircleParametric : MonoBehaviour
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

		for (int i = 0; i < 64; i++)
		{
			float angle = 2.0f * Mathf.PI * (float)i / 64.0f;

			float x = a + r * Mathf.Cos(angle);
			float y = b + r * Mathf.Sin(angle);

			Zenon.DrawCircle(x, y, 0.05f, 10003, Color.blue);
		}

		Vector2 line_p1 = new Vector2(line_x1, line_y1);
		Vector2 line_p2 = new Vector2(line_x2, line_y2);
		Vector2 line_v = line_p2 - line_p1;

		// https://www.wolframalpha.com/input?i=x_0+%2B+X+*+t+%3D+a+%2B+r+*+cos%28T%29%2C+y_0+%2B+Y+*+t+%3D+b+%2B+r+*+sin%28T%29%2C+find+t+and+T
		float x0 = line_p1.x;
		float y0 = line_p1.y;
		float X = line_v.x;
		float Y = line_v.y;
		float delta = Zenon.Sqr(-2.0f*a*X - 2.0f*b*Y + 2.0f*x0*X + 2.0f*y0*Y) - 4.0f*(X*X + Y*Y) * (a*a - 2.0f*a*x0 + b*b - 2.0f*b*y0 - r*r + x0*x0 + y0*y0);
		float t1 = 1.0f / (X*X + Y*Y) * (0.5f*Mathf.Sqrt(delta) + a*X + b*Y - x0*X - y0*Y);
		float t2 = 1.0f / (X*X + Y*Y) * (-0.5f*Mathf.Sqrt(delta) + a*X + b*Y - x0*X - y0*Y);

		Vector2 p1 = line_p1 + line_v * t1;
		Vector2 p2 = line_p1 + line_v * t2;

		Zenon.DrawCircle(p1.x, p1.y, 0.1f, 10004, Zenon.ColorGreen075);
		Zenon.DrawCircle(p2.x, p2.y, 0.1f, 10004, Zenon.ColorGreen075);

		Zenon.DrawTextWithBackground("t1: " + t1 + ",    t2: " + t2, -Zenon.GetCanvasWidth() * 0.5f + 0.1f, Zenon.GetCanvasHeight() * 0.5f - 0.1f, 0.01f, Zenon.HoriAlignment.Left, Zenon.VertAlignment.Top, 10004, Color.black, Color.white);
	}
}
