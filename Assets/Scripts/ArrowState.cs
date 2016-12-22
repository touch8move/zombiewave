using UnityEngine;
using System.Collections;

public class ArrowState : MonoBehaviour {
	public Arrow arrowCtrl;
	int cnt;
	CapsuleCollider cc;

	void Awake()
	{
		cc = GetComponent<CapsuleCollider>();
	}
	// Use this for initialization
	void Start () {
		cnt = 0;
	}
	void Update()
	{
		
	}
	//void OnTriggerEnter(Collider other)
	//{
	//	//Debug.Log("otherName:" + other.name);
	//	//cc.isTrigger = 
	//	cc.enabled = false;

	//	if(other.gameObject.layer == LayerMask.NameToLayer("Shootable"))
	//	{
	//		//Debug.Log(other.tag);
	//		int Damage = arrowCtrl.damage;
	//		bool isCritical = false;
	//		if (other.tag == "Head")
	//		{
	//			Debug.Log("Head");
	//			Damage =Mathf.CeilToInt(Damage * 2.5f);
	//			isCritical = true;
	//		}
	//		//other.
	//		//other.attachedRigidbody.
	//		//other.
	//		other.gameObject.GetComponentInParent<ZombieHealth>().TakeDamage(isCritical, Damage, other.transform.position);
	//	}
	//	arrowCtrl.RemoveParent();
	//}
	void OnCollisionEnter(Collision collision)
	{
		if (cnt == 0)
		{
			arrowCtrl.isMoving = false;
			cc.enabled = false;
			if (collision.gameObject.layer == LayerMask.NameToLayer("Shootable"))
			{

				int Damage = arrowCtrl.damage;
				bool isCritical = false;
				if (collision.collider.tag == "Head")
				{
					//Debug.Log("Head");-
					Damage = Mathf.CeilToInt(Damage * 2.5f);
					isCritical = true;
				}
				collision.gameObject.GetComponentInParent<ZombieHealth>().TakeDamage(isCritical, Damage, collision.contacts[0].point);
				arrowCtrl.RemoveParent(collision.gameObject);
			}
			else if (collision.gameObject.layer == LayerMask.NameToLayer("Target"))
			{
				Debug.Log("ArrowState Target");

				collision.gameObject.GetComponent<TargetScript>().TakeDamage(arrowCtrl.damage);
				arrowCtrl.RemoveParent(collision.gameObject);
			}
		}
		cnt++;
		//Debug.Log(collision.gameObject.name);
	}
}
