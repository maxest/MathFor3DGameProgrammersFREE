using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
	public Vector3 triangleP1 = new Vector3(0.0f, 0.0f, 0.0f);
	public Vector3 triangleP2 = new Vector3(0.0f, 0.0f, 10.0f);
	public Vector3 triangleP3 = new Vector3(10.0f, 5.0f, 10.0f);

	public GameObject sphereGO;
	public float sphereX = 2.0f;
	public float sphereZ = 5.0f;

	void Update()
	{
		Zenon.DrawTriangle3D(triangleP1, triangleP2, triangleP3, 10000, Color.white);

		Vector3 triangleN = Vector3.Cross(triangleP2 - triangleP1, triangleP3 - triangleP1).normalized;

		float a = triangleN.x;
		float b = triangleN.y;
		float c = triangleN.z;
		float d = -(a*triangleP1.x + b*triangleP1.y + c*triangleP1.z);

		float sphereY = -(a*sphereX + c*sphereZ + d) / b;

		sphereGO.transform.position = new Vector3(sphereX, sphereY, sphereZ);
	}
}
