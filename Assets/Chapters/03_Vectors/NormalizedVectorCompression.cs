using UnityEngine;

// https://knarkowicz.wordpress.com/2014/04/16/octahedron-normal-vector-encoding/
public class NormalizedVectorCompression : MonoBehaviour
{
	public enum EncodingMode
	{
		Sqrt,
		Octahedron,
		OctahedronByte
	}

	public EncodingMode encodingMode = EncodingMode.Sqrt;
	public Vector3 v = new Vector3(1.0f, 0.0f, 0.0f);
	public Vector3 v_reconstructed;

	void Update()
	{
		v.Normalize();

		if (encodingMode == EncodingMode.Sqrt)
		{
			SqrtNormalizedVector se = SqrtEncode(v);
			v_reconstructed = SqrtDecode(se);
		}
		else if (encodingMode == EncodingMode.Octahedron)
		{
			Vector2 oe = OctahedronEncode(v);
			v_reconstructed = OctahedronDecode(oe);
		}
		else if (encodingMode == EncodingMode.OctahedronByte)
		{
			Vector2Byte oeb = OctahedronEncode_Byte(v);
			v_reconstructed = OctahedronDecode_Byte(oeb);
		}
	}

	//

	struct SqrtNormalizedVector
	{
		public float x;
		public float y;
		public byte sign;
	}

	private SqrtNormalizedVector SqrtEncode(Vector3 v)
	{
		SqrtNormalizedVector e;
		e.x = v.x;
		e.y = v.y;
		e.sign = (v.z >= 0.0f ? (byte)1 : (byte)0);
		return e;
	}

	private Vector3 SqrtDecode(SqrtNormalizedVector e)
	{
		Vector3 v;
		v.x = e.x;
		v.y = e.y;
		v.z = Mathf.Sqrt(Mathf.Max(0.0f, 1.0f - v.x * v.x - v.y * v.y));

		if (e.sign == 0)
			v.z *= -1.0f;

		return v;
	}

	//

	private Vector2 OctahedronEncode(Vector3 v)
	{
		v /= (Mathf.Abs(v.x) + Mathf.Abs(v.y) + Mathf.Abs(v.z));
		Vector2 octWrap = OctWrap(new Vector2(v.x, v.y));

		Vector2 e;
		e.x = v.z >= 0.0f ? v.x : octWrap.x;
		e.y = v.z >= 0.0f ? v.y : octWrap.y;

		// [-1, 1] to [0, 1]
		e.x = 0.5f * e.x + 0.5f;
		e.y = 0.5f * e.y + 0.5f;

		return e;
	}

	private Vector3 OctahedronDecode(Vector2 e)
	{
		// [0, 1] to [-1, 1]
		e.x = 2.0f * e.x - 1.0f;
		e.y = 2.0f * e.y - 1.0f;

		Vector3 v = new Vector3(e.x, e.y, 1.0f - Mathf.Abs(e.x) - Mathf.Abs(e.y));
		float t = Mathf.Clamp01(-v.z);

		v.x += v.x >= 0.0f ? -t : t;
		v.y += v.y >= 0.0f ? -t : t;

		return v.normalized;
	}

	private Vector2 OctWrap(Vector2 v)
	{
		Vector2 w;

		w.x = (1.0f - Mathf.Abs(v.y)) * (v.x >= 0.0f ? 1.0f : -1.0f);
		w.y = (1.0f - Mathf.Abs(v.x)) * (v.y >= 0.0f ? 1.0f : -1.0f);

		return w;
	}

	//

	struct Vector2Byte
	{
		public byte x;
		public byte y;
	}

	private Vector2Byte OctahedronEncode_Byte(Vector3 v)
	{
		Vector2 e = OctahedronEncode(v);

		Vector2Byte eb;
		// https://twitter.com/rygorous/status/1249875302605438978
		eb.x = (byte)(255.0f * e.x + 0.5f);
		eb.y = (byte)(255.0f * e.y + 0.5f);

		return eb;
	}

	private Vector3 OctahedronDecode_Byte(Vector2Byte e)
	{
		Vector2 ef;
		ef.x = e.x / 255.0f;
		ef.y = e.y / 255.0f;

		return OctahedronDecode(ef);
	}
}
