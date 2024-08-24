using UnityEngine;

public class SpherePointsGeneration : MonoBehaviour
{
	public Mesh mesh;
	public Material whiteMaterial;
	public Material redMaterial;

	public int idx = 0;
	public int n = 256;
	public float radius = 2.0f;
	public float angleOffset = 0.0f;

	void Update()
	{
		// https://stackoverflow.com/questions/9600801/evenly-distributing-n-points-on-a-sphere
		for (int i = 0; i < n; i++)
		{
			float i_float = (float)i + 0.5f;

			float theta = Mathf.Acos(1.0f - 2.0f * i_float / n);
			float phi = Mathf.PI * (1.0f + Mathf.Sqrt(5.0f)) * i_float;

			SphericalToCartesian(out float x, out float y, out float z, radius, theta, phi + angleOffset);
			DrawMesh(i != idx ? whiteMaterial : redMaterial, x, y, z);
		}
	}

	private void SphericalToCartesian(out float x, out float y, out float z, float radius, float theta, float phi)
	{
		x = radius * Mathf.Sin(theta) * Mathf.Cos(phi);
		y = radius * Mathf.Sin(theta) * Mathf.Sin(phi);
		z = radius * Mathf.Cos(theta);
	}

	private void DrawMesh(Material material, float x, float y, float z, float scale = 0.1f)
	{
		Matrix4x4 transform = Matrix4x4.TRS(new Vector3(x, y, z), Quaternion.identity, new Vector3(scale, scale, scale));
		Graphics.DrawMesh(mesh, transform, material, 0);
	}
}
