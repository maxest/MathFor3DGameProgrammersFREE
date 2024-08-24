public partial class Zenon
{
	static public int Random(int min, int max)
	{
		return random.Next(min, max + 1);
	}

	static public float Random(float min, float max)
	{
		float f = (float)random.NextDouble();
		f *= (max - min);
		f += min;
		return f;
	}

	static public void Shuffle<T>(T[] arr)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			int idx = Random(0, arr.Length - 1);
			Swap(ref arr[i], ref arr[idx]);
		}
	}
}
