using UnityEngine;

public class LineParametricDistanceToPoint : MonoBehaviour
{
	public float line_x1 = -3.0f;
	public float line_y1 = 3.0f;
	public float line_x2 = 3.0f;
	public float line_y2 = -3.0f;

	public float pointX = 2.0f;
	public float pointY = 1.0f;

	void Update()
	{
		Zenon.DrawRect(Zenon.GetCanvasWidth(), Zenon.GetCanvasHeight(), 10000, Color.white);
		Zenon.DrawCoordSystem(true, 15.0f, 10.0f, 1.0f, 0.05f, 10001);

		//

		Vector2 line_p1 = new Vector2(line_x1, line_y1);
		Vector2 line_p2 = new Vector2(line_x2, line_y2);
		Vector2 line_v = line_p2 - line_p1;
		line_v.Normalize();

		//

		Vector2 lineToPoint = new Vector2(pointX, pointY) - line_p1;
		float projP_t = Vector2.Dot(line_v, lineToPoint);
		Vector2 projP = line_p1 + line_v * projP_t;

		float distance = Vector2.Distance(new Vector2(pointX, pointY), projP);
		Debug.Log(distance);

		//

		Zenon.DrawSegment(line_x1, line_y1, line_x2, line_y2, 0.05f, 10002, Color.red);
		Zenon.DrawCircle(pointX, pointY, 0.1f, 10004, Color.red);
		Zenon.DrawCircle(line_p1.x, line_p1.y, 0.1f, 10004, Color.blue);
		Zenon.DrawCircle(projP.x, projP.y, 0.1f, 10004, Color.black);
		Zenon.DrawAxisDashed(line_p1.x, line_p1.y, pointX, pointY, 0.05f, 10003, Color.blue);
	}
}
