using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnimationCtrl : MonoBehaviour {
	CharacterScript character;
	Animator animator;

	void Awake()
	{
		animator = GetComponent<Animator>();
		character = FindObjectOfType<CharacterScript>();
	}

	void Start()
	{

	}

	public void FireAnimationEnd()
	{
		//Debug.Log("FireAnimationEnd");
		character.FireEnd();
	}
	// Update is called once per frame
	void Update () {
		
	}
}
