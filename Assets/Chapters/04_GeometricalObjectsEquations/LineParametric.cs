using UnityEngine;

public class LineParametric : MonoBehaviour
{
	public float line1_x1 = -3.0f;
	public float line1_y1 = 3.0f;
	public float line1_x2 = 3.0f;
	public float line1_y2 = -3.0f;
	public float t1 = 0.5f;

	public float line2_x1 = -3.0f;
	public float line2_y1 = -3.0f;
	public float line2_x2 = 3.0f;
	public float line2_y2 = 3.0f;
	public float t2 = 0.5f;

	public bool findIntersection = false;

	void Update()
	{
		Zenon.DrawRect(Zenon.GetCanvasWidth(), Zenon.GetCanvasHeight(), 10000, Color.white);
		Zenon.DrawCoordSystem(true, 15.0f, 10.0f, 1.0f, 0.05f, 10001);
		
		//

		Vector2 line1_p1 = new Vector2(line1_x1, line1_y1);
		Vector2 line1_p2 = new Vector2(line1_x2, line1_y2);
		Vector2 line1_v = line1_p2 - line1_p1;
	//	line1_v.Normalize();

		Vector2 line2_p1 = new Vector2(line2_x1, line2_y1);
		Vector2 line2_p2 = new Vector2(line2_x2, line2_y2);
		Vector2 line2_v = line2_p2 - line2_p1;
	//	line2_v.Normalize();

		//

		if (findIntersection)
		{
			// https://www.wolframalpha.com/input?i=a+%2B+b+*+%28t_1%29+%3D+c+%2B+d+*+%28t_2%29%2C+e+%2B+f+*+%28t_1%29+%3D+g++%2B+h+*+%28t_2%29%2C+find+t_1+and+t_2
			float a = line1_p1.x;
			float b = line1_v.x;
			float c = line2_p1.x;
			float d = line2_v.x;
			float e = line1_p1.y;
			float f = line1_v.y;
			float g = line2_p1.y;
			float h = line2_v.y;

			t1 = (h * (a - c) + d * (g - e)) / (d * f - b * h);
			t2 = (f * (a - c) + b * (g - e)) / (d * f - b * h);
		}

		//

		Vector2 line1_p = line1_p1 + line1_v * t1;
		Vector2 line2_p = line2_p1 + line2_v * t2;

		//

		Zenon.DrawSegment(line1_x1, line1_y1, line1_x2, line1_y2, 0.05f, 10002, Color.red);
		Zenon.DrawSegment(line2_x1, line2_y1, line2_x2, line2_y2, 0.05f, 10003, Color.blue);
		Zenon.DrawCircle(line1_p.x, line1_p.y, 0.1f, 10004, Color.black);
		Zenon.DrawCircle(line2_p.x, line2_p.y, 0.1f, 10004, Color.black);
	}
}
