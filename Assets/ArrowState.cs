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
		//Debug.Log("Hit Position: " + other.transform.position);

		if(other.gameObject.layer == LayerMask.NameToLayer("Shootable"))
		{
			other.gameObject.GetComponent<EnemyHealth>().TakeDamage(arrowCtrl.damage, other.transform.position);
			arrowCtrl.RemoveParent (other);
			Destroy (gameObject);
		}

		if (other.gameObject.layer == LayerMask.NameToLayer ("Background")) {
			arrowCtrl.RemoveParent (other);
			Destroy (gameObject);
		}
	}
}
