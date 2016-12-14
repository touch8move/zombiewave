using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	void Awake()
	{
		anim = GetComponentInChildren<Animator>();
		//enemyAudio = GetComponent<AudioSource>();
		hitParticles = GetComponentInChildren<ParticleSystem>();
		//capsuleCollider = GetComponent <CapsuleCollider> ();
		colliders = GetComponentsInChildren<BoxCollider>();
		//zombieMovement = GetComponent<ZombieMovement>();
		//con = FindObjectOfType<Controller>();
		spawnManager = FindObjectOfType<SpawnManager>();
		nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
		currentHealth = startingHealth;
	}


	void Update()
	{
		if (isSinking)
		{
			transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
		}
	}


	public void TakeDamage(int amount, Vector3 hitPoint)
	{
		if (isDead)
			return;

		//enemyAudio.Play();

		currentHealth -= amount;

		hitParticles.transform.position = hitPoint;
		hitParticles.Play();

		if (currentHealth <= 0)
		{
			//Death();
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
