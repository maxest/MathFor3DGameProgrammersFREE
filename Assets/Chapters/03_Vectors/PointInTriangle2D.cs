using UnityEngine;

public class PointInTriangle2D : MonoBehaviour
{
	public Vector2 a = new Vector2(0.0f, 0.0f);
	public Vector2 b = new Vector2(2.5f, 5.0f);
	public Vector2 c = new Vector2(5.0f, 0.0f);
	public Vector2 p = new Vector2(2.0f, 2.5f);

	public int debugDrawVertexIndex = 0;

	void Update()
	{
		Vector3 ap_ab = Vector3.Cross(p - a, b - a);
		Vector3 bp_bc = Vector3.Cross(p - b, c - b);
		Vector3 cp_ca = Vector3.Cross(p - c, a - c);

		bool isInside = false;
		if (ap_ab.z > 0.0f &&
			bp_bc.z > 0.0f &&
			cp_ca.z > 0.0f)
		{
			isInside = true;
		}

		//

		Zenon.DrawGridXY(20.0f, 20.0f, 1.0f, 0.05f, 10000);

		Zenon.DrawSegmentXY(a.x, a.y, b.x, b.y, 0.1f, false, 10001, Color.red);
		Zenon.DrawSegmentXY(b.x, b.y, c.x, c.y, 0.1f, false, 10001, Color.red);
		Zenon.DrawSegmentXY(c.x, c.y, a.x, a.y, 0.1f, false, 10001, Color.red);

		Zenon.DrawCircleXY(a.x, a.y, 0.0f, 0.2f, 10002, Color.red);
		Zenon.DrawCircleXY(b.x, b.y, 0.0f, 0.2f, 10002, Color.red);
		Zenon.DrawCircleXY(c.x, c.y, 0.0f, 0.2f, 10002, Color.red);
		Zenon.DrawCircleXY(p.x, p.y, 0.0f, 0.2f, 10002, Color.blue);

		if (debugDrawVertexIndex == 1)
		{
			Zenon.DrawSegmentXY(a.x, a.y, p.x, p.y, 0.1f, false, 10003, Color.blue);
			Zenon.DrawAxis3D(a, new Vector3(a.x, a.y, 0.0f) + ap_ab, 0.05f, 0.2f, 0.1f, 10004, Zenon.ColorGreen075);
		}
		else if (debugDrawVertexIndex == 2)
		{
			Zenon.DrawSegmentXY(b.x, b.y, p.x, p.y, 0.1f, false, 10003, Color.blue);
			Zenon.DrawAxis3D(b, new Vector3(b.x, b.y, 0.0f) + bp_bc, 0.05f, 0.2f, 0.1f, 10004, Zenon.ColorGreen075);
		}
		else if (debugDrawVertexIndex == 3)
		{
			Zenon.DrawSegmentXY(c.x, c.y, p.x, p.y, 0.1f, false, 10003, Color.blue);
			Zenon.DrawAxis3D(c, new Vector3(c.x, c.y, 0.0f) + cp_ca, 0.05f, 0.2f, 0.1f, 10004, Zenon.ColorGreen075);
		}

		Zenon.DrawTextWithBackground(isInside.ToString(), -Zenon.GetCanvasWidth() * 0.5f + 0.1f, Zenon.GetCanvasHeight() * 0.5f - 0.1f, 0.01f, Zenon.HoriAlignment.Left, Zenon.VertAlignment.Top, 10005, Color.black, Color.white);
	}
}
