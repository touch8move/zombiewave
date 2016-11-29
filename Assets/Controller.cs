using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Controller : MonoBehaviour {
	public GameObject Bullet;
	public GameObject BulletStartPoint;
	public GameObject ArrowStartPoint;
	public GameObject ShotPoint;

	//private Vector3 firstpoint; //change type on Vector3
	//private Vector3 secondpoint;
 	//private float xAngle = 0.0f; //angle for axes x for rotation
	//private float yAngle = 0.0f;
	//private float xAngTemp = 0.0f; //temp variable for angle
	//private float yAngTemp = 0.0f;

	public GameObject direction;
	float bX;
	float bY;
	public float rotationspeed;

	public Text PowerLabel;
	public Text XDegree;
	public Text YDegree;
	public Scrollbar PowerGuage;

	public GameObject Arrow;

	public float MaxPower;
	public float MinPower;
	public float powerTic;
	public GameObject Dot;

	public RectTransform aimPoint;
	//Vector3 BeforeAimPoint;
	public GameObject Origin;
	public GameObject Direction;

	bool isBtnPressed;
	public float power;
	float Timedir;
	public GameObject Ao;

	float OnOffTimer;
	bool isUITouch;

	float gravity;
	float angle;
	float Xangle;

	public GameObject BowObject;

	float reloadTime;

	void Awake()
	{
		reloadTime = 2.0f;
		power = 3f;
		gravity = 9.8f / 60;
	}
	void Start()
	{
		//xAngle = 0.0f;
		//yAngle = 0.0f;
	}

	public void ShotArrow()
	{
		Aim();
		ShotPoint.transform.rotation = Quaternion.Euler(0, Xangle, 0);
		GameObject arrow = Instantiate(Arrow, BulletStartPoint.transform.position, Quaternion.Euler(angle, Xangle, 0)) as GameObject;
		GameObject _startPoint = Instantiate(ShotPoint);
		arrow.transform.SetParent(_startPoint.transform);
		arrow.GetComponent<Arrow>().Shot(power, angle);
	}

	void Update()
	{
		//UpdateLabel();
		//DrawBulletLine(power, 360 - Camera.main.transform.eulerAngles.x);

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
				//firstpoint = touchPosition;
				//Debug.Log("FirstPoint: " + firstpoint);
				//xAngTemp = xAngle;
				//yAngTemp = yAngle;
				//BeforeAimPoint = aimPoint.position;
				bX = touchPosition.x;
				bY = touchPosition.y;
				break;
			case TouchPhase.Moved:
				// TODO
				//secondpoint = touchPosition;

				//float dx = (secondpoint.x - firstpoint.x);
				//float dy = (secondpoint.y - firstpoint.y);
				//Debug.Log(bX + " " + bY);
				//aimPoint.position = BeforeAimPoint + (new Vector3(dx, dy, 0));
				aimPoint.position += new Vector3(touchPosition.x-bX, touchPosition.y-bY, 0);
				//if (Camera.main.fieldOfView > 60)
				//{
				//	Camera.main.fieldOfView -= 0.5f;
				//}
				bX = touchPosition.x;
				bY = touchPosition.y;
				break;
			case TouchPhase.Ended:
				// TODO
				//Debug.Log("Degree: " + (360-Camera.main.transform.eulerAngles.x));
				ShotArrow();
				//Camera.main.fieldOfView = 100;
				break;
		}
	}

	void Aim()
	{
		Ray ray = Camera.main.ScreenPointToRay(aimPoint.position);
		Origin.transform.position = ray.origin;
		Direction.transform.position = ray.direction;
		Debug.Log(ray+" ");
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			Debug.Log(hit.point);
			float deg = Mathf.Atan2(hit.point.x - ShotPoint.transform.position.x, hit.point.z-ShotPoint.transform.position.z) * Mathf.Rad2Deg;
			Xangle = deg;
			Debug.Log("Deg: " + Xangle);
			float dx = hit.point.z - ArrowStartPoint.transform.position.z;
			float dy = hit.point.y - ArrowStartPoint.transform.position.y;
			//float degreeU = 
			//	Mathf.Atan2(
			//		(power * power)
			//		 + Mathf.Sqrt(
			//		Mathf.Pow(power, 4) - gravity * (gravity * dx * dx + 2 * dy * power * power))
			//		, (gravity * dx));
			float degreeD =
				Mathf.Atan2(
					(power * power)
					 - Mathf.Sqrt(
						 Mathf.Pow(power, 4) - gravity * (gravity * dx * dx + 2 * dy * power * power)) , (gravity * dx));
			angle = degreeD * Mathf.Rad2Deg;
			if (System.Single.IsNaN(angle))
			{
				angle = 45;
			}
		}
		else {
			Debug.Log("Sky");
		}
	}

	public void UITouchOn()
	{
		isUITouch = true;
	}

	public void UITouchOff()
	{
		isUITouch = false;
	}
}
