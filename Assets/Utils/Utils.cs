using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Utils
{
	static public Texture2D LoadImage(string path)
	{
		byte[] fileData = File.ReadAllBytes(path);

		Texture2D tex = new Texture2D(1, 1);
		tex.LoadImage(fileData);
		tex.filterMode = FilterMode.Bilinear;
		tex.wrapMode = TextureWrapMode.Clamp;

		return tex;
	}

	static public void SaveImage(string path, Texture2D texture)
	{
		File.WriteAllBytes(path, texture.EncodeToPNG());
	}

	static public List<Vector4> GenerateCubeVertices()
	{
		List<Vector4> verts = new List<Vector4>();

		// front
		verts.Add(new Vector4(-0.5f, -0.5f, -0.5f, 1.0f));
		verts.Add(new Vector4(-0.5f, +0.5f, -0.5f, 1.0f));
		verts.Add(new Vector4(+0.5f, +0.5f, -0.5f, 1.0f));
		verts.Add(new Vector4(-0.5f, -0.5f, -0.5f, 1.0f));
		verts.Add(new Vector4(+0.5f, +0.5f, -0.5f, 1.0f));
		verts.Add(new Vector4(+0.5f, -0.5f, -0.5f, 1.0f));

		// back
		verts.Add(new Vector4(+0.5f, +0.5f, +0.5f, 1.0f));
		verts.Add(new Vector4(-0.5f, +0.5f, +0.5f, 1.0f));
		verts.Add(new Vector4(-0.5f, -0.5f, +0.5f, 1.0f));
		verts.Add(new Vector4(+0.5f, -0.5f, +0.5f, 1.0f));
		verts.Add(new Vector4(+0.5f, +0.5f, +0.5f, 1.0f));
		verts.Add(new Vector4(-0.5f, -0.5f, +0.5f, 1.0f));

		// right
		verts.Add(new Vector4(+0.5f, -0.5f, -0.5f, 1.0f));
		verts.Add(new Vector4(+0.5f, +0.5f, -0.5f, 1.0f));
		verts.Add(new Vector4(+0.5f, +0.5f, +0.5f, 1.0f));
		verts.Add(new Vector4(+0.5f, -0.5f, -0.5f, 1.0f));
		verts.Add(new Vector4(+0.5f, +0.5f, +0.5f, 1.0f));
		verts.Add(new Vector4(+0.5f, -0.5f, +0.5f, 1.0f));

		// left
		verts.Add(new Vector4(-0.5f, +0.5f, +0.5f, 1.0f));
		verts.Add(new Vector4(-0.5f, +0.5f, -0.5f, 1.0f));
		verts.Add(new Vector4(-0.5f, -0.5f, -0.5f, 1.0f));
		verts.Add(new Vector4(-0.5f, -0.5f, +0.5f, 1.0f));
		verts.Add(new Vector4(-0.5f, +0.5f, +0.5f, 1.0f));
		verts.Add(new Vector4(-0.5f, -0.5f, -0.5f, 1.0f));

		// top
		verts.Add(new Vector4(-0.5f, +0.5f, -0.5f, 1.0f));
		verts.Add(new Vector4(-0.5f, +0.5f, +0.5f, 1.0f));
		verts.Add(new Vector4(+0.5f, +0.5f, +0.5f, 1.0f));
		verts.Add(new Vector4(-0.5f, +0.5f, -0.5f, 1.0f));
		verts.Add(new Vector4(+0.5f, +0.5f, +0.5f, 1.0f));
		verts.Add(new Vector4(+0.5f, +0.5f, -0.5f, 1.0f));

		// bottom
		verts.Add(new Vector4(+0.5f, -0.5f, +0.5f, 1.0f));
		verts.Add(new Vector4(-0.5f, -0.5f, +0.5f, 1.0f));
		verts.Add(new Vector4(-0.5f, -0.5f, -0.5f, 1.0f));
		verts.Add(new Vector4(+0.5f, -0.5f, -0.5f, 1.0f));
		verts.Add(new Vector4(+0.5f, -0.5f, +0.5f, 1.0f));
		verts.Add(new Vector4(-0.5f, -0.5f, -0.5f, 1.0f));

		return verts;
	}

	static public void DrawTrianglesWithUnlitColorMaterial(List<Vector4> verts, Color color)
	{
		Material material = Zenon.objectsRegistry.CreateMaterial(Shader.Find("Unlit/Color"));
		material.renderQueue = 10000;
		material.color = color;

		List<Vector3> verts3 = new List<Vector3>();
		for (int i = 0; i < verts.Count; i++)
			verts3.Add(verts[i]);

		List<int> tris = new List<int>();
		for (int i = 0; i < verts.Count; i++)
			tris.Add(i);

		Mesh mesh = Zenon.objectsRegistry.CreateMesh();
		mesh.vertices = verts3.ToArray();
		mesh.triangles = tris.ToArray();

		Graphics.DrawMesh(mesh, Matrix4x4.identity, material, 0);
	}

	static public void DrawTrianglesWithStandardMaterial(List<Vector4> verts, Color color)
	{
		Material material = Zenon.objectsRegistry.CreateMaterial(Shader.Find("Standard"));
		material.renderQueue = 10000;
		material.color = color;

		List<Vector3> verts3 = new List<Vector3>();
		for (int i = 0; i < verts.Count; i++)
			verts3.Add(verts[i]);

		List<Vector3> normals = new List<Vector3>();
		for (int i = 0; i < verts.Count / 3; i++)
		{
			Vector3 normal = GetNormal(verts3[3*i + 0], verts3[3*i + 1], verts3[3*i + 2]);
			normals.Add(normal);
			normals.Add(normal);
			normals.Add(normal);
		}

		List<int> tris = new List<int>();
		for (int i = 0; i < verts.Count; i++)
			tris.Add(i);

		Mesh mesh = Zenon.objectsRegistry.CreateMesh();
		mesh.vertices = verts3.ToArray();
		mesh.normals = normals.ToArray();
		mesh.triangles = tris.ToArray();

		Graphics.DrawMesh(mesh, Matrix4x4.identity, material, 0);
	}

	static public void DrawTriangleWithUnlitColorMaterial(Vector3 v1, Vector3 v2, Vector3 v3, Color color)
	{
		Material material = Zenon.objectsRegistry.CreateMaterial(Shader.Find("Unlit/Color"));
		material.renderQueue = 10000;
		material.color = color;

		List<Vector3> verts = new List<Vector3>();
		verts.Add(v1);
		verts.Add(v2);
		verts.Add(v3);

		List<int> tris = new List<int>();
		tris.Add(0);
		tris.Add(1);
		tris.Add(2);

		Mesh mesh = Zenon.objectsRegistry.CreateMesh();
		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.RecalculateNormals();

		Graphics.DrawMesh(mesh, Matrix4x4.identity, material, 0);
	}

	static public void DrawTexturedTriangleWithStandardMaterial(Texture2D texture, Vector3 v1, Vector3 v2, Vector3 v3, Vector2 uv1, Vector2 uv2, Vector2 uv3, int renderQueue)
	{
		Material material = Zenon.objectsRegistry.CreateMaterial(Shader.Find("Standard"));
		material.renderQueue = renderQueue;
		material.mainTexture = texture;

		List<Vector3> verts = new List<Vector3>();
		verts.Add(v1);
		verts.Add(v2);
		verts.Add(v3);

		List<Vector2> uvs = new List<Vector2>();
		uvs.Add(uv1);
		uvs.Add(uv2);
		uvs.Add(uv3);

		List<int> tris = new List<int>();
		tris.Add(0);
		tris.Add(1);
		tris.Add(2);

		Mesh mesh = Zenon.objectsRegistry.CreateMesh();
		mesh.vertices = verts.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.RecalculateNormals();

		Graphics.DrawMesh(mesh, Matrix4x4.identity, material, 0);
	}

	static public void DrawScreenQuadWithTexture(Texture2D texture, Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
	{
		Material material = Zenon.objectsRegistry.CreateMaterial(Shader.Find("Zenon/Texture"));
		material.renderQueue = 10000;
		material.color = Color.white;
		material.mainTexture = texture;

		List<Vector3> verts = new List<Vector3>();
		verts.Add(v1);
		verts.Add(v2);
		verts.Add(v3);
		verts.Add(v4);

		List<Vector2> uvs = new List<Vector2>();
		uvs.Add(new Vector2(0.0f, 1.0f));
		uvs.Add(new Vector2(1.0f, 1.0f));
		uvs.Add(new Vector2(1.0f, 0.0f));
		uvs.Add(new Vector2(0.0f, 0.0f));

		List<int> tris = new List<int>();
		tris.Add(0);
		tris.Add(1);
		tris.Add(2);
		tris.Add(0);
		tris.Add(2);
		tris.Add(3);

		Mesh mesh = Zenon.objectsRegistry.CreateMesh();
		mesh.vertices = verts.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.bounds = new Bounds(Vector3.zero, new Vector3(float.MaxValue, float.MaxValue, float.MaxValue));

		Graphics.DrawMesh(mesh, Matrix4x4.identity, material, 0);
	}

	static public Vector3 GetNormal(Vector3 p1, Vector3 p2, Vector3 p3)
	{
		return Vector3.Cross(p2 - p1, p3 - p1).normalized;
	}

	static public Vector4 GetPlaneEquation(Vector3 p1, Vector3 p2, Vector3 p3)
	{
		Vector3 normal = GetNormal(p1, p2, p3);

		Vector4 planeEq;
		planeEq.x = normal.x;
		planeEq.y = normal.y;
		planeEq.z = normal.z;
		planeEq.w = -(planeEq.x * p1.x + planeEq.y * p1.y + planeEq.z * p1.z);

		return planeEq;
	}
}
