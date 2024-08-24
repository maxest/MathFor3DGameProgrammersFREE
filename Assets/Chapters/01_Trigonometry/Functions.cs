using UnityEngine;

public class Functions : MonoBehaviour
{
	public float x1 = 0.0f, y1 = 0.0f;
	public float x2 = 4.0f, y2 = 3.0f;

	void Update()
	{
		float x_atRightAngle = x2;
		float y_atRightAngle = y1;

		Vector2 p1 = new Vector2(x1, y1);
		Vector2 p2 = new Vector2(x_atRightAngle, y_atRightAngle);
		Vector2 p3 = new Vector2(x2, y2);

		Vector2 e1 = p2 - p1;
		Vector2 e2 = p3 - p2;
		Vector2 e3 = p1 - p3;
		Vector2 e1_normal = new Vector2(e1.y, -e1.x).normalized;
		Vector2 e2_normal = new Vector2(e2.y, -e2.x).normalized;
		Vector2 e3_normal = new Vector2(e3.y, -e3.x).normalized;

		Vector2 labelAPosition = p1 + 0.5f*e1 + 0.25f*e1_normal;
		Vector2 labelBPosition = p2 + 0.5f*e2 + 0.25f*e2_normal;
		Vector2 labelCPosition = p3 + 0.5f*e3 + 0.25f*e3_normal;

		//

		float a = e1.magnitude;
		float b = e2.magnitude;
		float c = e3.magnitude;

		float sine = b / c;
		float angleBySine = Mathf.Asin(sine);

		float cosine = a / c;
		float angleByCosine = Mathf.Acos(cosine);

		float tangent = b / a;
		float angleByTangent = Mathf.Atan(tangent);

		Debug.Log("a = " + a + "    b = " + b + "    c = " + c);
		Debug.Log(angleBySine * Mathf.Rad2Deg + "    " + angleByCosine * Mathf.Rad2Deg + "    " + angleByTangent * Mathf.Rad2Deg);

		//

		Zenon.DrawRect(Zenon.GetCanvasWidth(), Zenon.GetCanvasHeight(), 10000, Color.white);

		float startAngle = 0.0f;
		float stopAngle = angleByTangent * Mathf.Rad2Deg;

		Zenon.DrawCircleArc(x1, y1, 2.0f, startAngle, stopAngle, 10001, Zenon.ColorRGB(0.0f, 1.0f, 0.0f));
		Zenon.DrawAngle(x1, y1, 2.0f, 1.5f, "α", 0.007f, startAngle, stopAngle, 0.05f, 10002, Color.black);
		Zenon.DrawTriangleWireframe(x1, y1, x_atRightAngle, y_atRightAngle, x2, y2, 0.05f, 10003, Color.black);

		Zenon.DrawText("a", labelAPosition.x, labelAPosition.y, 0.007f, Zenon.HoriAlignment.Center, Zenon.VertAlignment.Center, 10004, Color.black);
		Zenon.DrawText("b", labelBPosition.x, labelBPosition.y, 0.007f, Zenon.HoriAlignment.Center, Zenon.VertAlignment.Center, 10004, Color.black);
		Zenon.DrawText("c", labelCPosition.x, labelCPosition.y, 0.007f, Zenon.HoriAlignment.Center, Zenon.VertAlignment.Center, 10004, Color.black);
	}
}
