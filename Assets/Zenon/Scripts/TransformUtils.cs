using UnityEngine;

public partial class Zenon
{
	// Position

	static public void SetPosition(Transform transform, float x, float y, float z)
	{
		transform.position = new Vector3(x, y, z);
	}

	static public void SetPositionX(Transform transform, float x)
	{
		Vector3 temp = transform.position;
		temp.x = x;
		transform.position = temp;
	}

	static public void SetPositionY(Transform transform, float y)
	{
		Vector3 temp = transform.position;
		temp.y = y;
		transform.position = temp;
	}

	static public void SetPositionZ(Transform transform, float z)
	{
		Vector3 temp = transform.position;
		temp.z = z;
		transform.position = temp;
	}

	static public void AddToPosition(Transform transform, float x, float y, float z)
	{
		Vector3 temp = transform.position;
		temp.x += x;
		temp.y += y;
		temp.z += z;
		transform.position = temp;
	}

	static public void AddToPositionX(Transform transform, float x)
	{
		Vector3 temp = transform.position;
		temp.x += x;
		transform.position = temp;
	}

	static public void AddToPositionY(Transform transform, float y)
	{
		Vector3 temp = transform.position;
		temp.y += y;
		transform.position = temp;
	}

	static public void AddToPositionZ(Transform transform, float z)
	{
		Vector3 temp = transform.position;
		temp.z += z;
		transform.position = temp;
	}

	static public Vector3 GetPosition(Transform transform)
	{
		return transform.position;
	}

	static public float GetPositionX(Transform transform)
	{
		return transform.position.x;
	}

	static public float GetPositionY(Transform transform)
	{
		return transform.position.y;
	}

	static public float GetPositionZ(Transform transform)
	{
		return transform.position.z;
	}

	static public void SetLocalPosition(Transform transform, float x, float y, float z)
	{
		transform.localPosition = new Vector3(x, y, z);
	}

	static public void SetLocalPositionX(Transform transform, float x)
	{
		Vector3 temp = transform.localPosition;
		temp.x = x;
		transform.localPosition = temp;
	}

	static public void SetLocalPositionY(Transform transform, float y)
	{
		Vector3 temp = transform.localPosition;
		temp.y = y;
		transform.localPosition = temp;
	}

	static public void SetLocalPositionZ(Transform transform, float z)
	{
		Vector3 temp = transform.localPosition;
		temp.z = z;
		transform.localPosition = temp;
	}

	static public void AddToLocalPosition(Transform transform, float x, float y, float z)
	{
		Vector3 temp = transform.localPosition;
		temp.x += x;
		temp.y += y;
		temp.z += z;
		transform.localPosition = temp;
	}

	static public void AddToLocalPositionX(Transform transform, float x)
	{
		Vector3 temp = transform.localPosition;
		temp.x += x;
		transform.localPosition = temp;
	}

	static public void AddToLocalPositionY(Transform transform, float y)
	{
		Vector3 temp = transform.localPosition;
		temp.y += y;
		transform.localPosition = temp;
	}

	static public void AddToLocalPositionZ(Transform transform, float z)
	{
		Vector3 temp = transform.localPosition;
		temp.z += z;
		transform.localPosition = temp;
	}

	static public Vector3 GetLocalPosition(Transform transform)
	{
		return transform.localPosition;
	}

	static public float GetLocalPositionX(Transform transform)
	{
		return transform.localPosition.x;
	}

	static public float GetLocalPositionY(Transform transform)
	{
		return transform.localPosition.y;
	}

	static public float GetLocalPositionZ(Transform transform)
	{
		return transform.localPosition.z;
	}

	// Rotation

	static public void SetRotation(Transform transform, float x, float y, float z)
	{
		transform.rotation = Quaternion.Euler(x, y, z);
	}

	static public void SetRotationX(Transform transform, float x)
	{
		Vector3 temp = transform.rotation.eulerAngles;
		temp.x = x;
		transform.rotation = Quaternion.Euler(temp);
	}

	static public void SetRotationY(Transform transform, float y)
	{
		Vector3 temp = transform.rotation.eulerAngles;
		temp.y = y;
		transform.rotation = Quaternion.Euler(temp);
	}

	static public void SetRotationZ(Transform transform, float z)
	{
		Vector3 temp = transform.rotation.eulerAngles;
		temp.z = z;
		transform.rotation = Quaternion.Euler(temp);
	}

	static public void AddToRotation(Transform transform, float x, float y, float z)
	{
		transform.rotation *= Quaternion.Euler(x, y, z);
	}

	static public void AddToRotationX(Transform transform, float x)
	{
		transform.rotation *= Quaternion.Euler(x, 0.0f, 0.0f);
	}

	static public void AddToRotationY(Transform transform, float y)
	{
		transform.rotation *= Quaternion.Euler(0.0f, y, 0.0f);
	}

	static public void AddToRotationZ(Transform transform, float z)
	{
		transform.rotation *= Quaternion.Euler(0.0f, 0.0f, z);
	}

	static public Vector3 GetRotation(Transform transform)
	{
		return transform.rotation.eulerAngles;
	}

	static public float GetRotationX(Transform transform)
	{
		return transform.rotation.eulerAngles.x;
	}

	static public float GetRotationY(Transform transform)
	{
		return transform.rotation.eulerAngles.y;
	}

	static public float GetRotationZ(Transform transform)
	{
		return transform.rotation.eulerAngles.z;
	}

	static public void SetLocalRotation(Transform transform, float x, float y, float z)
	{
		transform.localRotation = Quaternion.Euler(x, y, z);
	}

	static public void SetLocalRotationX(Transform transform, float x)
	{
		Vector3 temp = transform.localRotation.eulerAngles;
		temp.x = x;
		transform.localRotation = Quaternion.Euler(temp);
	}

	static public void SetLocalRotationY(Transform transform, float y)
	{
		Vector3 temp = transform.localRotation.eulerAngles;
		temp.y = y;
		transform.localRotation = Quaternion.Euler(temp);
	}

	static public void SetLocalRotationZ(Transform transform, float z)
	{
		Vector3 temp = transform.localRotation.eulerAngles;
		temp.z = z;
		transform.localRotation = Quaternion.Euler(temp);
	}

	static public void AddToLocalRotation(Transform transform, float x, float y, float z)
	{
		transform.localRotation *= Quaternion.Euler(x, y, z);
	}

	static public void AddToLocalRotationX(Transform transform, float x)
	{
		transform.localRotation *= Quaternion.Euler(x, 0.0f, 0.0f);
	}

	static public void AddToLocalRotationY(Transform transform, float y)
	{
		transform.localRotation *= Quaternion.Euler(0.0f, y, 0.0f);
	}

	static public void AddToLocalRotationZ(Transform transform, float z)
	{
		transform.localRotation *= Quaternion.Euler(0.0f, 0.0f, z);
	}

	static public Vector3 GetLocalRotation(Transform transform)
	{
		return transform.localRotation.eulerAngles;
	}

	static public float GetLocalRotationX(Transform transform)
	{
		return transform.localRotation.eulerAngles.x;
	}

	static public float GetLocalRotationY(Transform transform)
	{
		return transform.localRotation.eulerAngles.y;
	}

	static public float GetLocalRotationZ(Transform transform)
	{
		return transform.localRotation.eulerAngles.z;
	}

	// Scale

	static public void SetScale(Transform transform, float x, float y, float z)
	{
		transform.localScale = new Vector3(x, y, z);
	}

	static public void SetScaleX(Transform transform, float x)
	{
		Vector3 temp = transform.localScale;
		temp.x = x;
		transform.localScale = temp;
	}

	static public void SetScaleY(Transform transform, float y)
	{
		Vector3 temp = transform.localScale;
		temp.y = y;
		transform.localScale = temp;
	}

	static public void SetScaleZ(Transform transform, float z)
	{
		Vector3 temp = transform.localScale;
		temp.z = z;
		transform.localScale = temp;
	}

	static public void AddToScale(Transform transform, float x, float y, float z)
	{
		Vector3 temp = transform.position;
		temp.x += x;
		temp.y += y;
		temp.z += z;
		transform.localScale = temp;
	}

	static public void AddToScaleX(Transform transform, float x)
	{
		Vector3 temp = transform.localScale;
		temp.x += x;
		transform.localScale = temp;
	}

	static public void AddToScaleY(Transform transform, float y)
	{
		Vector3 temp = transform.localScale;
		temp.y += y;
		transform.localScale = temp;
	}

	static public void AddToScaleZ(Transform transform, float z)
	{
		Vector3 temp = transform.localScale;
		temp.z += z;
		transform.localScale = temp;
	}

	static public Vector3 GetScale(Transform transform)
	{
		return transform.localScale;
	}

	static public float GetScaleX(Transform transform)
	{
		return transform.localScale.x;
	}

	static public float GetScaleY(Transform transform)
	{
		return transform.localScale.y;
	}

	static public float GetScaleZ(Transform transform)
	{
		return transform.localScale.z;
	}
}
