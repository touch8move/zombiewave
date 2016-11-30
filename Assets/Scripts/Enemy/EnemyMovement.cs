﻿using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	EnemyHealth enemyHealth;
	public float MoveSpeed;
	public int Damage;
	Animator ani;
	Controller con;
	void Awake()
	{
		con = FindObjectOfType<Controller>();
		enemyHealth = GetComponent<EnemyHealth>(); 
		ani = GetComponent<Animator>();;
	}
	void Start()
	{
		ani.SetTrigger("Move");
	}
	void Update()
	{
		if (con.PlayerHP > 0)
		{
			if (enemyHealth.currentHealth > 0)
			{
				transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
				if (transform.position.x > 15)
				{
					enemyHealth.currentHealth = 0;
					FindObjectOfType<Controller>().PlayerHP -= Damage;
					enemyHealth.Death();
				}

			}
		}
	}
}