using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Controller : MonoBehaviour {
	//public GameObject Bullet;
	//public GameObject BulletStartPoint;
	public GameObject ArrowStartPoint;
	public GameObject ShotPoint;

	public GameObject[] Enemys;

	float bX;
	float bY;
	public float rotationspeed;

	public GameObject Arrow;

	public RectTransform aimPoint;

	public float power;
	float Timedir;

	float OnOffTimer;
	bool isUITouch;

	float gravity;
	float angle;
	float Xangle;
	float GenTime;
	float GenCurrentTime;
	public GameObject BowObject;

	int playerHp;
	public int PlayerHP
	{
		get
		{
			return playerHp;
		}
		set
		{
			playerHp = value;
			if (playerHp < 0)
				playerHp = 0;
		}
	}
	public int Point;
	public Camera camera;
	bool isGameOn;

	public Text HPLabel;
	public Text PointLabel;

	void Awake()
	{
		power = 3f;
		gravity = 9.8f / 60;
	}
	void Start()
	{
		GetGenTime();
		//isGameOn = true;
		GameStart();
		playerHp = 100;
	}
	public void GameStop()
	{
		isGameOn = false;
	}

	public void GameStart()
	{
		isGameOn = true;
	}
	public void ShotArrow()
	{
		Aim();
		ShotPoint.transform.rotation = Quaternion.Euler(0, Xangle, 0);
		GameObject arrow = Instantiate(Arrow, ArrowStartPoint.transform.position, Quaternion.Euler(angle, Xangle, 0)) as GameObject;
		GameObject _startPoint = Instantiate(ShotPoint);
		arrow.transform.SetParent(_startPoint.transform);
		arrow.GetComponent<Arrow>().Shot(power, angle);
	}
	void GetGenTime()
	{
		GenTime = Random.Range(0.5f, 2.0f);
	}
	void UpdateLabel()
	{
		HPLabel.text = "HP : "+PlayerHP.ToString("N0");
		PointLabel.text = "Point : " + Point.ToString("N0");
	}
	void CheckHP()
	{
		if (PlayerHP <= 0)
		{
			isGameOn = false;
		}
	}
	void Update()
	{
		CheckHP();
		UpdateLabel();
		if (isGameOn)
		{
			GenCurrentTime += Time.deltaTime;
			if (GenCurrentTime > GenTime)
			{
				GetGenTime();
				GenCurrentTime = 0;
				GenerateEnemy();
			}
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

				aimPoint.position += new Vector3(touchPosition.x-bX, touchPosition.y-bY, 0);
				//if (camera.fieldOfView > 60)
				//{
				//	camera.fieldOfView -= 0.5f;
				//}
				bX = touchPosition.x;
				bY = touchPosition.y;
				break;
			case TouchPhase.Ended:
				// TODO
				//Debug.Log("Degree: " + (360-camera.transform.eulerAngles.x));
				ShotArrow();
				//camera.fieldOfView = 100;
				break;
		}
	}

	void Aim()
	{
		Ray ray = camera.ScreenPointToRay(aimPoint.position);
		//Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		Debug.DrawRay(ray.origin, ray.direction * 20f, Color.red, 5f);
		Debug.Log(ray);

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

	void GenerateEnemy()
	{
		int enemyIndex = Random.Range(0, Enemys.Length);
		GameObject enemy = Instantiate(Enemys[enemyIndex], new Vector3(-18, 0, Random.Range(-10.0f,0.0f)), Quaternion.Euler(0,90,0)) as GameObject;
		Debug.Log("Gen Enemy " + enemyIndex);
	}
}
