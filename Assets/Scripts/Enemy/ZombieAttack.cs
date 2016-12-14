using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour {

	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 10;

	Animator anim;
	GameObject target;
	TargetScript targetScript;
	//PlayerHealth playerHealth;
	ZombieHealth zombieHealth;
	ZombieMovement zombieMovement;
	float timer;
	GameManager gm;


	void Awake()
	{
		gm = FindObjectOfType<GameManager>();
		target = gm.target;
		targetScript = target.GetComponent<TargetScript>();
		//target = gm.Target;
		//playerHealth = player.GetComponent<PlayerHealth>();
		zombieMovement = GetComponent<ZombieMovement>();
		zombieHealth = GetComponent<ZombieHealth>();
		anim = GetComponentInChildren<Animator>();
	}

	void Update()
	{
		timer += Time.deltaTime;

		if (timer >= timeBetweenAttacks && zombieMovement.playerInRange && zombieHealth.currentHealth > 0)
		{
			Attack();
		}

		if (targetScript.TargetCurrentHealth <= 0)
		{
			//anim.SetTrigger("PlayerDead");
			// Game End
			Debug.Log("Game End");
		}
	}


	void Attack()
	{
		timer = 0f;

		if (targetScript.TargetCurrentHealth > 0)
		{
			targetScript.TakeDamage(attackDamage);
		}
	}
}
