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

	public Transform TargetTransform;

	bool damaged;

	public GameOverScript GameOver;

	//public GameObject ArrowStartPoint;
	public GameObject ShotPoint;

	public GameObject[] Enemys;

	public Transform[] EnemyGenPoints;
	float bX;
	float bY;
	//public float rotationspeed;

	public GameObject Arrow;

	public RectTransform aimPoint;

	public float power;
	float Timedir;

	//float OnOffTimer;
	bool isUITouch;

	float gravity;
	float angle;
	float Xangle;

	float GenTime;
	float GenCurrentTime;
	public bool IsGameOn
	{
		get
		{
			return isGameOn;
		}
	}

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
	new public Camera camera;
	bool isGameOn;

	public Text HPLabel;
	public Text PointLabel;

	// reload
	bool isReload;
	public float reloadTime;
	public Slider ReloadSlider;
	public Text BulletText;
	public int BulletCount;
	int currentBulletCount;

	void Awake()
	{
		power = 3f;
		gravity = 9.8f / 60;
		PlayerHP = startingHealth;

		//GenAirEnemyTime = 5;
	}
	void Start()
	{
		GetGenTime();
		//isGameOn = true;
		GameStart();
		healthSlider.maxValue = PlayerHP;
		healthSlider.value = PlayerHP;

		ReloadSlider.minValue = 0;
		ReloadSlider.maxValue = reloadTime;
		currentBulletCount = BulletCount;
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
		if (!isReload)
		{
			Aim();
			ShotPoint.transform.rotation = Quaternion.Euler(0, Xangle, 0);
			GameObject arrow = Instantiate(Arrow, ShotPoint.transform.position, Quaternion.Euler(angle, Xangle, 0)) as GameObject;
			GameObject _startPoint = Instantiate(ShotPoint);
			arrow.transform.SetParent(_startPoint.transform);
			arrow.GetComponent<Arrow>().Shot(power, angle);

			useBullet();
		}
	}

	void useBullet()
	{
		currentBulletCount--;
		BulletText.text = currentBulletCount.ToString();
		if (currentBulletCount == 0)
			reload();
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
//			GenCurrentTime += Time.deltaTime;
//			if (GenCurrentTime > GenTime)
//			{
//				GetGenTime();
//				GenCurrentTime = 0;
//				GenerateEnemy();
//			}
			//GenAirEnemyCurrentTime += Time.deltaTime;
			//if (GenAirEnemyTime < GenAirEnemyCurrentTime)
			//{
			//	GenAirEnemyCurrentTime = 0;
			//	int enemyIndex = Random.Range(0, Enemys.Length);
			//	GameObject enemy = Instantiate(Enemys[enemyIndex], airPosition.position, Quaternion.Euler(0, 180, 0)) as GameObject;
			//	Debug.Log("Gen Enemy " + enemyIndex);
			//}

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
		//Debug.DrawRay(ray.origin, ray.direction * 20f, Color.red, 5f);
		//Debug.Log(ray);

		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 500.0f, 1 << LayerMask.NameToLayer("Background")))
		{
			//Debug.Log("Hit: "+hit.point);
			float deg = Mathf.Atan2(hit.point.x - ShotPoint.transform.position.x, hit.point.z-ShotPoint.transform.position.z) * Mathf.Rad2Deg;
			Xangle = deg;
			//Debug.Log("Deg: " + Xangle);
			//float dx = hit.point.z - ShotPoint.transform.position.z;
			float dx = Vector3.Distance(new Vector3(hit.point.x, 0, hit.point.z), new Vector3(ShotPoint.transform.position.x, 0,ShotPoint.transform.position.z));
			float dy = hit.point.y - ShotPoint.transform.position.y;

			// Degree > 45
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
		GameObject enemy = Instantiate(Enemys[enemyIndex], new Vector3(Random.Range(-10,10), 0, 20), Quaternion.Euler(0,180,0)) as GameObject;
//		Debug.Log("Gen Enemy " + enemyIndex);
	}

	public void Restart()
	{
		SceneManager.LoadScene(0);
	}

	public void reload()
	{
		if (!isReload)
		{
			StartCoroutine(IEReload());
		}
	}

	IEnumerator IEReload()
	{
		isReload = true;
		float TimeTic = 0.02f;
		ReloadSlider.enabled = true;
		while (ReloadSlider.value < ReloadSlider.maxValue)
		{
			ReloadSlider.value += TimeTic;
			yield return new WaitForSeconds(TimeTic);
		}
		//ReloadSlider.enabled = false;
		isReload = false;
		currentBulletCount = BulletCount;
		BulletText.text = currentBulletCount.ToString();
		ReloadSlider.value = 0;
	}
}
