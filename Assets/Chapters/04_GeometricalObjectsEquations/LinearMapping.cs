using UnityEngine;

public class LinearMapping : MonoBehaviour
{
	public float interval1_begin = 0.0f, interval1_end = 1.0f;
	public float interval2_begin = -2.0f, interval2_end = 6.0f;

	public float x = 0.0f;

	void Update()
	{
		float a = (interval2_end - interval2_begin) / (interval1_end - interval1_begin);
		float b = interval2_begin - a * interval1_begin;

		float y = a*x + b;

		//

		Debug.Log(x + " in [" + interval1_begin + ", " + interval1_end + "]        " + y + " in [" + interval2_begin + ", " + interval2_end + "]");

		//

		Zenon.DrawRect(Zenon.GetCanvasWidth(), Zenon.GetCanvasHeight(), 10000, Color.white);

		Zenon.DrawSegment(interval1_begin, 1.0f, interval1_end, 1.0f, 0.1f, 10001, Color.red);
		Zenon.DrawSegment(interval2_begin, -1.0f, interval2_end, -1.0f, 0.1f, 10001, Color.blue);

		Zenon.DrawCircle(x, 1.0f, 0.1f, 10002, Color.black);
		Zenon.DrawCircle(y, -1.0f, 0.1f, 10002, Color.black);
	}
}
