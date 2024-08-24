using UnityEngine;

public class ComplexNumbers : MonoBehaviour
{
	[System.Serializable]
	public struct Complex
	{
		public float a, b;

		public Complex(float a, float b)
		{
			this.a = a;
			this.b = b;
		}

		static public Complex Mul(Complex c1, Complex c2)
		{
			Complex c3;

			c3.a = c1.a*c2.a - c1.b*c2.b;
			c3.b = c1.a*c2.b + c2.a*c1.b;

			return c3;
		}
	}

	public Complex c1 = new Complex(1.0f, 0.0f);
	public Complex c2 = new Complex(0.71f, 0.71f);
	public Complex c3;

	void Update()
	{
		Zenon.CanvasWidth = 10.0f;
		Zenon.DrawRect(Zenon.GetCanvasWidth(), Zenon.GetCanvasHeight(), 10000, Color.white);
		Zenon.DrawCoordSystem(true, 15.0f, 10.0f, 1.0f, 0.05f, 10001);

		c3 = Complex.Mul(c1, c2);

		Zenon.DrawCircle(c1.a, c1.b, 0.075f, 10002, Color.red);
		Zenon.DrawCircle(c2.a, c2.b, 0.075f, 10003, Zenon.ColorGreen075);
		Zenon.DrawCircle(c3.a, c3.b, 0.075f, 10004, Color.blue);
		Zenon.DrawSegment(0.0f, 0.0f, c1.a, c1.b, 0.05f, 10002, Color.red);
		Zenon.DrawSegment(0.0f, 0.0f, c2.a, c2.b, 0.05f, 10003, Zenon.ColorGreen075);
		Zenon.DrawSegment(0.0f, 0.0f, c3.a, c3.b, 0.05f, 10004, Color.blue);
	}
}
