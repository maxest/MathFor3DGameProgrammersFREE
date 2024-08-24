using UnityEngine;

public class PolarCoords : MonoBehaviour
{
	public enum ConversionDirection
	{
		PolarToCartesian,
		CartesianToPolar
	}

	public ConversionDirection conversionDirection = ConversionDirection.PolarToCartesian;
	public float radius = 2.0f, angle = 0.0f;
	public float x = 0.0f, y = 0.0f;

	void Update()
	{
		Zenon.DrawRect(Zenon.GetCanvasWidth(), Zenon.GetCanvasHeight(), 10000, Color.white);
		Zenon.DrawCoordSystem(true, 15.0f, 10.0f, 1.0f, 0.05f, 10001);

		if (conversionDirection == ConversionDirection.PolarToCartesian)
		{
			PolarToCartesian(out x, out y, radius, angle);

			Zenon.DrawCircle(x, y, 0.2f, 10002, Color.red);
			Zenon.DrawAngle(0.0f, 0.0f, radius, 1.0f, "α", 0.007f, 0.0f, angle, 0.05f, 10002, Color.red);			
		}
		else if (conversionDirection == ConversionDirection.CartesianToPolar)
		{
			CartesianToPolar(out radius, out angle, x, y);

			Zenon.DrawCircle(x, y, 0.2f, 10002, Color.blue);
			Zenon.DrawSegmentDashed(0.0f, 0.0f, x, y, 0.05f, 0.1f, 10002, Color.blue);
		}
	}

	private void PolarToCartesian(out float x, out float y, float radius, float angle)
	{
		angle *= Mathf.Deg2Rad;

		x = radius * Mathf.Cos(angle);
		y = radius * Mathf.Sin(angle);
	}

	private void CartesianToPolar(out float radius, out float angle, float x, float y)
	{
		radius = Mathf.Sqrt(x*x + y*y);
		angle = Mathf.Atan2(y, x);

		angle *= Mathf.Rad2Deg;
	}
}
