using UnityEngine;
using System.Collections;

public class ObjectScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//void OnTriggerEnter(Collider other)
	//{
	//	Debug.Log("Tag:" + other.tag);
	//}

	void OnCollisionEnter(Collision other)
	{
		Debug.Log("Collision");
	}
}
