using UnityEngine;

public class VectorsAdd : MonoBehaviour
{
	public Vector2 v1 = new Vector2(3.0f, 0.0f);
	public Vector2 v2 = new Vector2(0.0f, 2.0f);

	public bool negateV2 = false;

	void Update()
	{
		Vector2 v3;

		if (!negateV2)
		{
			v3 = v1 + v2;
		}
		else
		{
			Vector2 v2_negated = -v2;
			v3 = v1 + v2_negated;
		}

		//

		Zenon.DrawRect(Zenon.GetCanvasWidth(), Zenon.GetCanvasHeight(), 10000, Color.white);
		Zenon.DrawCoordSystem(true, 15.0f, 10.0f, 1.0f, 0.05f, 10001);

		Zenon.DrawAxis(0.0f, 0.0f, v1.x, v1.y, 0.1f, 10002, Color.red);
		Zenon.DrawAxis(v1.x, v1.y, v3.x, v3.y, 0.1f, 10002, Zenon.ColorGreen075);
		Zenon.DrawAxis(0.0f, 0.0f, v3.x, v3.y, 0.1f, 10002, Color.blue);
		if (negateV2)
			Zenon.DrawAxis(v1.x, v1.y, (v1.x + v2.x), (v1.y + v2.y), 0.1f, 10002, Zenon.ColorRGBA(0.0f, 0.75f, 0.0f, 0.33f));

		Zenon.DrawTextWithBackground("[ " + v3.x + ", " + v3.y + " ]", -Zenon.GetCanvasWidth() * 0.5f + 0.1f, Zenon.GetCanvasHeight() * 0.5f - 0.1f, 0.01f, Zenon.HoriAlignment.Left, Zenon.VertAlignment.Top, 10002, Color.black, Color.white);
	}
}
