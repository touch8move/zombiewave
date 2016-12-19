using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieHealth : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	public float sinkSpeed = 2.5f;
	public int scoreValue = 10;
	ZombieMovement zombieMovement;
	Animator anim;
	//AudioSource enemyAudio;
	ParticleSystem hitParticles;
	BoxCollider[] colliders;
	bool isDead;
	bool isSinking;
	SpawnManager spawnManager;
	UnityEngine.AI.NavMeshAgent nav;
	GameManager gm;
	public Slider HPGuage;
	public Text DamageText;
	//Slider HPGuage;
	RectTransform rect;
	void Awake()
	{
		anim = GetComponentInChildren<Animator>();
		//enemyAudio = GetComponent<AudioSource>();
		hitParticles = GetComponentInChildren<ParticleSystem>();
		gm = FindObjectOfType<GameManager>();
		colliders = GetComponentsInChildren<BoxCollider>();
		spawnManager = FindObjectOfType<SpawnManager>();
		nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
		currentHealth = startingHealth;
		//HPGuage = Instantiate(HPGuageprefab);
		//Canvas enemyCanvas = transform.FindChild("ECanvas").gameObject.GetComponent<Canvas>();
		//HPGuage.transform.SetParent(enemyCanvas.transform);
	}
	void Start()
	{
		HPGuage.minValue = 0;
		HPGuage.maxValue = startingHealth;
		rect = HPGuage.GetComponent<RectTransform>();
	}

	void Update()
	{
		UpdateHPGuagePosition();
		if (isSinking)
		{
			transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
		}
	}

	void UpdateHPGuagePosition()
	{
		rect.position = Camera.main.WorldToScreenPoint(transform.position+Vector3.up*3);
	}


	public void TakeDamage(bool isCritical, int amount, Vector3 hitPoint)
	{
		if (isDead)
			return;

		//enemyAudio.Play();

		currentHealth -= amount;

		hitParticles.transform.position = hitPoint;
		hitParticles.Play();
		HPGuage.value = currentHealth;
		DamageText.text = amount.ToString("N0");
		if (isCritical)
		{
			DamageText.GetComponent<Animator>().SetTrigger("Critical");
		}
		else {
			DamageText.GetComponent<Animator>().SetTrigger("Hit");
		}
		if (currentHealth <= 0)
		{
			Killed();
		}
	}

	void Death()
	{
		isDead = true;
		nav.Stop();
		//capsuleCollider.isTrigger = true;
		for (int i = 0; i < colliders.Length; i++)
		{
			colliders[i].isTrigger = true;
		}
		anim.SetTrigger("Dead");
	}
	void Killed()
	{
		Death();
		//Point
		gm.KillZombie();
		//enemyAudio.clip = deathClip;
		//enemyAudio.Play();
		//con.Point += scoreValue;
		//StartSinking();
		Invoke("StartSinking", 2f);
	}

	public void StartSinking()
	{
		GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
		isSinking = true;

		//Destroy(gameObject, 2f);
		//Invoke
		//Invoke();
		Invoke("ReturnZombie", 1);
	}

	void ReturnZombie()
	{
		//
		gameObject.transform.position = spawnManager.OriginspawnPoint.position;
		gameObject.SetActive(false);
	}

	public bool isAlive()
	{
		return !isDead;
	}
}
