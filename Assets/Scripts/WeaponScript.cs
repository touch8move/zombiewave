using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {
	abstract class State
	{
		public abstract void Reload(WeaponScript weapon);
		public abstract void Fire(WeaponScript weapon);
		public abstract void StateUpdate(WeaponScript weapon);
	}
	class ReloadState : State
	{
		public override void Fire(WeaponScript weapon)
		{
			//throw new NotImplementedException();
		}

		public override void Reload(WeaponScript weapon)
		{
			//throw new NotImplementedException();
		}

		public override void StateUpdate(WeaponScript weapon)
		{
			weapon.ReloadCurrentDelay += Time.deltaTime;
			if (weapon.ReloadCurrentDelay > weapon.ReloadDelay)
			{
				weapon.ReloadCurrentDelay = 0;
				//weapon.weaponState = new IdleState();
			}
			//throw new NotImplementedException();
		}
	}
	class IdleState : State
	{
		public override void Fire(WeaponScript weapon)
		{
			//weapon.weaponState = new FireState();
			//throw new NotImplementedException();
		}

		public override void Reload(WeaponScript weapon)
		{
			//weapon.weaponState = new ReloadState();
			//throw new NotImplementedException();
		}

		public override void StateUpdate(WeaponScript weapon)
		{
			//throw new NotImplementedException();
		}
	}
	class FireState : State
	{
		public override void Fire(WeaponScript weapon)
		{
			//throw new NotImplementedException();
		}

		public override void Reload(WeaponScript weapon)
		{
			//throw new NotImplementedException();
		}

		public override void StateUpdate(WeaponScript weapon)
		{
			weapon.shotCurrentDelay += Time.deltaTime;
			if (weapon.shotCurrentDelay >= weapon.shotDelay)
			{
				weapon.shotCurrentDelay = 0;
				//weapon.weaponState = new IdleState();
			}
			//throw new NotImplementedException();
		}
	}

	public GameObject Arrow;
	GameObject ShotPoint;

	public float weaponPower;

	public float shotDelay;
	public float shotCurrentDelay;
	public float ReloadDelay;
	public float ReloadCurrentDelay;

	//State weaponState;
	public WeaponManager weaponManager;
	public int WeaponIndex;
	//public Transform weaponPoint;
	GameObject weaponObject;
	//float 

	// Use this for initialization
	void Start () {
		//weaponState = new IdleState();
		//weaponManager = FindObjectOfType<WeaponManager>();
	}

	public void ChangeWeapon(int weaponIndex, Transform rightHandPosition)
	{
		weaponObject = Instantiate(weaponManager.weapons[weaponIndex]);

		weaponObject.transform.SetParent(rightHandPosition);
		weaponObject.transform.localPosition = Vector3.zero;
		weaponObject.transform.localRotation = Quaternion.Euler(0, 90, 90);

		ShotPoint = weaponObject.transform.FindChild("BulletStartPoint").gameObject;
		//if (ShotPoint)
		//{
		//	Debug.Log("Have");
		//}
		//else {
		//	Debug.Log("Null");
		//}
	}
	public bool WeaponReady()
	{
		bool ret = false;;
		////if (weaponState.GetType() == typeof(IdleState))
		//{
		//	ret = true;
		//}

		return ret;
	}
	// Update is called once per frame
	void Update()
	{
		//weaponState.StateUpdate(this);
	}

	public void FireWeapon(float xAngle, float yAngle)
	{
		//weaponState.Fire(this);
		ShotArrow(xAngle, yAngle);
	}

	public void ReloadWeapon()
	{
		//weaponState.Reload(this);
	}



	public void ShotArrow(float yAngle, float xAngle)
	{
		GameObject arrow = Instantiate(Arrow);
		GameObject _startPoint = Instantiate(ShotPoint, ShotPoint.transform.position, Quaternion.Euler(360-xAngle, yAngle + 180, 0.0f));
		arrow.transform.SetParent(_startPoint.transform);
		arrow.transform.localPosition = Vector3.zero;
		arrow.transform.localRotation = Quaternion.Euler(0, 180, 0);
		arrow.GetComponent<Arrow>().Shot(weaponPower, 8);
	}
}
