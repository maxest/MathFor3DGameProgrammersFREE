using UnityEngine;

public class PointInTriangle3D : MonoBehaviour
{
	public Vector3 lineP1 = new Vector3(0.0f, 0.0f, -10.0f);
	public Vector3 lineP2 = new Vector3(0.0f, 0.0f, 10.0f);

	public Vector3 triangleP1 = new Vector3(-10.0f, -10.0f, 0.0f);
	public Vector3 triangleP2 = new Vector3(0.0f, 10.0f, 0.0f);
	public Vector3 triangleP3 = new Vector3(10.0f, -10.0f, 0.0f);

	public int debugDrawVertexIndex = 0;

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

		Vector3 intersectionPoint = lineP1 + lineV * t;

		//

		intersectionSphereGO.transform.position = intersectionPoint;

		//

		Vector3 v1a = intersectionPoint - triangleP1;
		Vector3 v1b = triangleP2 - triangleP1;
		Vector3 v2a = intersectionPoint - triangleP2;
		Vector3 v2b = triangleP3 - triangleP2;
		Vector3 v3a = intersectionPoint - triangleP3;
		Vector3 v3b = triangleP1 - triangleP3;

		Vector3 cross1 = Vector3.Cross(v1a, v1b);
		Vector3 cross2 = Vector3.Cross(v2a, v2b);
		Vector3 cross3 = Vector3.Cross(v3a, v3b);

		float d1 = Vector3.Dot(cross1, cross2);
		float d2 = Vector3.Dot(cross2, cross3);
		float d3 = Vector3.Dot(cross3, cross1);

		bool isInside = false;
		if (d1 > 0.0f && d2 > 0.0f && d3 > 0.0f)
			isInside = true;

		Zenon.DrawTextWithBackground(isInside.ToString(), -Zenon.GetCanvasWidth() * 0.5f + 0.1f, Zenon.GetCanvasHeight() * 0.5f - 0.1f, 0.01f, Zenon.HoriAlignment.Left, Zenon.VertAlignment.Top, 10003, Color.black, Color.white);

		//

		if (debugDrawVertexIndex == 1)
		{
			Zenon.DrawAxis3D(triangleP1, intersectionPoint, 0.5f * 0.125f, 1.5f, 0.25f, 10002, Color.blue);
			Zenon.DrawAxis3D(triangleP1, triangleP2, 0.5f * 0.125f, 1.5f, 0.25f, 10002, Color.blue);
			Zenon.DrawAxis3D(triangleP1, triangleP1 + cross1, 0.5f * 0.125f, 1.5f, 0.25f, 10002, Color.blue);
		}
		else if (debugDrawVertexIndex == 2)
		{
			Zenon.DrawAxis3D(triangleP2, intersectionPoint, 0.5f * 0.125f, 1.5f, 0.25f, 10002, Color.blue);
			Zenon.DrawAxis3D(triangleP2, triangleP3, 0.5f * 0.125f, 1.5f, 0.25f, 10002, Color.blue);
			Zenon.DrawAxis3D(triangleP2, triangleP2 + cross2, 0.5f * 0.125f, 1.5f, 0.25f, 10002, Color.blue);
		}
		else if (debugDrawVertexIndex == 3)
		{
			Zenon.DrawAxis3D(triangleP3, intersectionPoint, 0.5f * 0.125f, 1.5f, 0.25f, 10002, Color.blue);
			Zenon.DrawAxis3D(triangleP3, triangleP1, 0.5f * 0.125f, 1.5f, 0.25f, 10002, Color.blue);
			Zenon.DrawAxis3D(triangleP3, triangleP3 + cross3, 0.5f * 0.125f, 1.5f, 0.25f, 10002, Color.blue);
		}
	}
}
