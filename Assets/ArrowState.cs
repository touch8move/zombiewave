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
		if (other.CompareTag("Object"))
		{
			arrowCtrl.isMoving = false;
			//gameObject.transform.SetParent(null);
			arrowCtrl.RemoveParent();
		}
	}
}
