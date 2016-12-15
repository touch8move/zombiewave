using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCtrl : MonoBehaviour {

	private Vector3 firstpoint; //change type on Vector3
	private Vector3 secondpoint;
	private float xAngle = 0.0f; //angle for axes x for rotation
	private float yAngle = 0.0f;
	private float xAngTemp = 0.0f; //temp variable for angle
	private float yAngTemp = 0.0f;
	public float rotationspeed;
	Vector3 CharacterRotate;
	Transform characterTranform;
	bool isUITouch;
	public Transform CameraJoint;
	public CharacterScript characterscript;

	float xAngleLimit;
	public float limitDegreeHorizontal;
	public float limitDegreeVertical;
	// Use this for initialization
	void Awake()
	{
		
	}
	void Start()
	{
		characterTranform = characterscript.gameObject.transform;
		CharacterRotate = characterTranform.eulerAngles;
		//Debug.Log("Character:" + characterTranform.eulerAngles);
	}
	
	void LateUpdate()
	{
		if (isUITouch == false)
		{
			foreach (Touch touch in Input.touches)
			{
				HandleTouch(touch.fingerId, (touch.position), touch.phase);
			}

			// Simulate touch events from mouse events
			if (Input.touchCount == 0)
			{
				if (Input.GetMouseButtonDown(0))
				{
					//Debug.Log("MousePoint:" + (Input.mousePosition));
					HandleTouch(10, (Input.mousePosition), TouchPhase.Began);
				}
				if (Input.GetMouseButton(0))
				{
					HandleTouch(10, (Input.mousePosition), TouchPhase.Moved);
				}
				if (Input.GetMouseButtonUp(0))
				{
					HandleTouch(10, (Input.mousePosition), TouchPhase.Ended);
				}
			}
		}
	}

	private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase)
	{
		switch (touchPhase)
		{
			case TouchPhase.Began:
				// TODO
				firstpoint = touchPosition;
				//Debug.Log("FirstPoint: " + firstpoint);
				xAngTemp = characterTranform.eulerAngles.y;
				yAngTemp = Camera.main.transform.eulerAngles.x;
				break;
			case TouchPhase.Moved:
				// TODO
				secondpoint = touchPosition;
				xAngle = xAngTemp + (secondpoint.x - firstpoint.x) / Screen.width * rotationspeed;
				yAngle = yAngTemp - (secondpoint.y - firstpoint.y) / Screen.height * rotationspeed;


				//Debug.Log("yAngle: " + yAngle);
				if (Mathf.DeltaAngle(yAngle, 0) < -limitDegreeVertical)
				{
					yAngle = limitDegreeVertical;
				}

				if (Mathf.DeltaAngle(yAngle, 0) > limitDegreeVertical)
				{
					yAngle = -limitDegreeVertical;
				}

				float deltaAngle = Mathf.DeltaAngle(xAngle, CharacterRotate.y);
				//Debug.Log("DeltaAngle:" + deltaAngle);
				if (deltaAngle > limitDegreeHorizontal)
				{
					//xAngle = characterTranform.eulerAngles.y-30;
					xAngle = CharacterRotate.y - limitDegreeHorizontal;
				}

				if (deltaAngle < -limitDegreeHorizontal)
				{
					//xAngle = characterTranform.eulerAngles.y + 30;
					xAngle = CharacterRotate.y + limitDegreeHorizontal;
				}


				//Camera.main.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
				characterscript.gameObject.transform.rotation = 
					Quaternion.Euler(characterTranform.eulerAngles.x, xAngle, 0);
				CameraJoint.localRotation = Quaternion.Euler(yAngle, 0, 0);
				characterscript.UpdateCharacterDirection(-yAngle);
				break;
			case TouchPhase.Ended:
				// TODO
				characterscript.Shot(xAngle, yAngle);
				break;
		}
	}
}
