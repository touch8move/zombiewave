using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	EnemyHealth enemyHealth;
	public float MoveSpeed;
	public int Damage;
	Transform target;
	Animator ani;
	Controller con;
	UnityEngine.AI.NavMeshAgent nav;
	void Awake()
	{
		con = FindObjectOfType<Controller>();
		enemyHealth = GetComponent<EnemyHealth>(); 
		ani = GetComponent<Animator>();
		nav = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		target = con.TargetTransform;
	}

	void Start()
	{
		ani.SetTrigger("Move");
		//Debug.Log("MyPos:" + transform.position + "Dest:" + target.position);
		nav.SetDestination (target.position);
		//if (nav.pathStatus != UnityEngine.AI.NavMeshPathStatus.PathComplete)
		//{
		//	Debug.Log("Nav Error");
		//}
		//for (int i = 0; i < nav.path.corners.Length; i++)
		//{
		//	Debug.Log("Path:" + nav.path.corners[i]);
		//}
	}

	void Update()
	{
		if (CheckReached ()) {
			FindObjectOfType<Controller>().TakeDamage(Damage);
			enemyHealth.LineDeath();
		}

		if (!con.IsGameOn)
		{
			if (nav.isActiveAndEnabled)
				nav.enabled = false;
			//nav.Stop();
		}
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

	bool CheckReached(){
		if (enemyHealth.isAlive() && con.IsGameOn)
		{
			float dist;

			//-- If navMeshAgent is still looking for a path then use line test
			if (nav.pathPending)
			{
				dist = Vector3.Distance(transform.position, target.position);
			}
			else {
				dist = nav.remainingDistance;
			}

			if (dist < 3)
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