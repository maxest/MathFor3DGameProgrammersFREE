using UnityEngine;

public class VectorNormalize : MonoBehaviour
{
	public Vector2 v = new Vector2(3.0f, 2.0f);

	void Update()
	{
		Vector2 v_normalized = v / v.magnitude;

	//	Vector2 v_normalized = v.normalized;

	//	Vector2 v_normalized = v;
	//	v_normalized.Normalize();

		//

		Zenon.DrawRect(Zenon.GetCanvasWidth(), Zenon.GetCanvasHeight(), 10000, Color.white);
		Zenon.DrawCoordSystem(true, 15.0f, 10.0f, 1.0f, 0.05f, 10001);

		Zenon.DrawAxis(0.0f, 0.0f, v.x, v.y, 0.1f, 10002, Color.red);
		Zenon.DrawAxis(0.0f, 0.0f, v_normalized.x, v_normalized.y, 0.1f, 10003, Zenon.ColorGreen075);
	}
}
