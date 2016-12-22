using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleWalkerScript : MonoBehaviour {
	Animator ani;
	ParticleSystem hitParticle;
	void Awake()
	{
		ani = GetComponentInChildren<Animator>();
		ani.SetTrigger("Walk");
		hitParticle = GetComponentInChildren<ParticleSystem>();
	}

	void Start()
	{
		
	}

	public void Hit()
	{
		hitParticle.Play();
	}
}
