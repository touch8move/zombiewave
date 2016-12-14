using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetScript : MonoBehaviour {
	//public GameObject Target;
	public Slider TargetHPSlider;
	public float TargetHealth;
	float targetCurrentHealth;
	Animator ani;
	bool isAlive;
	public float TargetCurrentHealth
	{
		get
		{
			return targetCurrentHealth;
		}
		set
		{
			targetCurrentHealth = value;
			if (targetCurrentHealth <= 0 && isAlive)
			{
				isAlive = false;
				targetCurrentHealth = 0;
				ani.SetBool("Death_b", true);
			}
			TargetHPSlider.value = targetCurrentHealth;
		}
	}
	// Use this for initialization
	void Awake()
	{
		ani = GetComponentInChildren<Animator>();
	}
	void Start () {
		isAlive = true;
		TargetHPSlider.minValue = 0;
		TargetHPSlider.maxValue = TargetHealth;
		TargetCurrentHealth = TargetHealth;
		ani.SetInteger("Animation_int", 4);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TakeDamage(int damage)
	{
		Debug.Log("TakeDamage:");
		TargetCurrentHealth -= damage;
	}
}
