using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleArrow : MonoBehaviour {
	public float angle;

	float Power;
	//public GameObject ArrowObject;
	float Timedir;
	float gravity;
	public bool isMoving;
	public int damage;
	float tmpDx;
	float tmpDy;
	Vector3 direction;
	new Collider collider;
	// Use this for initialization
	void Awake()
	{
		gravity = 9.8f / 60;
	}
	
	void Update()
	{
		if (isMoving)
		{
			Timedir += Time.deltaTime;
			// x = (V0cos(angle))t
			// y = (V0sin(angle))t - 1/2gt^2
			transform.localPosition = new Vector3(0,
												  Power * Mathf.Sin(angle * Mathf.Deg2Rad) * Timedir - (gravity * Timedir * Timedir / 2),
																   (Power * Mathf.Cos(angle * Mathf.Deg2Rad)) * Timedir);
			float dx = transform.localPosition.z - tmpDx;
			float dy = transform.localPosition.y - tmpDy;

			//transform.localRotation = Quaternion.Euler(-(Mathf.Atan2(dy, dx) * Mathf.Rad2Deg), 0, 0);
			tmpDx = transform.localPosition.z;
			tmpDy = transform.localPosition.y;
		}
	}

	public void Shot(float firstSpeed, float firstAngle)
	{
		Debug.Log("Shot");
		Power = firstSpeed;
		angle = firstAngle;

		isMoving = true;

	}

	void OnCollisionEnter(Collision collision)
	{
		isMoving = false;
		transform.SetParent(collision.gameObject.transform);
		collision.gameObject.GetComponentInParent<TitleWalkerScript>().Hit();
	}

}
