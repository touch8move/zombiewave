using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Controller : MonoBehaviour {
	public GameObject Bullet;
	public GameObject BulletStartPoint;
	public GameObject ArrowStartPoint;
	public GameObject ShotPoint;
	public GameObject Ethan;

	private Vector3 firstpoint; //change type on Vector3
	private Vector3 secondpoint;
 	private float xAngle = 0.0f; //angle for axes x for rotation
	private float yAngle = 0.0f;
	private float xAngTemp = 0.0f; //temp variable for angle
	private float yAngTemp = 0.0f;
	public float rotationspeed;

	public Text PowerLabel;
	public Text XDegree;
	public Text YDegree;
	public Scrollbar PowerGuage;

	public GameObject Arrow;
	public GameObject Shooter;
	public float MaxPower;
	public float MinPower;
	public float powerTic;
	public GameObject Dot;

	//float accuracy = 90.0f;
	//GameObject[] Bullets;

	bool isBtnPressed;
	public float power;
	float Timedir;
	public GameObject Ao;

	float OnOffTimer;
	bool isUITouch;

	float gravity;
	float angle;
	//public BowScript bow;
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
		xAngle = 0.0f;
		yAngle = 0.0f;
	}

	public void ResetEthan()
	{
		Ethan.transform.position = new Vector3(0, 0.6f, 0);
		Ethan.transform.rotation = Quaternion.identity;
	}

	public void ShotArrow()
	{
		DrawAim();
		GameObject arrow = Instantiate(Arrow, BulletStartPoint.transform.position, Quaternion.Euler(yAngle, xAngle, 0)) as GameObject;
		GameObject _startPoint = Instantiate(ShotPoint);
		arrow.transform.SetParent(_startPoint.transform);
		//arrow.GetComponent<Arrow>().Shot(power, (360 - Camera.main.transform.eulerAngles.x)));

		arrow.GetComponent<Arrow>().Shot(power, angle);
	}

	void UpdateLabel()
	{
		XDegree.text = (Camera.main.transform.eulerAngles.y).ToString("F1");
		YDegree.text = (360-Camera.main.transform.eulerAngles.x).ToString("F1");
	}
	void Update()
	{
		UpdateLabel();
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
				firstpoint = touchPosition;
				//Debug.Log("FirstPoint: " + firstpoint);
				xAngTemp = xAngle;
				yAngTemp = yAngle;
				break;
			case TouchPhase.Moved:
				// TODO
				secondpoint = touchPosition;
				xAngle = xAngTemp + (secondpoint.x - firstpoint.x)  / Screen.width * rotationspeed;
				yAngle = yAngTemp - (secondpoint.y - firstpoint.y)  / Screen.height * rotationspeed;

				if (Mathf.Abs(Mathf.DeltaAngle(xAngle, 0)) <= 30)
				{
					Camera.main.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
					BulletStartPoint.transform.rotation = Quaternion.Euler(0, xAngle, 0.0f);
					ArrowStartPoint.transform.rotation = Quaternion.Euler(0, xAngle, 0.0f);
					ShotPoint.transform.rotation = Quaternion.Euler(0, xAngle, 0.0f);
					BowObject.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
				}

				break;
			case TouchPhase.Ended:
				// TODO
				//Debug.Log("Degree: " + (360-Camera.main.transform.eulerAngles.x));
				ShotArrow();
				break;
		}
	}

	public void IncreasePower()
	{
		//StartCoroutine(IEIncreasePower());
		//power = 1.5f;
	}

	public void releasePower()
	{
		isBtnPressed = false;
		//power = 2;
		//Debug.Log("Degree: " + Camera.main.transform.eulerAngles.x);
		ShotArrow();
		//power = 0;
	}

	IEnumerator IEIncreasePower()
	{
		isBtnPressed = true;
		while (isBtnPressed)
		{
			if (power < MaxPower)
				power += Time.deltaTime * powerTic;
			else
				power = MaxPower;
			yield return new WaitForEndOfFrame();
		}
	}

	void CreateLine(float angle)
	{
		Vector3 v1 = Vector3.zero;
		Timedir = 0;

		float da = angle*Mathf.Deg2Rad;
		//Debug.Log("Degree: " + angle + " Deg2Rad: " + da);
		while (Timedir < 100)
		{
			float realPower = 1;
			v1.z = realPower * Mathf.Cos(da) * Timedir;
			v1.y = realPower * Mathf.Sin(da) * Timedir - (9.8f / 60 * Mathf.Pow(Timedir, 2) / 2);
			Timedir += 0.3f;
			Instantiate(Ao, v1, Quaternion.identity);
		}
	}
	//void InitBulletLine()
	//{
	//	//GameObject
	//	Bullets = new GameObject[200];
	//	for (int i = 0; i < Bullets.Length; i++)
	//	{
	//		Bullets[i] = Instantiate(Dot) as GameObject;
	//		Bullets[i].transform.SetParent(BulletStartPoint.transform);
	//		Bullets[i].transform.localPosition = Vector3.zero;
	//	}
	//}
	//void DrawBulletLine(float _power, float _angleX)
	//{
	//	float daX = _angleX * Mathf.Deg2Rad;
	//	float timeValues = 0;
	//	for (int i = 0; i < Bullets.Length; i++)
	//	{
	//		Bullets[i].transform.localPosition = new Vector3(0, _power * Mathf.Sin(daX) * timeValues - (9.8f / 60 * Mathf.Pow(timeValues, 2) / 2), _power * Mathf.Cos(daX) * timeValues);
	//		timeValues += 0.2f;
	//	}
	//}

	void DrawAim()
	{
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
		Vector3 accu = Random.insideUnitSphere;
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			//if (hit.transform.gameObject.tag != "Target")
			//{
			Debug.Log("Target: " + hit.point + " Accu: " + accu);
				hit.point = hit.point + accu;
				float dx = hit.point.z - ArrowStartPoint.transform.position.z;
				float dy = hit.point.y - ArrowStartPoint.transform.position.y;

				float degreeU = 
					Mathf.Atan2(
						(power * power)
						 + Mathf.Sqrt(
						Mathf.Pow(power, 4) - gravity * (gravity * dx * dx + 2 * dy * power * power))
						, (gravity * dx));
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
				//DrawBulletLine(power, angle);

			//}
		}
		else {
			Debug.Log("Sky");
		}
	}

	public void OnChangeValue()
	{
		//PowerLabel.text = (PowerGuage.value * 100).ToString("F1")+" %" ;
		//power = MinPower + PowerGuage.value * (MaxPower - MinPower);
		//Debug.Log("Power:: " + (PowerGuage.value * 100).ToString("F1"));
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
