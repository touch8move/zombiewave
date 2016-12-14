using UnityEngine;
using System.Collections;

public class ArrowState : MonoBehaviour {
	public Arrow arrowCtrl;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//if (transform.position.z > 0)
		//{
			//arrowCtrl.RemoveParent(null);
			//Destroy(gameObject);
		//}
			//Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Hit Position: " + other.transform.position);

		if(other.gameObject.layer == LayerMask.NameToLayer("Shootable"))
		{
			Debug.Log(other.tag);
			int Damage = arrowCtrl.damage;
			if (other.tag == "Head")
			{
				Debug.Log("Head");
				Damage =Mathf.CeilToInt(Damage * 2.5f);
			}
			other.gameObject.GetComponentInParent<ZombieHealth>().TakeDamage(Damage, other.transform.position);
		}

		//if (other.gameObject.layer == LayerMask.NameToLayer ("Background")) {
		//	Debug.Log("Background Hit");

		//}
		arrowCtrl.RemoveParent(other);
		Destroy(gameObject);
	}
}
