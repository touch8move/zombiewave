using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	EnemyHealth enemyHealth;
	public float MoveSpeed;
	public int Damage;
	Animator ani;
	void Awake()
	{
		enemyHealth = GetComponent<EnemyHealth>(); 
		ani = GetComponent<Animator>();;
	}
	void Start()
	{
		ani.SetTrigger("Move");
	}
	void Update()
	{
		if (enemyHealth.currentHealth > 0)
		{
			transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
			if (transform.position.x > 18)
			{
				enemyHealth.currentHealth = 0;
				FindObjectOfType<Controller>().PlayerHP -= Damage;
				enemyHealth.Death();
			}
		}
	}
}