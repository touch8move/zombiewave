using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour {

	ZombieHealth zombieHealth;
	//ZombieAttack zombieAttack;
	public float MoveSpeed;
	//public int Damage;
	GameObject target;
	Animator ani;
	//Controller con;
	UnityEngine.AI.NavMeshAgent nav;
	//public bool isMelee;
	GameManager gm;
	//TargetScript targetScript;
	public bool playerInRange;
	void Awake()
	{
		//zombieAttack = GetComponent<ZombieAttack>();
		gm = FindObjectOfType<GameManager>();
		target = gm.target;
		//targetScript = target.GetComponent<TargetScript>();
		zombieHealth = GetComponent<ZombieHealth>();
		ani = GetComponentInChildren<Animator>();
		nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
		//target = con.TargetTransform;
		//target = gm.Target;
	}

	void Start()
	{
		//if (isMelee)
		//{
			ani.SetTrigger("Walk");
			//Debug.Log("MyPos:" + transform.position + "Dest:" + target.position);
			nav.SetDestination(target.transform.position);
		//}
		//if (nav.pathStatus != UnityEngine.AI.NavMeshPathStatus.PathComplete)
		//{
		//	Debug.Log("Nav Error");
		//}
		//for (int i = 0; i < nav.path.corners.Length; i++)
		//{
		//	Debug.Log("Path:" + nav.path.corners[i]);
		//}
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == target)
		{
			playerInRange = true;
			if (nav.isActiveAndEnabled)
			{
				//nav.enabled = false;
				nav.Stop();
			}
		}
	}


	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == target)
		{
			playerInRange = false;
		}
	}
	void Update()
	{
		//if (isMelee)
		//{
			//if (CheckReached())
			//{
			//	if (nav.isActiveAndEnabled)
			//		nav.enabled = false;
			//}
			//if (!con.IsGameOn)
			//{

				
			//}
		//}


		//		if (con.PlayerHP > 0)
		//		{
		//			if (enemyHealth.isAlive())
		//			{

		//				//transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
		////				if (transform.position.z < -10)
		////				{
		////					FindObjectOfType<Controller>().TakeDamage(Damage);
		////					enemyHealth.LineDeath();
		////				}
		//			}
		//		}
	}

	bool CheckReached()
	{
		if (zombieHealth.isAlive())
		{
			float dist;

			//-- If navMeshAgent is still looking for a path then use line test
			if (nav.pathPending)
			{
				dist = Vector3.Distance(transform.position, target.transform.position);
			}
			else {
				dist = nav.remainingDistance;
			}

			if (dist < 1)
			{
				//Debug.Log("Distance:" + dist);
				return true;
			}
			else {
				return false;
			}
		}
		else {
			return false;
		}
	}
}
