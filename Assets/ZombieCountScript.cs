using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieCountScript : MonoBehaviour {
	Text ZombieCount;
	int zombieCount;
	void Awake()
	{
		ZombieCount = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetZombieCount(int count)
	{
		ZombieCount.text = count.ToString("N0");
	}
}
