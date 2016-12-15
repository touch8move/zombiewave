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
		if(other.gameObject.layer == LayerMask.NameToLayer("Shootable"))
		{
			Debug.Log(other.tag);
			int Damage = arrowCtrl.damage;
			bool isCritical = false;
			if (other.tag == "Head")
			{
				Debug.Log("Head");
				Damage =Mathf.CeilToInt(Damage * 2.5f);
				isCritical = true;
			}
			other.gameObject.GetComponentInParent<ZombieHealth>().TakeDamage(isCritical, Damage, other.transform.position);
		}
		arrowCtrl.RemoveParent(other);
		Destroy(gameObject);
	}
}
