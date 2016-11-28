using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
	public float angle;

	public float Power;
	public GameObject ArrowObject;
	float Timedir;
	float gravity;
	public bool isMoving;
	float tmpDx;
	float tmpDy;
	// Use this for initialization
	void Awake()
	{
		gravity = 9.8f / 60;
	}

	void Update()
	{
		if (isMoving)
		{
			Timedir += Time.deltaTime * 10;
			// x = (V0cos(angle))t
			// y = (V0sin(angle))t - 1/2gt^2
			transform.localPosition = new Vector3(0,
			                                      Power * Mathf.Sin(angle * Mathf.Deg2Rad) * Timedir - (gravity * Timedir * Timedir / 2), 
			                                                       (Power * Mathf.Cos(angle * Mathf.Deg2Rad)) * Timedir);
			float dx = transform.localPosition.z - tmpDx;
			float dy = transform.localPosition.y - tmpDy;
			//Debug.Log("Atan2: "+(Mathf.Atan2(dy, dx)*Mathf.Rad2Deg));
			transform.localRotation = Quaternion.Euler(-(Mathf.Atan2(dy, dx) * Mathf.Rad2Deg),0,0);
			tmpDx = transform.localPosition.z;
			tmpDy = transform.localPosition.y;
		}
	}

	//void OnTriggerEnter(Collider other)
	//{
	//	Debug.Log("Tag:" + other.tag);
	//	if (other.CompareTag("Object"))
	//	{
	//		isMoving = false;
	//	}
	//}

	//void OnCollisionEnter(Collision other)
	//{
	//	if (other.collider.CompareTag("Object"))
	//	{
	//		Debug.Log("Stop");
	//		isMoving = false;
	//	}
	//}

	public void Shot(float firstSpeed, float firstAngle)
	{
		Power = firstSpeed;
		angle = firstAngle;
		//angleX = firstAngleX;
		isMoving = true;
		//Debug.Log("Degree: " + firstAngle + " Power: " + firstSpeed);
	}

	public void RemoveParent()
	{
		GameObject parent = transform.parent.gameObject;
		transform.SetParent(null);
		Destroy(parent);
	}
}
