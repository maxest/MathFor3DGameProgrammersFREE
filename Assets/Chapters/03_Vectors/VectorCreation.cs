using UnityEngine;

public class VectorCreation : MonoBehaviour
{
	public Vector2 p1 = new Vector2(1.0f, 2.0f);
	public Vector2 p2 = new Vector2(4.0f, -3.0f);

	void Update()
	{
		Vector2 v = p2 - p1;

		//

		Zenon.DrawRect(Zenon.GetCanvasWidth(), Zenon.GetCanvasHeight(), 10000, Color.white);
		Zenon.DrawCoordSystem(true, 15.0f, 10.0f, 1.0f, 0.05f, 10001);	

		Zenon.DrawCircle(p1.x, p1.y, 0.2f, 10002, Color.red);
		Zenon.DrawCircle(p2.x, p2.y, 0.2f, 10002, Color.red);
		Zenon.DrawAxis(p1.x, p1.y, p1.x + v.x, p1.y + v.y, 0.1f, 10003, Color.blue);
	}
}
