using UnityEngine;

public class PolarCoordsMovement : MonoBehaviour
{
	public float x = 0.0f, y = 0.0f;
	public float angle = 0.0f;

	void Update()
	{
		Zenon.DrawRect(Zenon.GetCanvasWidth(), Zenon.GetCanvasHeight(), 10000, Color.white);
		Zenon.DrawCoordSystem(true, 15.0f, 10.0f, 1.0f, 0.05f, 10001);

		// move and draw the "player"

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			angle += 3.0f * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.RightArrow))
		{
			angle -= 3.0f * Time.deltaTime;
		}

		float speed = 0.0f;
		if (Input.GetKey(KeyCode.UpArrow))
		{
			speed = 3.0f * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			speed = -3.0f * Time.deltaTime;
		}

		PolarToCartesian(out float dx, out float dy, speed, angle);
		x += dx;
		y += dy;

		Zenon.DrawCircle(x, y, 0.2f, 10002, Color.red);

		// draw arrow

		PolarToCartesian(out float dx2, out float dy2, 2.0f, angle);
		float x2 = x + dx2;
		float y2 = y + dy2;

		Zenon.DrawAxis(x, y, x2, y2, 0.1f, 10003, Color.blue);
	}

	private void PolarToCartesian(out float x, out float y, float radius, float angle)
	{
		x = radius * Mathf.Cos(angle);
		y = radius * Mathf.Sin(angle);
	}
}
