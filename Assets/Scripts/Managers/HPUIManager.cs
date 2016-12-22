using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPUIManager : MonoBehaviour {
	SpawnManager spawnManager;
	public GameObject HPUI;
	GameObject[] HPs;
	void Awake()
	{
		spawnManager = FindObjectOfType<SpawnManager>();
	}

	public void CreateZombieHPUI()
	{
		for (int i = 0; i < spawnManager.zombies.Length; i++)
		{
			HPs[i] = Instantiate(HPUI);
		}
	}

	void UpdateHPUI()
	{
		for (int i = 0; i < HPs.Length; i++)
		{
			HPs[i].GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(spawnManager.zombies[i].transform.position + Vector3.up * 3);
		}
	}
}
