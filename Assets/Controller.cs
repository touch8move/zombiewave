using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Controller : MonoBehaviour {


	public int startingHealth = 100;
	int currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	public AudioClip deathClip;
	public float flashSpeed = 5f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
	bool damaged;

	public GameOverScript GameOver;

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

	//public Image TakeDamage;
	//public Image damage;

	int playerHp;
	public int PlayerHP
	{
		get
		{
			return currentHealth;
		}
		set
		{
			currentHealth = value;
			if (currentHealth < 0)
				currentHealth = 0;
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
		PlayerHP = startingHealth;
	}
	void Start()
	{
		GetGenTime();
		//isGameOn = true;
		GameStart();
		healthSlider.maxValue = PlayerHP;
		healthSlider.value = PlayerHP;
		//playerHp = 100;
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
		GenTime = Random.Range(1.0f, 2.0f);
	}
	void UpdateLabel()
	{
		HPLabel.text = "HP : "+PlayerHP.ToString("N0");
		PointLabel.text = "Point : " + Point.ToString("N0");
	}

	public void TakeDamage(int amount)
	{
		damaged = true;

		PlayerHP -= amount;

		healthSlider.value = PlayerHP;

		//playerAudio.Play();

		if (PlayerHP <= 0 )
		{
			isGameOn = false;
			int highScore = PlayerPrefs.GetInt("HighScore", 0);
			if (highScore < Point)
			{
				PlayerPrefs.SetInt("HighScore", Point);
			}
			GameOver.Show();
		}
	}
	void Update()
	{

		if (damaged)
		{
			damageImage.color = flashColour;
		}
		else
		{
			damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;

		//CheckHP();
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
				bX = touchPosition.x;
				bY = touchPosition.y;
				break;
			case TouchPhase.Moved:
				// TODO

				aimPoint.position += new Vector3(touchPosition.x-bX, touchPosition.y-bY, 0);

				bX = touchPosition.x;
				bY = touchPosition.y;
				break;
			case TouchPhase.Ended:
				// TODO
				ShotArrow();
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
		GameObject enemy = Instantiate(Enemys[enemyIndex], new Vector3(-18, 0, Random.Range(-5.0f,5.0f)), Quaternion.Euler(0,90,0)) as GameObject;
		Debug.Log("Gen Enemy " + enemyIndex);
	}

	public void Restart()
	{
		SceneManager.LoadScene(0);
	}
}
