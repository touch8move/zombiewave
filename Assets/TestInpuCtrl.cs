using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInpuCtrl : MonoBehaviour {

	private Vector3 firstpoint; //change type on Vector3
	private Vector3 secondpoint;
	private float xAngle = 0.0f; //angle for axes x for rotation
	private float yAngle = 0.0f;
	private float xAngTemp = 0.0f; //temp variable for angle
	private float yAngTemp = 0.0f;
	public float rotationspeed;
	//Vector3 CharacterRotate;
	//Transform characterTranform;
	bool isUITouch;
	public Transform CameraJoint;
	//public CharacterScript characterscript;
	//public ClearScript clear;
	//public FailScript fail;
	//float xAngleLimit;
	//public float limitDegreeHorizontal;
	//public float limitDegreeVertical;
	//GameManager gm;
	// Use this for initialization
	void Awake()
	{
		//gm = FindObjectOfType<GameManager>();
	}
	void Start()
	{
		//characterTranform = characterscript.gameObject.transform;
		//CharacterRotate = characterTranform.eulerAngles;
	}

	void FixedUpdate()
	{
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
		else {
			HandleTouch(Input.touches[0].fingerId, (Input.touches[0].position), Input.touches[0].phase);
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
				xAngTemp = CameraJoint.transform.eulerAngles.y;
				yAngTemp = CameraJoint.transform.eulerAngles.x;
				break;
			case TouchPhase.Moved:
				// TODO
				secondpoint = touchPosition;

				Vector3 delta = secondpoint - firstpoint;

				xAngle = xAngTemp + Mathf.Atan2(delta.x, 600) * Mathf.Rad2Deg;
				yAngle = yAngTemp - Mathf.Atan2(delta.y, 600) * Mathf.Rad2Deg;

				//characterscript.gameObject.transform.rotation = Quaternion.Euler(characterTranform.eulerAngles.x, xAngle, 0);
				CameraJoint.localRotation = Quaternion.Euler(yAngle, xAngle, 0);
				//characterscript.UpdateCharacterDirection(-yAngle);
				break;
			case TouchPhase.Ended:
				// TODO
				//if (gm.phase == GameManager.GamePhase.Playing)
				//{
				//	characterscript.Shot(xAngle, yAngle);
				//}
				//else if (gm.phase == GameManager.GamePhase.Clear || gm.phase == GameManager.GamePhase.Fail)
				//{
				//	gm.Next();
				//}

				break;
		}
	}
}
