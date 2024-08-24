using UnityEngine;

public class PlaneParametric : MonoBehaviour
{
	public Mesh mesh;
	public Material material;

	public Vector3 planeO = new Vector3(0.0f, 0.0f, 0.0f);
	public Vector3 planeT = new Vector3(1.0f, 0.0f, 0.0f);
	public Vector3 planeB = new Vector3(0.0f, 1.0f, 0.0f);
	public float planeMinU = 0.0f;
	public float planeMaxU = 10.0f;
	public float planeMinV = 0.0f;
	public float planeMaxV = 10.0f;

	public bool displayNormal = false;

	public bool findIntersection = false;
	public GameObject intersectionSphereGO;
	public Vector3 lineP1 = new Vector3(5.0f, 5.0f, -10.0f);
	public Vector3 lineP2 = new Vector3(5.0f, 5.0f, 10.0f);

	void Update()
	{
		planeT.Normalize();
		planeB.Normalize();

		Zenon.DrawAxis3D(planeO, planeO + planeT, 0.5f * 0.125f, 0.5f * 0.5f, 0.5f * 0.25f, 10001, Color.red);
		Zenon.DrawAxis3D(planeO, planeO + planeB, 0.5f * 0.125f, 0.5f * 0.5f, 0.5f * 0.25f, 10002, Zenon.ColorGreen075);

		for (float u = planeMinU; u <= planeMaxU; u += 0.2f)
		{
			for (float v = planeMinV; v <= planeMaxV; v += 0.2f)
			{
				Vector3 p = planeO + u*planeT + v*planeB;
				DrawMesh(p.x, p.y, p.z);
			}
		}

		if (displayNormal)
		{
			Vector3 normal = Vector3.Cross(planeT, planeB);
			Zenon.DrawAxis3D(planeO, planeO + normal, 0.5f * 0.125f, 0.5f * 0.5f, 0.5f * 0.25f, 10003, Color.blue);
		}

		if (findIntersection)
		{
			Vector3 lineV = lineP2 - lineP1;

			// https://www.wolframalpha.com/input?i=o_1+%2B+u*t_1+%2B+v*b_1+%3D+x_0+%2B+X*t%2C+o_2+%2B+u*t_2+%2B+v*b_2+%3D+y_0+%2B+Y*t%2C+o_3+%2B+u*t_3+%2B+v*b_3+%3D+z_0+%2B+Z*t%2C+find+u%2C+v+and+t
			float x0 = lineP1.x;
			float y0 = lineP1.y;
			float z0 = lineP1.z;
			float X = lineV.x;
			float Y = lineV.y;
			float Z = lineV.z;
			//
			float o0 = planeO.x;
			float t0 = planeT.x;
			float b0 = planeB.x;
			float o1 = planeO.y;
			float t1 = planeT.y;
			float b1 = planeB.y;
			float o2 = planeO.z;
			float t2 = planeT.z;
			float b2 = planeB.z;
			//
			float t =
				-((b1*o0*t2 - b2*o0*t1 - b0*o1*t2 + b2*o1*t0 + b0*o2*t1 - b1*o2*t0 -
				b1*t2*x0 + b2*t1*x0 + b0*t2*y0 - b2*t0*y0 - b0*t1*z0 + b1*t0*z0) /
				(-b1*t2*X + b2*t1*X + b0*t2*Y - b2*t0*Y + b0*t1*(-Z) + b1*t0*Z));

			intersectionSphereGO.transform.position = lineP1 + lineV * t;
			intersectionSphereGO.SetActive(true);

			Zenon.DrawCylinder(lineP1.x, lineP1.y, lineP1.z, lineP2.x, lineP2.y, lineP2.z, 0.1f, 10000, Color.black);
			Zenon.DrawTextWithBackground("t: " + t, -Zenon.GetCanvasWidth() * 0.5f + 0.1f, Zenon.GetCanvasHeight() * 0.5f - 0.1f, 0.01f, Zenon.HoriAlignment.Left, Zenon.VertAlignment.Top, 10004, Color.black, Color.white);
		}
		else
		{
			intersectionSphereGO.SetActive(false);
		}
	}

	private void DrawMesh(float x, float y, float z, float scale = 0.1f)
	{
		Matrix4x4 transform = Matrix4x4.TRS(new Vector3(x, y, z), Quaternion.identity, new Vector3(scale, scale, scale));
		Graphics.DrawMesh(mesh, transform, material, 0);
	}
}
