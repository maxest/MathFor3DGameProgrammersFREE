using UnityEngine;

public class VectorProjection : MonoBehaviour
{
	public Vector2 baseVector = new Vector2(4.0f, -2.0f);
	public Vector2 vectorToProject = new Vector2(3.0f, 3.0f);

	void Update()
	{
		// base vector is not normalized
		float baseVector_lengthSqr = Vector2.Dot(baseVector, baseVector);
		Vector2 projectedVector = (Vector2.Dot(baseVector, vectorToProject) * baseVector) / (baseVector_lengthSqr);

		// base vector is normalized
	//	Vector2 baseVector_normalized = baseVector.normalized;
	//	Vector2 projectedVector = Vector2.Dot(baseVector_normalized, vectorToProject) * baseVector_normalized;

		//

		Zenon.DrawRect(Zenon.GetCanvasWidth(), Zenon.GetCanvasHeight(), 10000, Color.white);
		Zenon.DrawCoordSystem(true, 15.0f, 10.0f, 1.0f, 0.05f, 10001);

		Zenon.DrawAxis(0.0f, 0.0f, baseVector.x, baseVector.y, 0.1f, 10002, Color.red);
		Zenon.DrawAxis(0.0f, 0.0f, vectorToProject.x, vectorToProject.y, 0.1f, 10003, Zenon.ColorGreen075);
		Zenon.DrawAxis(0.0f, 0.0f, projectedVector.x, projectedVector.y, 0.1f, 10004, Color.blue);
	}
}
