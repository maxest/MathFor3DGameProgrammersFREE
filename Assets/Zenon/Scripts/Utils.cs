using UnityEngine;

public partial class Zenon
{
	static public Color ColorGreen05 = ColorRGB(0.0f, 0.5f, 0.0f);
	static public Color ColorGreen075 = ColorRGB(0.0f, 0.75f, 0.0f);

	static public void Swap<T>(ref T a, ref T b)
	{
		T temp = a;
		a = b;
		b = temp;
	}

	static public Color ColorRGB(float r, float g, float b)
	{
		return new Color(r, g, b);
	}

	static public Color ColorRGBA(float r, float g, float b, float a)
	{
		return new Color(r, g, b, a);
	}

	static Vector2 RotatePoint(Vector2 point, float angle)
	{
		angle *= Mathf.Deg2Rad;
		float s = Mathf.Sin(angle);
		float c = Mathf.Cos(angle);

		Vector2 newPoint;
		newPoint.x = c * point.x - s * point.y;
		newPoint.y = s * point.x + c * point.y;

		return newPoint;
	}

	static public void ConstructBasis(ref Vector3 v1, out Vector3 v2, out Vector3 v3)
	{
		v1.Normalize();

		float dx = Mathf.Abs(Vector3.Dot(v1, new Vector3(1.0f, 0.0f, 0.0f)));
		float dy = Mathf.Abs(Vector3.Dot(v1, new Vector3(0.0f, 1.0f, 0.0f)));
		float dz = Mathf.Abs(Vector3.Dot(v1, new Vector3(0.0f, 0.0f, 1.0f)));

		float tempD = dx;
		Vector3 tempVector = new Vector3(1.0f, 0.0f, 0.0f);
		if (dy < tempD)
		{
			tempD = dy;
			tempVector = new Vector3(0.0f, 1.0f, 0.0f);
		}
		if (dz < tempD)
		{
			tempD = dz;
			tempVector = new Vector3(0.0f, 0.0f, 1.0f);
		}
		tempVector.Normalize();

		v2 = Vector3.Cross(v1, tempVector);
		v3 = Vector3.Cross(v1, v2);

		v2.Normalize();
		v3.Normalize();
	}
}
