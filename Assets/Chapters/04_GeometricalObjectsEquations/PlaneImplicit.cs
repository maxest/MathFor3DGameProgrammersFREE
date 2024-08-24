using UnityEngine;

public class PlaneImplicit : MonoBehaviour
{
	public Vector3 lineP1 = new Vector3(0.0f, 0.0f, -10.0f);
	public Vector3 lineP2 = new Vector3(0.0f, 0.0f, 10.0f);

	public Vector3 triangleP1 = new Vector3(-10.0f, -10.0f, 0.0f);
	public Vector3 triangleP2 = new Vector3(0.0f, 10.0f, 0.0f);
	public Vector3 triangleP3 = new Vector3(10.0f, -10.0f, 0.0f);

	public GameObject intersectionSphereGO;

	void Update()
	{
		Zenon.DrawCylinder(lineP1.x, lineP1.y, lineP1.z, lineP2.x, lineP2.y, lineP2.z, 0.1f, 10000, Color.black);
		Zenon.DrawTriangle3D(triangleP1, triangleP2, triangleP3, 10001, Color.white);

		Vector3 lineV = lineP2 - lineP1;
		Vector3 triangleN = Vector3.Cross(triangleP2 - triangleP1, triangleP3 - triangleP1).normalized;

		// https://www.wolframalpha.com/input?i=A+*+%28x_0+%2B+X+*+t%29+%2B+B+*+%28y_0+%2B+Y+*+t%29+%2B+C+*+%28z_0+%2B+Z+*+t%29+%2B+D+%3D+0%2C+find+t
		float x0 = lineP1.x;
		float y0 = lineP1.y;
		float z0 = lineP1.z;
		float X = lineV.x;
		float Y = lineV.y;
		float Z = lineV.z;
		//
		float a = triangleN.x;
		float b = triangleN.y;
		float c = triangleN.z;
		float d = -(a*triangleP1.x + b*triangleP1.y + c*triangleP1.z);
		//
		float t = -(a*x0 + b*y0 + c*z0 + d) / (a*X + b*Y + c*Z);

		intersectionSphereGO.transform.position = lineP1 + lineV * t;

		Zenon.DrawTextWithBackground("t: " + t, -Zenon.GetCanvasWidth() * 0.5f + 0.1f, Zenon.GetCanvasHeight() * 0.5f - 0.1f, 0.01f, Zenon.HoriAlignment.Left, Zenon.VertAlignment.Top, 10002, Color.black, Color.white);
	}
}
