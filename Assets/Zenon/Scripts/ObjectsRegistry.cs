using UnityEngine;
using System.Collections.Generic;

public class ObjectsRegistry
{
	public ObjectsRegistry()
	{
		objects = new List<ObjectDesc>();
		timeAtWhichObjectsWereAutomaticallyCleared = -1.0f;
	}

	public void Clear()
	{
		foreach (ObjectDesc obj in objects)
		{
			Object.Destroy(obj.obj);
		}

		objects.Clear();
	}

	public Mesh CreateMesh()
	{
		Mesh mesh = new Mesh();
		Register(mesh);
		return mesh;
	}

	public Material CreateMaterial(Material material_)
	{
		Material material = new Material(material_);
		Register(material);
		return material;
	}

	public Material CreateMaterial(Shader shader)
	{
		Material material = new Material(shader);
		Register(material);
		return material;
	}

	public void Register(Object obj)
	{
		float currentTime = Time.time;

		if (currentTime != timeAtWhichObjectsWereAutomaticallyCleared)
		{
			Clear();
			timeAtWhichObjectsWereAutomaticallyCleared = currentTime;
		}

		ObjectDesc objDesc;
		objDesc.obj = obj;
		objDesc.creationTime = currentTime;

		objects.Add(objDesc);
	}

	private struct ObjectDesc
	{
		public UnityEngine.Object obj;
		public float creationTime;
	}

	private List<ObjectDesc> objects;
	private float timeAtWhichObjectsWereAutomaticallyCleared;
}
