using UnityEngine;

public class VectorsPerpendicular2D : MonoBehaviour
{
	public Vector2 v = new Vector2(3.0f, 0.0f);

	void Update()
	{
		Vector2 n1 = new Vector2(-v.y, v.x);
		Vector2 n2 = new Vector2(v.y, -v.x);

		//

		Zenon.DrawRect(Zenon.GetCanvasWidth(), Zenon.GetCanvasHeight(), 10000, Color.white);
		Zenon.DrawCoordSystem(true, 15.0f, 10.0f, 1.0f, 0.05f, 10001);

		Zenon.DrawAxis(0.0f, 0.0f, v.x, v.y, 0.1f, 10002, Color.red);
		Zenon.DrawAxis(0.0f, 0.0f, n1.x, n1.y, 0.1f, 10002, Zenon.ColorGreen075);
		Zenon.DrawAxis(0.0f, 0.0f, n2.x, n2.y, 0.1f, 10002, Color.blue);
	}
}
