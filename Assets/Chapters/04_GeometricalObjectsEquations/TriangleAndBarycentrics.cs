using UnityEngine;

public class TriangleAndBarycentrics : MonoBehaviour
{
	public Mesh mesh;
	public Material blackMaterial;
	public Material whiteMaterial;
	public Material redMaterial;
	public Material greenMaterial;

	public Vector3 p1 = new Vector3(0.0f, 0.0f, 0.0f);
	public Vector3 p2 = new Vector3(5.0f, 0.0f, 0.0f);
	public Vector3 p3 = new Vector3(0.0f, 5.0f, 0.0f);
	public Vector2 barycentrics = new Vector2(0.5f, 0.5f);
	public Vector3 p = new Vector3(2.5f, 2.5f, 0.0f);

	void Update()
	{
		Vector3 e1 = p2 - p1;
		Vector3 e2 = p3 - p1;

		DrawMesh(whiteMaterial, p1.x, p1.y, p1.z, 0.5f);
		DrawMesh(whiteMaterial, p2.x, p2.y, p2.z, 0.5f);
		DrawMesh(whiteMaterial, p3.x, p3.y, p3.z, 0.5f);

		Zenon.DrawAxis3D(p1, p1 + e1, 0.25f * 0.125f, 0.75f, 0.5f * 0.25f, 10001, Color.red);
		Zenon.DrawAxis3D(p1, p1 + e2, 0.25f * 0.125f, 0.75f, 0.5f * 0.25f, 10001, Color.red);

		// single point test (barycentrics -> point)
		{
			Vector3 p = p1 + barycentrics.x*e1 + barycentrics.y*e2;

			bool isInside = (
				barycentrics.x >= 0.0f &&
				barycentrics.y >= 0.0f &&
				barycentrics.x + barycentrics.y <= 1.0f);

			DrawMesh(isInside ? redMaterial : blackMaterial, p.x, p.y, p.z, 0.5f);
		}

		// single point test (point -> barycentrics)
		{
			Vector3 pBarycentrics = BarycentricsXY(p1, p2, p3, p);

			bool isInside = (
				pBarycentrics.x >= 0.0f &&
				pBarycentrics.y >= 0.0f &&
				pBarycentrics.x + pBarycentrics.y <= 1.0f);

			DrawMesh(isInside ? redMaterial : blackMaterial, p.x, p.y, p.z, 0.5f);
		}

		// draw grid
		for (float u = -10.0f; u <= 10.0f; u += 0.5f)
		{
			for (float v = -10.0f; v <= 10.0f; v += 0.5f)
			{
				Vector3 p = p1 + u*e1.normalized + v*e2.normalized; // parametric plane equation
				Vector2 pBarycentrics = BarycentricsXY(p1, p2, p3, p);

				bool isInside = (
					pBarycentrics.x >= 0.0f &&
					pBarycentrics.y >= 0.0f &&
					pBarycentrics.x + pBarycentrics.y <= 1.0f);

				DrawMesh(isInside ? greenMaterial : whiteMaterial, p.x, p.y, p.z, 0.2f);
			}
		}
	}

	private Vector2 BarycentricsXY(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p)
	{
		// https://www.wolframalpha.com/input?i=a_1+%2B+u*%28b_1-a_1%29+%2B+v*%28c_1-a_1%29+%3D+d_1%2C+a_2+%2B+u*%28b_2-a_2%29+%2B+v*%28c_2-a_2%29+%3D+d_2%2C+find+u+and+v
		float a1 = p1.x;
		float b1 = p2.x;
		float c1 = p3.x;
		float d1 = p.x;
		float a2 = p1.y;
		float b2 = p2.y;
		float c2 = p3.y;
		float d2 = p.y;
		//
		float u = (a2 * (c1 - d1) + a1 * (d2 - c2) + c2 * d1 - c1 * d2) / (a2 * (c1 - b1) + a1 * (b2 - c2) - b2 * c1 + b1 * c2);
		float v = (a2 * (d1 - b1) + a1 * (b2 - d2) - b2 * d1 + b1 * d2) / (a2 * (c1 - b1) + a1 * (b2 - c2) - b2 * c1 + b1 * c2);

		return new Vector2(u, v);
	}

	private Vector2 BarycentricsYZ(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p)
	{
		// https://www.wolframalpha.com/input?i=a_1+%2B+u*%28b_1-a_1%29+%2B+v*%28c_1-a_1%29+%3D+d_1%2C+a_2+%2B+u*%28b_2-a_2%29+%2B+v*%28c_2-a_2%29+%3D+d_2%2C+find+u+and+v
		float a1 = p1.y;
		float b1 = p2.y;
		float c1 = p3.y;
		float d1 = p.y;
		float a2 = p1.z;
		float b2 = p2.z;
		float c2 = p3.z;
		float d2 = p.z;
		//
		float u = (a2 * (c1 - d1) + a1 * (d2 - c2) + c2 * d1 - c1 * d2) / (a2 * (c1 - b1) + a1 * (b2 - c2) - b2 * c1 + b1 * c2);
		float v = (a2 * (d1 - b1) + a1 * (b2 - d2) - b2 * d1 + b1 * d2) / (a2 * (c1 - b1) + a1 * (b2 - c2) - b2 * c1 + b1 * c2);

		return new Vector2(u, v);
	}

	private Vector2 BarycentricsZX(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p)
	{
		// https://www.wolframalpha.com/input?i=a_1+%2B+u*%28b_1-a_1%29+%2B+v*%28c_1-a_1%29+%3D+d_1%2C+a_2+%2B+u*%28b_2-a_2%29+%2B+v*%28c_2-a_2%29+%3D+d_2%2C+find+u+and+v
		float a1 = p1.z;
		float b1 = p2.z;
		float c1 = p3.z;
		float d1 = p.z;
		float a2 = p1.x;
		float b2 = p2.x;
		float c2 = p3.x;
		float d2 = p.x;
		//
		float u = (a2 * (c1 - d1) + a1 * (d2 - c2) + c2 * d1 - c1 * d2) / (a2 * (c1 - b1) + a1 * (b2 - c2) - b2 * c1 + b1 * c2);
		float v = (a2 * (d1 - b1) + a1 * (b2 - d2) - b2 * d1 + b1 * d2) / (a2 * (c1 - b1) + a1 * (b2 - c2) - b2 * c1 + b1 * c2);

		return new Vector2(u, v);
	}

	private Vector2 Barycentrics3D(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p)
	{
		Vector3 normal = Utils.GetNormal(p1, p2, p3);

		float zAxisLikeness = Vector3.Dot(normal, new Vector3(0.0f, 0.0f, 1.0f));
		float xAxisLikeness = Vector3.Dot(normal, new Vector3(1.0f, 0.0f, 0.0f));
		float yAxisLikeness = Vector3.Dot(normal, new Vector3(0.0f, 1.0f, 0.0f));

		zAxisLikeness = Mathf.Abs(zAxisLikeness);
		xAxisLikeness = Mathf.Abs(xAxisLikeness);
		yAxisLikeness = Mathf.Abs(yAxisLikeness);

		if (zAxisLikeness >= xAxisLikeness && zAxisLikeness >= yAxisLikeness)
		{
			return BarycentricsXY(p1, p2, p3, p);
		}
		else if (xAxisLikeness >= yAxisLikeness)
		{
			return BarycentricsYZ(p1, p2, p3, p);
		}
		else
		{
			return BarycentricsZX(p1, p2, p3, p);
		}
	}

	private Vector2 Barycentrics_Lengyel(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p)
	{
		float Sqr(float x)
		{
			return x * x;
		}

		Vector3 r = p - p1;
		Vector3 q1 = p2 - p1;
		Vector3 q2 = p3 - p1;

		float q1ls = Vector3.Dot(q1, q1);
		float q2ls = Vector3.Dot(q2, q2);

		float det = 1.0f / (q1ls * q2ls - Sqr(Vector3.Dot(q1, q2)));
		float m00 = q2ls;
		float m01 = -Vector3.Dot(q1, q2);
		float m10 = -Vector3.Dot(q1, q2);
		float m11 = q1ls;
		float v0 = Vector3.Dot(r, q1);
		float v1 = Vector3.Dot(r, q2);

		float u = det * (m00 * v0 + m01 * v1);
		float v = det * (m10 * v0 + m11 * v1);

		return new Vector2(u, v);
	}

	private void DrawMesh(Material material, float x, float y, float z, float scale = 0.1f)
	{
		Matrix4x4 transform = Matrix4x4.TRS(new Vector3(x, y, z), Quaternion.identity, new Vector3(scale, scale, scale));
		Graphics.DrawMesh(mesh, transform, material, 0);
	}
}
