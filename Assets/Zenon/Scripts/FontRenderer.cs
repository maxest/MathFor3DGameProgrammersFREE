using UnityEngine;

public partial class Zenon
{
	public class FontRenderer
	{
		public void Create(Shader shader_, string name = "Arial", int size = 72)
		{
			shader = shader_;
			font = Font.CreateDynamicFontFromOSFont(name, size);
		}

		public Rect Draw(string text, Vector2 position, float scale, HoriAlignment horiAlignment, VertAlignment vertAlignment, int renderQueue, Color color)
		{
			font.RequestCharactersInTexture(text);

			Mesh mesh = CreateStringMesh(text, position, scale, horiAlignment, vertAlignment, out Rect rect);

			Material material = objectsRegistry.CreateMaterial(font.material);
			material.shader = shader;
			material.renderQueue = renderQueue;
			material.color = color;

			Graphics.DrawMesh(mesh, Matrix4x4.Translate(position), material, 0);

			return rect;//new Vector2(rect.x + rect.width, rect.y);
		}

		public Rect GetStringRect(string text, Vector2 position, float scale)
		{
			font.RequestCharactersInTexture(text);

			Vector2 min = new Vector2(float.MaxValue, float.MaxValue);
			Vector2 max = new Vector2(float.MinValue, float.MinValue);

			Vector2 charPos = Vector2.zero;
			for (int i = 0; i < text.Length; i++)
			{
				CharacterInfo charInfo;
				font.GetCharacterInfo(text[i], out charInfo);

				Rect charRect = GetCharRect(charInfo);
				Vector2 charMin = position + scale * (charPos + new Vector2(charRect.xMin, charRect.yMin));
				Vector2 charMax = position + scale * (charPos + new Vector2(charRect.xMax, charRect.yMax));

				min.x = Mathf.Min(min.x, charMin.x);
				min.y = Mathf.Min(min.y, charMin.y);
				max.x = Mathf.Max(max.x, charMax.x);
				max.y = Mathf.Max(max.y, charMax.y);

				charPos += new Vector2(GetCharAdvance(charInfo), 0.0f);
			}

			return new Rect(min.x, min.y, max.x - min.x, max.y - min.y);
		}

		public Vector2 GetStringSize(string text, float scale)
		{
			Rect rect = GetStringRect(text, Vector2.zero, scale);
			return new Vector2(rect.width, rect.height);
		}

		private Mesh CreateStringMesh(string text, Vector2 position, float scale, HoriAlignment horiAlignment, VertAlignment vertAlignment, out Rect rect)
		{
			Vector3[] vertices = new Vector3[text.Length * 4];
			Vector2[] uv = new Vector2[text.Length * 4];
			int[] triangles = new int[text.Length * 6];

			Vector2 stringSize = GetStringSize(text, scale);
			if (horiAlignment == HoriAlignment.Center)
				position.x -= 0.5f * stringSize.x;
			else if (horiAlignment == HoriAlignment.Right)
				position.x -= stringSize.x;
			if (vertAlignment == VertAlignment.Center)
				position.y -= 0.5f * stringSize.y;
			else if (vertAlignment == VertAlignment.Top)
				position.y -= stringSize.y;

			Vector2 min = new Vector2(float.MaxValue, float.MaxValue);
			Vector2 max = new Vector2(float.MinValue, float.MinValue);

			Vector2 charPos = Vector2.zero;
			for (int i = 0; i < text.Length; i++)
			{
				CharacterInfo charInfo;
				font.GetCharacterInfo(text[i], out charInfo);

				Rect charRect = GetCharRect(charInfo);
				vertices[4 * i + 0] = position + scale * (charPos + new Vector2(charRect.xMin, charRect.yMax));
				vertices[4 * i + 1] = position + scale * (charPos + new Vector2(charRect.xMax, charRect.yMax));
				vertices[4 * i + 2] = position + scale * (charPos + new Vector2(charRect.xMax, charRect.yMin));
				vertices[4 * i + 3] = position + scale * (charPos + new Vector2(charRect.xMin, charRect.yMin));

				for (int j = 0; j < 4; j++)
				{
					min.x = Mathf.Min(min.x, vertices[4 * i + j].x);
					min.y = Mathf.Min(min.y, vertices[4 * i + j].y);
					max.x = Mathf.Max(max.x, vertices[4 * i + j].x);
					max.y = Mathf.Max(max.y, vertices[4 * i + j].y);

					vertices[4 * i + j] = TransformCanvasCoordsToScreenCoords(vertices[4 * i + j]);
				}

				uv[4 * i + 0] = charInfo.uvTopLeft;
				uv[4 * i + 1] = charInfo.uvTopRight;
				uv[4 * i + 2] = charInfo.uvBottomRight;
				uv[4 * i + 3] = charInfo.uvBottomLeft;

				triangles[6 * i + 0] = 4 * i + 0;
				triangles[6 * i + 1] = 4 * i + 1;
				triangles[6 * i + 2] = 4 * i + 2;

				triangles[6 * i + 3] = 4 * i + 0;
				triangles[6 * i + 4] = 4 * i + 2;
				triangles[6 * i + 5] = 4 * i + 3;

				charPos += new Vector2(GetCharAdvance(charInfo), 0);
			}

			Mesh mesh = objectsRegistry.CreateMesh();
			mesh.vertices = vertices;
			mesh.uv = uv;
			mesh.triangles = triangles;
			mesh.bounds = new Bounds(Vector3.zero, new Vector3(float.MaxValue, float.MaxValue, float.MaxValue));

			rect = new Rect(min.x, min.y, max.x - min.x, max.y - min.y);

			return mesh;
		}

		private Rect GetCharRect(CharacterInfo charInfo)
		{
			if (charInfo.index == 49) // '1'
				return new Rect(0.0f, 0.0f, charInfo.glyphWidth, charInfo.glyphHeight);
			else
				return new Rect(charInfo.minX, charInfo.minY, charInfo.glyphWidth, charInfo.glyphHeight);
		}

		private float GetCharAdvance(CharacterInfo charInfo)
		{
			if (charInfo.index == 49) // '1'
				return charInfo.advance - 6;
			else
				return charInfo.advance;
		}

		private Shader shader;
		private Font font;
	}
}
