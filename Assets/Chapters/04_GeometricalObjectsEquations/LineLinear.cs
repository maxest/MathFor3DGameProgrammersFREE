using UnityEngine;

public class LineLinear : MonoBehaviour
{
	public float line1_x1 = -3.0f;
	public float line1_y1 = 3.0f;
	public float line1_x2 = 3.0f;
	public float line1_y2 = -3.0f;

	public float line2_x1 = -3.0f;
	public float line2_y1 = -3.0f;
	public float line2_x2 = 3.0f;
	public float line2_y2 = 3.0f;

	public float a1, b1, a2, b2;

	void Update()
	{
		Zenon.DrawRect(Zenon.GetCanvasWidth(), Zenon.GetCanvasHeight(), 10000, Color.white);
		Zenon.DrawCoordSystem(true, 15.0f, 10.0f, 1.0f, 0.05f, 10001);

		//

		a1 = (line1_y2 - line1_y1) / (line1_x2 - line1_x1);
		b1 = line1_y1 - a1 * line1_x1;

		a2 = (line2_y2 - line2_y1) / (line2_x2 - line2_x1);
		b2 = line2_y1 - a2 * line2_x1;

		//

		float ix = (b2 - b1) / (a1 - a2);
		float iy = a1*ix + b1;

		//

		Zenon.DrawSegment(line1_x1, line1_y1, line1_x2, line1_y2, 0.05f, 10002, Color.red);
		Zenon.DrawSegment(line2_x1, line2_y1, line2_x2, line2_y2, 0.05f, 10003, Color.blue);
		Zenon.DrawCircle(ix, iy, 0.1f, 10004, Color.black);
	}
}
