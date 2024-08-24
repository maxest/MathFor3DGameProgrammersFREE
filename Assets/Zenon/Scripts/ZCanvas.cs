using System.Collections.Generic;
using UnityEngine;

public partial class Zenon
{
	static public float GetScreenAspectRatio()
	{
		return (float)Screen.width / (float)Screen.height;
	}

	static public float GetCanvasWidth()
	{
		return CanvasWidth;
	}

	static public float GetCanvasHeight()
	{
		return GetCanvasWidth() / GetScreenAspectRatio();
	}

	static public void DrawSegment(float x1, float y1, float x2, float y2, float width, bool extendLength, int renderQueue, Color color)
	{
		Vector2 p1 = new Vector2(x1, y1);
		Vector2 p2 = new Vector2(x2, y2);

		Vector2 dir = p2 - p1;
		dir.Normalize();
		Vector2 normal = new Vector2(-dir.y, dir.x);
		normal.Normalize();

		Material material;
		if (color.a == 1.0f)
			material = objectsRegistry.CreateMaterial(Shader.Find("Zenon/Color"));
		else
			material = objectsRegistry.CreateMaterial(Shader.Find("Zenon/Color_Alpha"));
		material.renderQueue = renderQueue;
		material.color = color;

		List<Vector3> verts = new List<Vector3>();
		if (extendLength)
		{
			verts.Add(p1 - 0.5f * width * normal - dir * width * 0.5f);
			verts.Add(p2 - 0.5f * width * normal + dir * width * 0.5f);
			verts.Add(p2 + 0.5f * width * normal + dir * width * 0.5f);
			verts.Add(p1 + 0.5f * width * normal - dir * width * 0.5f);
		}
		else
		{
			verts.Add(p1 - 0.5f * width * normal);
			verts.Add(p2 - 0.5f * width * normal);
			verts.Add(p2 + 0.5f * width * normal);
			verts.Add(p1 + 0.5f * width * normal);
		}

		for (int i = 0; i < verts.Count; i++)
		{
			verts[i] = TransformCanvasCoordsToScreenCoords(verts[i]);
		}

		List<int> tris = new List<int>();
		tris.Add(0);
		tris.Add(1);
		tris.Add(2);
		tris.Add(0);
		tris.Add(2);
		tris.Add(3);

		Mesh mesh = objectsRegistry.CreateMesh();
		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.bounds = new Bounds(Vector3.zero, new Vector3(float.MaxValue, float.MaxValue, float.MaxValue));

		Graphics.DrawMesh(mesh, Matrix4x4.identity, material, 0);
	}

	static public void DrawSegmentXY(float x1, float y1, float x2, float y2, float width, bool extendLength, int renderQueue, Color color)
	{
		Vector2 p1 = new Vector2(x1, y1);
		Vector2 p2 = new Vector2(x2, y2);

		Vector2 dir = p2 - p1;
		dir.Normalize();
		Vector2 normal = new Vector2(-dir.y, dir.x);
		normal.Normalize();

		Material material;
		if (color.a == 1.0f)
			material = objectsRegistry.CreateMaterial(Shader.Find("Zenon/Color3D"));
		else
			material = objectsRegistry.CreateMaterial(Shader.Find("Zenon/Color3D_Alpha"));
		material.renderQueue = renderQueue;
		material.color = color;

		List<Vector3> verts = new List<Vector3>();
		if (extendLength)
		{
			verts.Add(p1 - 0.5f * width * normal - dir * width * 0.5f);
			verts.Add(p2 - 0.5f * width * normal + dir * width * 0.5f);
			verts.Add(p2 + 0.5f * width * normal + dir * width * 0.5f);
			verts.Add(p1 + 0.5f * width * normal - dir * width * 0.5f);
		}
		else
		{
			verts.Add(p1 - 0.5f * width * normal);
			verts.Add(p2 - 0.5f * width * normal);
			verts.Add(p2 + 0.5f * width * normal);
			verts.Add(p1 + 0.5f * width * normal);
		}

		List<int> tris = new List<int>();
		tris.Add(2);
		tris.Add(1);
		tris.Add(0);
		tris.Add(3);
		tris.Add(2);
		tris.Add(0);

		Mesh mesh = objectsRegistry.CreateMesh();
		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.bounds = new Bounds(Vector3.zero, new Vector3(float.MaxValue, float.MaxValue, float.MaxValue));

		Graphics.DrawMesh(mesh, Matrix4x4.identity, material, 0);
	}

	static public void DrawSegment(float x1, float y1, float x2, float y2, float width, int renderQueue, Color color)
	{
		DrawSegment(x1, y1, x2, y2, width, false, renderQueue, color);
	}

	static public void DrawSegmentDashed(float x1, float y1, float x2, float y2, float width, float dashLength, int renderQueue, Color color)
	{
		Vector2 p1 = new Vector2(x1, y1);
		Vector2 p2 = new Vector2(x2, y2);

		Vector2 dir = p2 - p1;
		float length = dir.magnitude;
		dir.Normalize();

		float stepsCount_float = length / (2.0f * dashLength);
		int stepsCount = Mathf.RoundToInt(stepsCount_float + 0.0001f);

		Vector2 dashP1 = p1;
		for (int i = 0; i < stepsCount; i++)
		{
			Vector2 dashP2 = dashP1 + dashLength * dir;
			DrawSegment(dashP1.x, dashP1.y, dashP2.x, dashP2.y, width, false, renderQueue, color);

			dashP1 += 2.0f * dashLength * dir;
		}

		if (Vector2.Dot(dir, (p2 - dashP1).normalized) > 0.0f)
			DrawSegment(dashP1.x, dashP1.y, p2.x, p2.y, width, false, renderQueue, color);
	}

	static public void DrawRect(float minX, float minY, float maxX, float maxY, float angle, int renderQueue, Color color)
	{
		Material material;
		if (color.a == 1.0f)
			material = objectsRegistry.CreateMaterial(Shader.Find("Zenon/Color"));
		else
			material = objectsRegistry.CreateMaterial(Shader.Find("Zenon/Color_Alpha"));
		material.renderQueue = renderQueue;
		material.color = color;

		List<Vector3> verts = new List<Vector3>();
		verts.Add(new Vector3(minX, minY, 0.0f));
		verts.Add(new Vector3(maxX, minY, 0.0f));
		verts.Add(new Vector3(maxX, maxY, 0.0f));
		verts.Add(new Vector3(minX, maxY, 0.0f));
		for (int i = 0; i < verts.Count; i++)
		{
			verts[i] = RotatePoint(verts[i], angle);
			verts[i] = TransformCanvasCoordsToScreenCoords(verts[i]);
		}

		List<int> tris = new List<int>();
		tris.Add(0);
		tris.Add(1);
		tris.Add(2);
		tris.Add(0);
		tris.Add(2);
		tris.Add(3);

		Mesh mesh = objectsRegistry.CreateMesh();
		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.bounds = new Bounds(Vector3.zero, new Vector3(float.MaxValue, float.MaxValue, float.MaxValue));

		Graphics.DrawMesh(mesh, Matrix4x4.identity, material, 0);
	}

	static public void DrawRect(float minX, float minY, float maxX, float maxY, int renderQueue, Color color)
	{
		DrawRect(minX, minY, maxX, maxY, 0.0f, renderQueue, color);
	}

	static public void DrawRect(float width, float height, int renderQueue, Color color)
	{
		DrawRect(-0.5f * width, -0.5f * height, 0.5f * width, 0.5f * height, renderQueue, color);
	}

	static public void DrawRectWireframe(float minX, float minY, float maxX, float maxY, float width, int renderQueue, Color color)
	{
		DrawSegment(minX, minY, maxX, minY, width, true, renderQueue, color);
		DrawSegment(minX, maxY, maxX, maxY, width, true, renderQueue, color);
		DrawSegment(minX, minY, minX, maxY, width, true, renderQueue, color);
		DrawSegment(maxX, minY, maxX, maxY, width, true, renderQueue, color);
	}

	static public void DrawRectWireframe(float width, float height, int renderQueue, Color color)
	{
		DrawRectWireframe(-0.5f * width, -0.5f * height, 0.5f * width, 0.5f * height, 0.05f, renderQueue, color);
	}

	static public void DrawTriangle(float x1, float y1, float x2, float y2, float x3, float y3, int renderQueue, Color color)
	{
		Material material;
		if (color.a == 1.0f)
			material = objectsRegistry.CreateMaterial(Shader.Find("Zenon/Color"));
		else
			material = objectsRegistry.CreateMaterial(Shader.Find("Zenon/Color_Alpha"));
		material.renderQueue = renderQueue;
		material.color = color;

		List<Vector3> verts = new List<Vector3>();
		verts.Add(new Vector2(x1, y1));
		verts.Add(new Vector2(x2, y2));
		verts.Add(new Vector2(x3, y3));
		for (int i = 0; i < verts.Count; i++)
		{
			verts[i] = TransformCanvasCoordsToScreenCoords(verts[i]);
		}

		List<int> tris = new List<int>();
		tris.Add(0);
		tris.Add(1);
		tris.Add(2);

		Mesh mesh = objectsRegistry.CreateMesh();
		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.bounds = new Bounds(Vector3.zero, new Vector3(float.MaxValue, float.MaxValue, float.MaxValue));

		Graphics.DrawMesh(mesh, Matrix4x4.identity, material, 0);
	}

	static public void DrawTriangle3D(float x1, float y1, float z1, float x2, float y2, float z2, float x3, float y3, float z3, int renderQueue, Color color)
	{
		Material material = objectsRegistry.CreateMaterial(Shader.Find("Standard"));
		material.renderQueue = renderQueue;
		material.color = color;

		List<Vector3> verts = new List<Vector3>();
		verts.Add(new Vector3(x1, y1, z1));
		verts.Add(new Vector3(x2, y2, z2));
		verts.Add(new Vector3(x3, y3, z3));

		List<int> tris = new List<int>();
		tris.Add(0);
		tris.Add(1);
		tris.Add(2);

		Mesh mesh = objectsRegistry.CreateMesh();
		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.RecalculateNormals();

		Graphics.DrawMesh(mesh, Matrix4x4.identity, material, 0);
	}

	static public void DrawTriangle3D(Vector3 v1, Vector3 v2, Vector3 v3, int renderQueue, Color color)
	{
		DrawTriangle3D(v1.x, v1.y, v1.z, v2.x, v2.y, v2.z, v3.x, v3.y, v3.z, renderQueue, color);
	}

	static public void DrawTriangleWireframe(float x1, float y1, float x2, float y2, float x3, float y3, float width, int renderQueue, Color color)
	{
		DrawSegment(x1, y1, x2, y2, width, true, renderQueue, color);
		DrawSegment(x2, y2, x3, y3, width, true, renderQueue, color);
		DrawSegment(x3, y3, x1, y1, width, true, renderQueue, color);
	}

	static public void DrawCircle(float x, float y, float radius, int renderQueue, Color color)
	{
		Material material;
		if (color.a == 1.0f)
			material = objectsRegistry.CreateMaterial(Shader.Find("Zenon/Color"));
		else
			material = objectsRegistry.CreateMaterial(Shader.Find("Zenon/Color_Alpha"));
		material.renderQueue = renderQueue;
		material.color = color;

		int n = 32;
		Vector2[] points = new Vector2[n];
		for (int i = 0; i < n; i++)
		{
			float angle = 2.0f * Mathf.PI * i / n;

			float x2 = x + radius * Mathf.Cos(angle);
			float y2 = y + radius * Mathf.Sin(angle);

			points[i] = new Vector2(x2, y2);
		}

		List<Vector3> verts = new List<Vector3>();
		for (int i = 0; i < n; i++)
		{
			verts.Add(new Vector2(x, y));
			verts.Add(points[(i + 1) % n]);
			verts.Add(points[i]);
		}
		for (int i = 0; i < verts.Count; i++)
		{
			verts[i] = TransformCanvasCoordsToScreenCoords(verts[i]);
		}

		List<int> tris = new List<int>();
		for (int i = 0; i < n; i++)
		{
			tris.Add(3 * i + 0);
			tris.Add(3 * i + 1);
			tris.Add(3 * i + 2);
		}

		Mesh mesh = objectsRegistry.CreateMesh();
		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.bounds = new Bounds(Vector3.zero, new Vector3(float.MaxValue, float.MaxValue, float.MaxValue));

		Graphics.DrawMesh(mesh, Matrix4x4.identity, material, 0);
	}

	static public void DrawCircleXY(float x, float y, float z, float radius, int renderQueue, Color color)
	{
		Material material;
		if (color.a == 1.0f)
			material = objectsRegistry.CreateMaterial(Shader.Find("Zenon/Color3D"));
		else
			material = objectsRegistry.CreateMaterial(Shader.Find("Zenon/Color3D_Alpha"));
		material.renderQueue = renderQueue;
		material.color = color;

		int n = 32;
		Vector3[] points = new Vector3[n];
		for (int i = 0; i < n; i++)
		{
			float angle = 2.0f * Mathf.PI * i / n;

			float x2 = x + radius * Mathf.Cos(angle);
			float y2 = y + radius * Mathf.Sin(angle);

			points[i] = new Vector3(x2, y2, z);
		}

		List<Vector3> verts = new List<Vector3>();
		for (int i = 0; i < n; i++)
		{
			verts.Add(new Vector3(x, y, z));
			verts.Add(points[(i + 1) % n]);
			verts.Add(points[i]);
		}

		List<int> tris = new List<int>();
		for (int i = 0; i < n; i++)
		{
			tris.Add(3 * i + 0);
			tris.Add(3 * i + 1);
			tris.Add(3 * i + 2);
		}

		Mesh mesh = objectsRegistry.CreateMesh();
		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.bounds = new Bounds(Vector3.zero, new Vector3(float.MaxValue, float.MaxValue, float.MaxValue));

		Graphics.DrawMesh(mesh, Matrix4x4.identity, material, 0);
	}

	static public void DrawCircleWireframe(float x, float y, float radius, float width, int renderQueue, Color color)
	{
		Material material;
		if (color.a == 1.0f)
			material = objectsRegistry.CreateMaterial(Shader.Find("Zenon/Color"));
		else
			material = objectsRegistry.CreateMaterial(Shader.Find("Zenon/Color_Alpha"));
		material.renderQueue = renderQueue;
		material.color = color;

		int n = 128;
		Vector2[] points = new Vector2[n];
		for (int i = 0; i < points.Length; i++)
		{
			float angle = 2.0f * Mathf.PI * i / n;

			float x2 = x + radius * Mathf.Cos(angle);
			float y2 = y + radius * Mathf.Sin(angle);

			points[i] = new Vector2(x2, y2);
		}

		for (int i = 0; i < n; i++)
		{
			DrawSegment(points[i].x, points[i].y, points[(i + 1) % n].x, points[(i + 1) % n].y, width, true, renderQueue, color);
		}
	}

	static public void DrawCircleArc(float x, float y, float radius, float startAngle, float stopAngle, int renderQueue, Color color)
	{
		Material material;
		if (color.a == 1.0f)
			material = objectsRegistry.CreateMaterial(Shader.Find("Zenon/Color"));
		else
			material = objectsRegistry.CreateMaterial(Shader.Find("Zenon/Color_Alpha"));
		material.renderQueue = renderQueue;
		material.color = color;

		startAngle *= Mathf.Deg2Rad;
		stopAngle *= Mathf.Deg2Rad;
		float arcAngle = stopAngle - startAngle;

		int n = 32;
		Vector2[] points = new Vector2[n + 1];
		for (int i = 0; i < points.Length; i++)
		{
			float angle = startAngle + arcAngle/n * i;

			float x2 = x + radius * Mathf.Cos(angle);
			float y2 = y + radius * Mathf.Sin(angle);

			points[i] = new Vector2(x2, y2);
		}

		List<Vector3> verts = new List<Vector3>();
		for (int i = 0; i < n; i++)
		{
			verts.Add(new Vector2(x, y));
			verts.Add(points[i + 1]);
			verts.Add(points[i]);
		}
		for (int i = 0; i < verts.Count; i++)
		{
			verts[i] = TransformCanvasCoordsToScreenCoords(verts[i]);
		}

		List<int> tris = new List<int>();
		for (int i = 0; i < n; i++)
		{
			tris.Add(3 * i + 0);
			tris.Add(3 * i + 1);
			tris.Add(3 * i + 2);
		}

		Mesh mesh = objectsRegistry.CreateMesh();
		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.bounds = new Bounds(Vector3.zero, new Vector3(float.MaxValue, float.MaxValue, float.MaxValue));

		Graphics.DrawMesh(mesh, Matrix4x4.identity, material, 0);
	}

	static public void DrawCircleArc(float x, float y, float radius, float startX, float startY, float stopX, float stopY, int renderQueue, Color color)
	{
		Vector2 v1 = new Vector2(startX - x, startY - y).normalized;
		Vector2 v2 = new Vector2(stopX - x, stopY - y).normalized;

		float startAngle;
		if (v1.y >= 0.0f)
		{
			startAngle = Mathf.Acos(Vector2.Dot(v1, Vector2.right)) * Mathf.Rad2Deg;
		}
		else
		{
			startAngle = 360.0f - Mathf.Acos(Vector2.Dot(v1, Vector2.right)) * Mathf.Rad2Deg;
		}

		float stopAngle;
		if (v2.y >= 0.0f)
		{
			stopAngle = Mathf.Acos(Vector2.Dot(v2, Vector2.right)) * Mathf.Rad2Deg;
		}
		else
		{
			stopAngle = 360.0f - Mathf.Acos(Vector2.Dot(v2, Vector2.right)) * Mathf.Rad2Deg;
		}

		if (stopAngle < startAngle)
			stopAngle += 360.0f;

		DrawCircleArc(x, y, radius, startAngle, stopAngle, renderQueue, color);
	}

	static public void DrawCircleArcWireframe(float x, float y, float radius, float startAngle, float stopAngle, float width, int renderQueue, Color color)
	{
		Material material;
		if (color.a == 1.0f)
			material = objectsRegistry.CreateMaterial(Shader.Find("Zenon/Color"));
		else
			material = objectsRegistry.CreateMaterial(Shader.Find("Zenon/Color_Alpha"));
		material.renderQueue = renderQueue;
		material.color = color;

		startAngle *= Mathf.Deg2Rad;
		stopAngle *= Mathf.Deg2Rad;
		float arcAngle = stopAngle - startAngle;

		int n = 32;
		Vector2[] points = new Vector2[n + 1];
		for (int i = 0; i < points.Length; i++)
		{
			float angle = startAngle + arcAngle / n * i;

			float x2 = x + radius * Mathf.Cos(angle);
			float y2 = y + radius * Mathf.Sin(angle);

			points[i] = new Vector2(x2, y2);
		}

		DrawSegment(x, y, points[0].x, points[0].y, width, true, renderQueue, color);
		for (int i = 0; i < n; i++)
		{
			DrawSegment(points[i].x, points[i].y, points[i + 1].x, points[i + 1].y, width, true, renderQueue, color);
		}
		DrawSegment(x, y, points[n].x, points[n].y, width, true, renderQueue, color);
	}

	static public void DrawCircleArcWireframe(float x, float y, float radius, float startX, float startY, float stopX, float stopY, float width, int renderQueue, Color color)
	{
		Vector2 v1 = new Vector2(startX - x, startY - y).normalized;
		Vector2 v2 = new Vector2(stopX - x, stopY - y).normalized;

		float startAngle;
		if (v1.y >= 0.0f)
		{
			startAngle = Mathf.Acos(Vector2.Dot(v1, Vector2.right)) * Mathf.Rad2Deg;
		}
		else
		{
			startAngle = 360.0f - Mathf.Acos(Vector2.Dot(v1, Vector2.right)) * Mathf.Rad2Deg;
		}

		float stopAngle;
		if (v2.y >= 0.0f)
		{
			stopAngle = Mathf.Acos(Vector2.Dot(v2, Vector2.right)) * Mathf.Rad2Deg;
		}
		else
		{
			stopAngle = 360.0f - Mathf.Acos(Vector2.Dot(v2, Vector2.right)) * Mathf.Rad2Deg;
		}

		if (stopAngle < startAngle)
			stopAngle += 360.0f;

		DrawCircleArcWireframe(x, y, radius, startAngle, stopAngle, width, renderQueue, color);
	}

	static public void DrawEllipseWireframe(float x, float y, float radiusX, float radiusY, float width, int renderQueue, Color color)
	{
		Material material;
		if (color.a == 1.0f)
			material = objectsRegistry.CreateMaterial(Shader.Find("Zenon/Color"));
		else
			material = objectsRegistry.CreateMaterial(Shader.Find("Zenon/Color_Alpha"));
		material.renderQueue = renderQueue;
		material.color = color;

		int n = 128;
		Vector2[] points = new Vector2[n];
		for (int i = 0; i < points.Length; i++)
		{
			float angle = 2.0f * Mathf.PI * i / n;

			float x2 = x + radiusX * Mathf.Cos(angle);
			float y2 = y + radiusY * Mathf.Sin(angle);

			points[i] = new Vector2(x2, y2);
		}

		for (int i = 0; i < n; i++)
		{
			DrawSegment(points[i].x, points[i].y, points[(i + 1) % n].x, points[(i + 1) % n].y, width, true, renderQueue, color);
		}
	}

	static public float DrawAngle(float x, float y, float radius, float innerRadius, string text, float textScale, float startAngle, float stopAngle, float width, int renderQueue, Color color)
	{
		Material material;
		if (color.a == 1.0f)
			material = objectsRegistry.CreateMaterial(Shader.Find("Zenon/Color"));
		else
			material = objectsRegistry.CreateMaterial(Shader.Find("Zenon/Color_Alpha"));
		material.renderQueue = renderQueue;
		material.color = color;

		DrawCircleArcWireframe(x, y, innerRadius, startAngle, stopAngle, width, renderQueue, color);

		float angle = stopAngle - startAngle;
		bool isRightAngle = (angle > 89.9f && angle < 90.1f);

		startAngle *= Mathf.Deg2Rad;
		stopAngle *= Mathf.Deg2Rad;

		float startX = x + radius * Mathf.Cos(startAngle);
		float startY = y + radius * Mathf.Sin(startAngle);
		float stopX = x + radius * Mathf.Cos(stopAngle);
		float stopY = y + radius * Mathf.Sin(stopAngle);

		DrawSegment(x, y, startX, startY, width, true, renderQueue, color);
		DrawSegment(x, y, stopX, stopY, width, true, renderQueue, color);

		// text
		{
			float textX = x + 0.5f * innerRadius * Mathf.Cos(0.5f * (startAngle + stopAngle));
			float textY = y + 0.5f * innerRadius * Mathf.Sin(0.5f * (startAngle + stopAngle));

			if (isRightAngle)
			{
				DrawCircle(textX, textY, 0.05f, renderQueue, color);
			}
			else
			{
				DrawText(text, textX, textY, textScale, HoriAlignment.Center, VertAlignment.Center, renderQueue, color);
			}
		}

		return angle;
	}

	static public void DrawText(string text, float x, float y, float scale, HoriAlignment horiAlignment, VertAlignment vertAlignment, int renderQueue, Color color)
	{
		fontRenderer.Draw(text, new Vector2(x, y), scale, horiAlignment, vertAlignment, renderQueue, color);
	}

	static public void DrawText(string text, float x, float y, float scale, int renderQueue, Color color)
	{
		DrawText(text, x, y, scale, HoriAlignment.Left, VertAlignment.Bottom, renderQueue, color);
	}

	static public void DrawTextWithBackground(string text, float x, float y, float scale, HoriAlignment horiAlignment, VertAlignment vertAlignment, int renderQueue, Color color, Color backgroundColor)
	{
		Rect rect = fontRenderer.Draw(text, new Vector2(x, y), scale, horiAlignment, vertAlignment, renderQueue + 1, color);

		float rectBorderSize = 0.1f;
		DrawRect(rect.min.x - rectBorderSize, rect.min.y - rectBorderSize, rect.max.x + rectBorderSize, rect.max.y + rectBorderSize, renderQueue, backgroundColor);
	}

	static public void DrawTextWithBackground(string text, float x, float y, float scale, int renderQueue, Color color, Color backgroundColor)
	{
		DrawTextWithBackground(text, x, y, scale, HoriAlignment.Left, VertAlignment.Bottom, renderQueue, color, backgroundColor);
	}

	static public void DrawTexturedText(string text, float x, float y, float width, float height, int renderQueue, Color color)
	{
		for (int i = 0; i < text.Length; i++)
		{
			Vector2 charPosition = new Vector2(x + i * width, y);
			DrawTexturedChar(text[i], charPosition.x, charPosition.y, width, height, renderQueue, color);
		}
	}

	static public void DrawAxis(float x1, float y1, float x2, float y2, bool dashed, float width, float arrowheadLength, float arrowheadWidth, int renderQueue, Color color)
	{
		Vector2 p1 = new Vector2(x1, y1);
		Vector2 p2 = new Vector2(x2, y2);

		Vector2 dir = p2 - p1;
		dir.Normalize();
		Vector2 normal = new Vector2(-dir.y, dir.x);

		Vector2 p3 = p2;
		p2 -= arrowheadLength * dir;

		//

		if (dashed)
			DrawSegmentDashed(p1.x, p1.y, p2.x, p2.y, width, 0.2f, renderQueue, color);
		else
			DrawSegment(p1.x, p1.y, p2.x, p2.y, width, false, renderQueue, color);

		//

		Vector2 tp1 = p2 - 0.5f * arrowheadWidth * normal;
		Vector2 tp2 = p3;
		Vector2 tp3 = p2 + 0.5f * arrowheadWidth * normal;
		DrawTriangle(tp1.x, tp1.y, tp2.x, tp2.y, tp3.x, tp3.y, renderQueue, color);
	}

	static public void DrawAxis(float x1, float y1, float x2, float y2, float width, int renderQueue, Color color)
	{
		DrawAxis(x1, y1, x2, y2, false, width, 5.0f * width, 3.0f * width, renderQueue, color);
	}

	static public void DrawAxisDashed(float x1, float y1, float x2, float y2, float width, int renderQueue, Color color)
	{
		DrawAxis(x1, y1, x2, y2, true, width, 5.0f * width, 3.0f * width, renderQueue, color);
	}

	static public void DrawAxis_TwoSided(float x1, float y1, float x2, float y2, float width, int renderQueue, Color color)
	{
		float cx = 0.5f * (x1 + x2);
		float cy = 0.5f * (y1 + y2);

		DrawAxis(cx, cy, x1, y1, width, renderQueue, color);
		DrawAxis(cx, cy, x2, y2, width, renderQueue, color);
	}

	static public void DrawNumberLine(bool showNumbers, float size, float step, float width, int renderQueue)
	{
		DrawAxis(-0.5f * size, 0.0f, 0.5f * size, 0.0f, width, renderQueue, Color.black);

		// center segment
		float segmentWidth = width;
		float segmentLength = 0.4f;
		DrawSegment(0.0f, -0.5f * segmentLength, 0.0f, 0.5f * segmentLength, segmentWidth, false, renderQueue, Color.black);
		if (showNumbers)
			DrawText("0", 0.0f, -0.5f, 0.006f, HoriAlignment.Center, VertAlignment.Center, renderQueue, Color.black);

		segmentWidth = 0.5f * width;
		segmentLength = 0.2f;
		int index = 1;
		for (float x = step; x <= 0.45f * size; x += step)
		{
			// left segment
			DrawSegment(-x, -0.5f * segmentLength, -x, 0.5f * segmentLength, segmentWidth, false, renderQueue, Color.black);
			if (showNumbers)
				DrawText("-" + index.ToString(), -x - 0.08f, -0.5f, 0.006f, HoriAlignment.Center, VertAlignment.Center, renderQueue, Color.black);

			// right segment
			DrawSegment(x, -0.5f * segmentLength, x, 0.5f * segmentLength, segmentWidth, false, renderQueue, Color.black);
			if (showNumbers)
				DrawText(index.ToString(), x, -0.5f, 0.006f, HoriAlignment.Center, VertAlignment.Center, renderQueue, Color.black);

			index++;
		}
	}

	static public void DrawCoordSystem(bool grid, float sizeX, float sizeY, float step, float width, int renderQueue)
	{
		DrawAxis(-0.5f * sizeX, 0.0f, 0.5f * sizeX, 0.0f, width, renderQueue, Color.black);
		DrawAxis(0.0f, -0.5f * sizeY, 0.0f, 0.5f * sizeY, width, renderQueue, Color.black);

		float segmentWidth = width;
		float segmentLength = 0.2f;

		int index = 1;
		for (float x = step; x <= 0.45f * sizeX; x += step)
		{
			// left segment
			DrawSegment(-x, -0.5f * segmentLength, -x, 0.5f * segmentLength, segmentWidth, false, renderQueue, Color.black);
			if (grid)
				DrawSegment(-x, -0.5f * sizeY, -x, 0.5f * sizeY, 0.5f * segmentWidth, false, renderQueue, new Color(0.0f, 0.0f, 0.0f, 0.5f));

			// right segment
			DrawSegment(x, -0.5f * segmentLength, x, 0.5f * segmentLength, segmentWidth, false, renderQueue, Color.black);
			if (grid)
				DrawSegment(x, -0.5f * sizeY, x, 0.5f * sizeY, 0.5f * segmentWidth, false, renderQueue, new Color(0.0f, 0.0f, 0.0f, 0.5f));

			index++;
		}
		DrawText("X", 0.5f * sizeX + 0.15f, 0.0f, 0.009f, HoriAlignment.Left, VertAlignment.Center, renderQueue, Color.black);

		index = 1;
		for (float y = step; y <= 0.45f * sizeY; y += step)
		{
			// bottom segment
			DrawSegment(-0.5f * segmentLength, -y, 0.5f * segmentLength, -y, segmentWidth, false, renderQueue, Color.black);
			if (grid)
				DrawSegment(-0.5f * sizeX, -y, 0.5f * sizeX, -y, 0.5f * segmentWidth, false, renderQueue, new Color(0.0f, 0.0f, 0.0f, 0.5f));

			// top segment
			DrawSegment(-0.5f * segmentLength, y, 0.5f * segmentLength, y, segmentWidth, false, renderQueue, Color.black);
			if (grid)
				DrawSegment(-0.5f * sizeX, y, 0.5f * sizeX, y, 0.5f * segmentWidth, false, renderQueue, new Color(0.0f, 0.0f, 0.0f, 0.5f));

			index++;
		}
		DrawText("Y", 0.0f, 0.5f * sizeY + 0.15f, 0.009f, HoriAlignment.Center, VertAlignment.Bottom, renderQueue, Color.black);
	}

	static public void DrawGridXY(float sizeX, float sizeY, float step, float width, int renderQueue)
	{
		for (float x = -0.5f * sizeX; x <= 0.5f * sizeX; x += step)
		{
			DrawSegmentXY(x, -0.5f * sizeY, x, 0.5f * sizeY, width, false, renderQueue, new Color(0.0f, 0.0f, 0.0f, 0.5f));
		}
		DrawSegmentXY(0.0f, -0.5f * sizeY, 0.0f, 0.5f * sizeY, 1.5f * width, false, renderQueue, new Color(0.0f, 0.0f, 0.0f, 1.0f));

		for (float y = -0.5f * sizeY; y <= 0.5f * sizeY; y += step)
		{
			DrawSegmentXY(-0.5f * sizeX, y, 0.5f * sizeX, y, width, false, renderQueue, new Color(0.0f, 0.0f, 0.0f, 0.5f));
		}
		DrawSegmentXY(-0.5f * sizeX, 0.0f, 0.5f * sizeX, 0.0f, 1.5f * width, false, renderQueue, new Color(0.0f, 0.0f, 0.0f, 1.0f));
	}

	static public void DrawCoordSystemPoint(float x, float y, bool dashed, string text, float textScale, HoriAlignment horiAlignment, VertAlignment vertAlignment, int renderQueue, Color color)
	{
		DrawCircle(x, y, 0.1f, renderQueue, color);

		if (dashed)
		{
			DrawSegmentDashed(x, 0.0f, x, y, 0.05f, 0.2f, renderQueue, color);
			DrawSegmentDashed(0.0f, y, x, y, 0.05f, 0.2f, renderQueue, color);
		}

		float textOffsetX = 0.0f;
		float textOffsetY = 0.0f;
		{
			if (horiAlignment == HoriAlignment.Left)
				textOffsetX = 0.15f;
			else if (horiAlignment == HoriAlignment.Right)
				textOffsetX -= 0.15f;

			if (vertAlignment == VertAlignment.Bottom)
				textOffsetY = 0.15f;
			else if (vertAlignment == VertAlignment.Top)
				textOffsetY -= 0.15f;
		}
		DrawText(text, x + textOffsetX, y + textOffsetY, textScale, horiAlignment, vertAlignment, renderQueue, Color.black);
	}

	static public void DrawCoordSystemPoint(float x, float y, bool dashed, string text, HoriAlignment horiAlignment, VertAlignment vertAlignment, int renderQueue, Color color)
	{
		DrawCoordSystemPoint(x, y, dashed, text, 0.006f, horiAlignment, vertAlignment, renderQueue, color);
	}

	static public void DrawCoordSystemPoint(float x, float y, bool dashed, HoriAlignment horiAlignment, VertAlignment vertAlignment, int renderQueue, Color color)
	{
		DrawCoordSystemPoint(x, y, dashed, "(" + x.ToString("0.0") + ", " + y.ToString("0.0") + ")", horiAlignment, vertAlignment, renderQueue, color);
	}

	static public void DrawCoordSystemPoint(float x, float y, bool dashed, string text, int renderQueue, Color color)
	{
		DrawCoordSystemPoint(x, y, dashed, text, HoriAlignment.Left, VertAlignment.Bottom, renderQueue, color);
	}

	static public void DrawCoordSystemPoint(float x, float y, bool dashed, int renderQueue, Color color)
	{
		DrawCoordSystemPoint(x, y, dashed, "(" + x.ToString("0.0") + ", " + y.ToString("0.0") + ")", HoriAlignment.Left, VertAlignment.Bottom, renderQueue, color);
	}

	static public void DrawCoordSystemPoint(float x, float y, int renderQueue, Color color)
	{
		DrawCoordSystemPoint(x, y, true, renderQueue, color);
	}

	static public void DrawFunction(System.Func<float, float> function, float xMin, float xMax, float xStep, float maxAllowedDiffInYBetweenTwoPoints, float width, int renderQueue, Color color)
	{
		List<Vector2> pts = new List<Vector2>();

		for (float x = xMin; x < xMax; x += xStep)
		{
			float y = function(x);
			pts.Add(new Vector2(x, y));
		}
		pts.Add(new Vector2(xMax, function(xMax)));

		for (int i = 1; i < pts.Count; i++)
		{
			Vector2 prevPt = pts[i - 1];
			Vector2 pt = pts[i];

			float diffY = Mathf.Abs(pt.y - prevPt.y);
			if (diffY > maxAllowedDiffInYBetweenTwoPoints)
				continue;

			DrawSegment(prevPt.x, prevPt.y, pt.x, pt.y, width, renderQueue, color);
		}
	}

	static public void DrawFunction(System.Func<float, float> function, float xMin, float xMax, float width, int renderQueue, Color color)
	{
		DrawFunction(function, xMin, xMax, 0.1f, float.MaxValue, width, renderQueue, color);
	}

	static public void DrawCylinder(float x1, float y1, float z1, float x2, float y2, float z2, float radius, int renderQueue, Color color)
	{
		Vector3 dir = new Vector3(x2 - x1, y2 - y1, z2 - z1);

		Quaternion quaternion = new Quaternion();
		quaternion.SetLookRotation(dir);
		Matrix4x4 matrix = Matrix4x4.Rotate(quaternion);

		Material material = objectsRegistry.CreateMaterial(Shader.Find("Standard"));
		material.renderQueue = renderQueue;
		material.color = color;

		int n = 32;
		Vector3[] bottomPoints = new Vector3[n];
		Vector3[] topPoints = new Vector3[n];
		for (int i = 0; i < n; i++)
		{
			float angle = 2.0f * Mathf.PI * i / n;

			float x = radius * Mathf.Cos(angle);
			float y = radius * Mathf.Sin(angle);

			bottomPoints[i] = new Vector3(x, y, 0.0f);
			topPoints[i] = new Vector3(x, y, dir.magnitude);
		}

		List<Vector3> verts = new List<Vector3>();
		for (int i = 0; i < n; i++)
		{
			verts.Add(new Vector3(x1, y1, z1) + matrix.MultiplyPoint(new Vector3(0.0f, 0.0f, 0.0f)));
			verts.Add(new Vector3(x1, y1, z1) + matrix.MultiplyPoint(bottomPoints[(i + 1) % n]));
			verts.Add(new Vector3(x1, y1, z1) + matrix.MultiplyPoint(bottomPoints[i]));

			verts.Add(new Vector3(x1, y1, z1) + matrix.MultiplyPoint(new Vector3(0.0f, 0.0f, dir.magnitude)));
			verts.Add(new Vector3(x1, y1, z1) + matrix.MultiplyPoint(topPoints[(i + 1) % n]));
			verts.Add(new Vector3(x1, y1, z1) + matrix.MultiplyPoint(topPoints[i]));
		}

		List<int> tris = new List<int>();
		for (int i = 0; i < n; i++)
		{
			// bottom
			tris.Add(6 * i + 0);
			tris.Add(6 * i + 1);
			tris.Add(6 * i + 2);

			// top
			tris.Add(6 * i + 3);
			tris.Add(6 * i + 5);
			tris.Add(6 * i + 4);

			// side
			tris.Add(6 * i + 1);
			tris.Add(6 * i + 4);
			tris.Add(6 * i + 5);
			//
			tris.Add(6 * i + 1);
			tris.Add(6 * i + 5);
			tris.Add(6 * i + 2);
		}

		Mesh mesh = objectsRegistry.CreateMesh();
		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.RecalculateNormals();

		Graphics.DrawMesh(mesh, Matrix4x4.identity, material, 0);
	}

	static public void DrawCone(float x1, float y1, float z1, float x2, float y2, float z2, float radius, int renderQueue, Color color)
	{
		Vector3 dir = new Vector3(x2 - x1, y2 - y1, z2 - z1);

		Quaternion quaternion = new Quaternion();
		quaternion.SetLookRotation(dir);
		Matrix4x4 matrix = Matrix4x4.Rotate(quaternion);

		Material material = objectsRegistry.CreateMaterial(Shader.Find("Standard"));
		material.renderQueue = renderQueue;
		material.color = color;

		int n = 32;
		Vector3[] bottomPoints = new Vector3[n];
		Vector3[] topPoints = new Vector3[n];
		for (int i = 0; i < n; i++)
		{
			float angle = 2.0f * Mathf.PI * i / n;

			float x = radius * Mathf.Cos(angle);
			float y = radius * Mathf.Sin(angle);

			bottomPoints[i] = new Vector3(x, y, 0.0f);
			topPoints[i] = new Vector3(x, y, dir.magnitude);
		}

		List<Vector3> verts = new List<Vector3>();
		for (int i = 0; i < n; i++)
		{
			verts.Add(new Vector3(x1, y1, z1) + matrix.MultiplyPoint(new Vector3(0.0f, 0.0f, 0.0f)));
			verts.Add(new Vector3(x1, y1, z1) + matrix.MultiplyPoint(bottomPoints[(i + 1) % n]));
			verts.Add(new Vector3(x1, y1, z1) + matrix.MultiplyPoint(bottomPoints[i]));
			verts.Add(new Vector3(x1, y1, z1) + matrix.MultiplyPoint(new Vector3(0.0f, 0.0f, dir.magnitude)));
		}

		List<int> tris = new List<int>();
		for (int i = 0; i < n; i++)
		{
			// bottom
			tris.Add(4 * i + 0);
			tris.Add(4 * i + 1);
			tris.Add(4 * i + 2);

			// side
			tris.Add(4 * i + 1);
			tris.Add(4 * i + 3);
			tris.Add(4 * i + 2);
		}

		Mesh mesh = objectsRegistry.CreateMesh();
		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.RecalculateNormals();

		Graphics.DrawMesh(mesh, Matrix4x4.identity, material, 0);
	}

	static public void DrawAxis3D(float x1, float y1, float z1, float x2, float y2, float z2, float radius, float arrowheadLength, float arrowheadRadius, int renderQueue, Color color)
	{
		Vector3 p1 = new Vector3(x1, y1, z1);
		Vector3 p2 = new Vector3(x2, y2, z2);

		Vector3 dir = p2 - p1;
		dir.Normalize();

		Vector3 p3 = p2;
		if ((p3 - p1).magnitude < arrowheadLength)
			arrowheadLength = (p3 - p1).magnitude;
		p2 -= arrowheadLength * dir;

		DrawCylinder(p1.x, p1.y, p1.z, p2.x, p2.y, p2.z, radius, renderQueue, color);
		DrawCone(p2.x, p2.y, p2.z, p3.x, p3.y, p3.z, arrowheadRadius, renderQueue, color);
	}

	static public void DrawAxis3D(Vector3 p1, Vector3 p2, float radius, float arrowheadLength, float arrowheadRadius, int renderQueue, Color color)
	{
		DrawAxis3D(p1.x, p1.y, p1.z, p2.x, p2.y, p2.z, radius, arrowheadLength, arrowheadRadius, renderQueue, color);
	}

	static public float CanvasWidth = 20.0f;

	static private Vector2 TransformCanvasCoordsToScreenCoords(Vector2 v)
	{
		Vector2 tv;

		tv.x = 2.0f * v.x / GetCanvasWidth();
		tv.y = -2.0f * v.y / GetCanvasHeight();

		return tv;
	}

	static private void DrawTexturedChar(char c, float x, float y, float width, float height, int renderQueue, Color color)
	{
		byte b = (byte)c;
		Vector2 baseUV = new Vector2((float)(b % 16) / 16.0f, (float)(b / 16) / 16.0f);
		float uvWidth = 1.0f / 16.0f;
		float uvHeight = 1.0f / 16.0f;

		Texture2D fontTexture = Resources.Load<Texture2D>("Textures/Font_Alpha");
		Material material = objectsRegistry.CreateMaterial(Shader.Find("Zenon/Font"));
		material.renderQueue = renderQueue;
		material.mainTexture = fontTexture;
		material.color = color;

		List<Vector3> verts = new List<Vector3>();
		verts.Add(new Vector2(x, y));
		verts.Add(new Vector2(x + width, y));
		verts.Add(new Vector2(x + width, y + height));
		verts.Add(new Vector2(x, y + height));
		for (int i = 0; i < verts.Count; i++)
		{
			verts[i] = TransformCanvasCoordsToScreenCoords(verts[i]);
		}

		List<Vector2> uvs = new List<Vector2>();
		uvs.Add(baseUV + new Vector2(0.0f, uvHeight));
		uvs.Add(baseUV + new Vector2(uvWidth, uvHeight));
		uvs.Add(baseUV + new Vector2(uvWidth, 0.0f));
		uvs.Add(baseUV + new Vector2(0.0f, 0.0f));

		List<int> tris = new List<int>();
		tris.Add(0);
		tris.Add(1);
		tris.Add(2);
		tris.Add(0);
		tris.Add(2);
		tris.Add(3);

		Mesh mesh = objectsRegistry.CreateMesh();
		mesh.vertices = verts.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.bounds = new Bounds(Vector3.zero, new Vector3(float.MaxValue, float.MaxValue, float.MaxValue));

		Graphics.DrawMesh(mesh, Matrix4x4.identity, material, 0);
	}
}
