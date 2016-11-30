using UnityEngine;
using System.Collections;

public class ArrowState : MonoBehaviour {
	public Arrow arrowCtrl;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log("Tag:" + other.tag);
		if (other.gameObject.layer == LayerMask.NameToLayer("ObjectLayer"))
		{
			arrowCtrl.RemoveParent(other);
		}
		if (other.CompareTag("Target"))
		{
			other.gameObject.GetComponent<EnemyHealth>().TakeDamage(arrowCtrl.damage, other.transform.position);
		}
	}
}
