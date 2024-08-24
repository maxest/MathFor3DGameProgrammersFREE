using UnityEngine;

public class VectorsCross : MonoBehaviour
{
	public GameObject vec1GO;
	public GameObject vec2GO;

	void Update()
	{
		Vector3 v1 = vec1GO.transform.position;
		Vector3 v2 = vec2GO.transform.position;

		Vector3 v3 = Vector3.Cross(v1, v2);

		//

		Zenon.DrawGridXY(20.0f, 20.0f, 1.0f, 0.05f, 10000);

		Zenon.DrawAxis3D(Vector3.zero, v1, 0.5f * 0.125f, 0.5f * 0.5f, 0.5f * 0.25f, 10001, Color.red);
		Zenon.DrawAxis3D(Vector3.zero, v2, 0.5f * 0.125f, 0.5f * 0.5f, 0.5f * 0.25f, 10001, Zenon.ColorGreen075);
		Zenon.DrawAxis3D(Vector3.zero, v3, 0.5f * 0.125f, 0.5f * 0.5f, 0.5f * 0.25f, 10001, Color.blue);
	}
}
