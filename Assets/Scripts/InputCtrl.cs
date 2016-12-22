using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCtrl : MonoBehaviour {

	private Vector3 firstpoint; //change type on Vector3
	private Vector3 secondpoint;
	private float xAngle = 0.0f; //angle for axes x for rotation
	private float yAngle = 0.0f;
	private float xFirstDeg = 0.0f; //temp variable for angle
	private float yFirstDeg = 0.0f;
	public float rotationspeed;
	Vector3 CharacterRotate;
	Transform characterTranform;
	bool isUITouch;
	public Transform CameraJoint;
	public CharacterScript characterscript;
	public ClearScript clear;
	public FailScript fail;
	float xAngleLimit;
	public float limitDegreeHorizontal;
	public float limitDegreeVertical;
	public Vector3 TouchPoint;
	GameManager gm;


	Vector3 mouselastPos;
	//Vector3 mouseDelta;
	float averageDist;
	// Use this for initialization
	void Awake()
	{
		gm = FindObjectOfType<GameManager>();
		averageDist = 100;
	}
	void Start()
	{
		characterTranform = characterscript.gameObject.transform;
		CharacterRotate = characterTranform.eulerAngles;
	}
	
	void Update()
	{
		//TouchPoint = Input.touches[0].position;
		//if (isUITouch == false)
		//{
		//	// Simulate touch events from mouse events
		//	if (Input.touchCount == 0)
		//	{
		//		if (Input.GetMouseButtonDown(0))
		//		{
		//			//Debug.Log("MousePoint:" + (Input.mousePosition));
		//			HandleTouch(10, (Input.mousePosition), TouchPhase.Began);
		//		}
		//		if (Input.GetMouseButton(0))
		//		{
		//			HandleTouch(10, (Input.mousePosition), TouchPhase.Moved);
		//		}
		//		if (Input.GetMouseButtonUp(0))
		//		{
		//			HandleTouch(10, (Input.mousePosition), TouchPhase.Ended);
		//		}
		//	}
		//	else {
		//		HandleTouch(Input.touches[0].fingerId, (Input.touches[0].position), Input.touches[0].phase);
		//	}
		//}
		Vector3 delta = Vector3.zero;
		//bool MouseOnClick = false;
		if (Application.isEditor)
		{
			//if (Input.GetMouseButtonDown(0))
			//{
				//Debug.Log("MousePoint:" + (Input.mousePosition));
				//HandleTouch(10, (Input.mousePosition), TouchPhase.Began);
			//}
			//if (Input.GetMouseButton(0))
			//{
				//HandleTouch(10, (Input.mousePosition), TouchPhase.Moved);
			//}
			if (Input.GetMouseButtonUp(0))
			{
				HandleTouch2();
			}
			delta = GetMousePoisionDelta();
		}
		if (Application.isPlaying)
		{
			if (Input.touchCount > 0)
			{
				delta = Input.touches[0].deltaPosition;
				if(Input.touches[0].phase == TouchPhase.Ended)
					HandleTouch2();
			}

		}
		//if (Mathf.DeltaAngle(characterTranform.eulerAngles.x, 0) > 30)
		//{
		//}
		//Debug.Log(Mathf.DeltaAngle(characterTranform.eulerAngles.y, 0));
		//if (Mathf.Abs(Mathf.DeltaAngle(characterTranform.eulerAngles.y, 0)) < 30)
		//{
			characterTranform.Rotate(0, Mathf.Atan2(delta.x, averageDist) * Mathf.Rad2Deg, 0);
		//}
		//if (Mathf.Abs(Mathf.DeltaAngle(CameraJoint.eulerAngles.x, 0)) < 30)
		//{
			CameraJoint.Rotate(-Mathf.Atan2(delta.y, averageDist) * Mathf.Rad2Deg, 0, 0);
		//}
			
			
		//characterTranform.rotation = Quaternion.Euler(characterTranform.eulerAngles.x, xAngle, 0);
		//CameraJoint.localRotation = Quaternion.Euler(yAngle, 0, 0);
	}

	Vector3 GetMousePoisionDelta()
	{
		Vector3 mouseDelta = Vector3.zero;
		if (Input.GetMouseButtonDown(0))
		{
			mouselastPos = Input.mousePosition;
		}
		else if (Input.GetMouseButton(0))
		{
			mouseDelta = Input.mousePosition - mouselastPos;

			// Do Stuff here

			//Debug.Log("delta X : " + delta.x);
			//Debug.Log("delta Y : " + delta.y);

			// End do stuff

			mouselastPos = Input.mousePosition;
		}
		return mouseDelta;
	}

	private void HandleTouch2()
	{
		// TODO
		if (gm.phase == GameManager.GamePhase.Playing)
		{
			characterscript.Shot(characterTranform.eulerAngles.y, CameraJoint.eulerAngles.x);
		}
		else if (gm.phase == GameManager.GamePhase.Clear || gm.phase == GameManager.GamePhase.Fail)
		{
			gm.Next();
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
				xFirstDeg = characterTranform.eulerAngles.y;
				yFirstDeg = CameraJoint.eulerAngles.x;
				//Debug.Log("xFirstDeg:" + xFirstDeg + " yFirstDeg:" + yFirstDeg);
				break;
			case TouchPhase.Moved:
				// TODO
				secondpoint = touchPosition;

				Vector3 delta = secondpoint - firstpoint;
				//xAngle = xFirstDeg + (secondpoint.x - firstpoint.x) / Screen.width * rotationspeed;
				//yAngle = yFirstDeg - (secondpoint.y - firstpoint.y) / Screen.height * rotationspeed;
				 xAngle = xFirstDeg+Mathf.Atan2(delta.x, 300) * Mathf.Rad2Deg;
				 yAngle = yFirstDeg-Mathf.Atan2(delta.y, 300) * Mathf.Rad2Deg;
				//Debug.Log("angleX:" + angleX + " angelY:" + angleY);

				float deltaAngleY = Mathf.DeltaAngle(yAngle, 0);
				if (deltaAngleY < -limitDegreeVertical)
				{
					yAngle = limitDegreeVertical;
				}

				if (deltaAngleY > limitDegreeVertical)
				{
					yAngle = -limitDegreeVertical;
				}

				float deltaAngle = Mathf.DeltaAngle(xAngle, CharacterRotate.y);

				if (deltaAngle > limitDegreeHorizontal)
				{
					xAngle = CharacterRotate.y - limitDegreeHorizontal;
				}

				if (deltaAngle < -limitDegreeHorizontal)
				{
					xAngle = CharacterRotate.y + limitDegreeHorizontal;
				}

				characterTranform.rotation = Quaternion.Euler(characterTranform.eulerAngles.x, xAngle, 0);
				CameraJoint.localRotation = Quaternion.Euler(yAngle, 0, 0);
				//characterscript.UpdateCharacterDirection(-yAngle);
				break;
			case TouchPhase.Ended:
				// TODO
				if (gm.phase == GameManager.GamePhase.Playing)
				{
					characterscript.Shot(xAngle, yAngle);
				}
				else if (gm.phase == GameManager.GamePhase.Clear || gm.phase == GameManager.GamePhase.Fail)
				{
					gm.Next();
				}

				break;
		}
	}
}
