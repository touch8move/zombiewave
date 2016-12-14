using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour {
	//public float speed = 3f;

	public Animator animator;
	//Ani Parameters
	float speed_f;
	int animation_int;
	bool death_b;
	bool jump_b;
	bool crouch_b;
	int WeaponType_int;
	bool grounded_b;
	bool fullauto_b;
	bool shoot_b;
	bool reload_b;
	//bool static_b;

	float head_horizontal_f;
	float head_vertical_f;

	int deathtype_int;

	float body_vertical_f;
	float body_horizontal_f;
	public WeaponScript weapon;
	public Transform rightHand;
	public Transform leftHand;

	float vertical;

	//public Transform head;
	//public Transform body;
	// Use this for initialization
	void Start () {
		//WeaponType_int = 3;
		animator.SetInteger("WeaponType_int", 6);
		animator.SetFloat("Body_Horizontal_f", 0.6f);
		animator.SetFloat("Head_Horizontal_f", -0.2f);
		//weapon = new WeaponScript();
		weapon.ChangeWeapon(12, rightHand);
		//vertical_down = -30.0f;
		vertical = 50.0f;
	}
	
	// Update is called once per frame
	void Update () {
		//if (weapon.WeaponReady())
		//{
		//	animator.SetBool("Shoot_b", false);
		//}
	}



	public void Shot(float xAngle, float yAngle)
	{
		if (weapon.WeaponReady())
		{
			//animator.SetBool("Shoot_b", true);
			Debug.Log("xAngle: " + xAngle + " yAngle: " + yAngle);
			animator.SetTrigger("Shoot_t");
			weapon.FireWeapon(xAngle, yAngle);
		}
	}

	public void Reload()
	{
	}

	public void UpdateCharacterDirection(float YDegree)
	{
		if (YDegree >= vertical && YDegree < 0)
		{
			//Debug.Log("-20");
			animator.SetFloat("Body_Vertical_f", YDegree / -(vertical*2));
		}
		else if (YDegree >= 0 && YDegree < vertical)
		{
			//Debug.Log("45");
			animator.SetFloat("Body_Vertical_f", YDegree / (vertical*2));
		}
		//else if (YDegree >= 45 && YDegree < 90)
		//{
		//	//Debug.Log("90");
		//	animator.SetFloat("Head_Vertical_f", 1);
		//	animator.SetFloat("Body_Vertical_f", (YDegree - 45.0f) / 45.0f);
		//}
	}
}
