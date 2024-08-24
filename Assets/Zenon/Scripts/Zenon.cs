using UnityEngine;

public partial class Zenon
{
	static Zenon()
	{
		random = new System.Random(System.Environment.TickCount);

		objectsRegistry = new ObjectsRegistry();

		fontRenderer = new FontRenderer();
		fontRenderer.Create(Shader.Find("Zenon/Font"));
	}

	static public void Clear()
	{
		objectsRegistry.Clear();
	}

	static public System.Random random;
	static public ObjectsRegistry objectsRegistry;
	static public FontRenderer fontRenderer;
}
