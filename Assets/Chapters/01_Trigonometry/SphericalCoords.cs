using UnityEngine;

// https://en.wikipedia.org/wiki/Spherical_coordinate_system
public class SphericalCoords : MonoBehaviour
{
	public Mesh mesh;
	public Material whiteMaterial;
	public Material redMaterial;

	public float radius = 3.0f;
	public float thetaRange = Mathf.PI;
	public float phiRange = 2.0f * Mathf.PI;
	public float singleTheta = 0.0f;
	public float singlePhi = 0.0f;

	void Update()
	{
		for (float theta = 0.0f; theta < thetaRange; theta += 0.1f)
		{
			for (float phi = 0.0f; phi < phiRange; phi += 0.1f)
			{
				SphericalToCartesian(out float x, out float y, out float z, radius, theta, phi);
				DrawMesh(whiteMaterial, x, y, z);
			}
		}

		SphericalToCartesian(out float x2, out float y2, out float z2, radius, singleTheta, singlePhi);
		DrawMesh(redMaterial, x2, y2, z2, 0.15f);
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
