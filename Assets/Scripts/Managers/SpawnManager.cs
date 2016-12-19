﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
	public TargetScript target;
	public Transform[] spawnPoint;
	public Transform OriginspawnPoint;
	public int StageZombieCount;
	public int TotalSpawnCount;
	GameObject[] zombies;
	public GameObject ZombieObject;
	public float GenTime;
	float curGenTime;
	bool GeneratingZombie;
	int curZombieIndex;
	int objectCount;
	// Use this for initialization
	void Awake()
	{
		//zombies = new GameObject[
		curZombieIndex = 0;
	}
	void Start () {
		//objectCount = 0;
		//InvokeRepeating("GenerateZombie", 1, 1);
		//GeneratingZombie = true;
	}

	public void SetSpawnValue(int spawnCount)
	{
		curZombieIndex = 0;
		if (zombies != null)
		{
			for (int i = 0; i < zombies.Length; i++)
			{
				Destroy(zombies[i]);
			}
		}
		zombies = new GameObject[spawnCount];
		TotalSpawnCount = spawnCount;

		for (int i = 0; i < zombies.Length; i++)
		{
			zombies[i] = Instantiate(ZombieObject, OriginspawnPoint.position, Quaternion.identity);

			zombies[i].name = objectCount.ToString();
			objectCount++;
		}
	}

	public void StartGame()
	{
		GeneratingZombie = true;
	}
	// Update is called once per frame
	void Update () {
		if (GeneratingZombie)
		{
			curGenTime += Time.deltaTime;
			if (curGenTime > GenTime)
			{
				//GenerateZombie();
				CallZombie();
				curGenTime = 0;
			}
		}
	}
	void CallZombie()
	{
		int index = Random.Range(0, spawnPoint.Length);
		zombies[curZombieIndex].transform.position = spawnPoint[index].position;
		zombies[curZombieIndex].SetActive(true);
		curZombieIndex++;
		if (curZombieIndex == zombies.Length)
		{
			GeneratingZombie = false;
		}
	}

	void GenerateZombie()
	{
		int index = Random.Range(0, spawnPoint.Length);
		Instantiate(ZombieObject, spawnPoint[index].position, Quaternion.identity);
		TotalSpawnCount++;
		if (TotalSpawnCount == StageZombieCount)
		{
			GeneratingZombie = false;
		}
	}
}
