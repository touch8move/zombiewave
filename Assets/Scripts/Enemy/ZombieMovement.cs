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
		ani.SetTrigger("Walk");
		//Debug.Log("MyPos:" + transform.position + "Dest:" + target.position);
		nav.SetDestination(target.transform.position);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Target"))
		{
			playerInRange = true;
			if (nav.isActiveAndEnabled)
			{
				//nav.enabled = false;
				nav.Stop();
			}
		}
	}

	//void OnTriggerExit(Collider other)
	//{
	//	if (other.gameObject.layer == LayerMask.NameToLayer("Target"))
	//	{
	//		playerInRange = false;
	//	}
	//}

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
