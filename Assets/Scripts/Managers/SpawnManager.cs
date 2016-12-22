using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
	public TargetScript target;
	Transform[] spawnPoint;
	public int StageZombieCount;
	public int TotalSpawnCount;
	public GameObject SpawnGroup;
	public GameObject[] zombies;
	public GameObject ZombieObject;
	public float GenTime;
	float curGenTime;
	bool GeneratingZombie;
	int curZombieIndex;
	int objectCount;
	Vector3 OriginPoint;
	void Awake()
	{
		curZombieIndex = 0;
		OriginPoint = new Vector3(0, 0, 3000);
		spawnPoint = SpawnGroup.transform.GetComponentsInChildren<Transform>();
	}
	void Start()
	{

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
			zombies[i] = Instantiate(ZombieObject, OriginPoint, Quaternion.identity);

			zombies[i].name = objectCount.ToString();
			objectCount++;
		}
	}

	public void StartGame()
	{
		GeneratingZombie = true;
	}

	void Update()
	{
		if (GeneratingZombie)
		{
			curGenTime += Time.deltaTime;
			if (curGenTime > GenTime)
			{
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

	void UpdateZombieHp()
	{
		
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

	public Vector3 ReturnZombiePoint()
	{
		return OriginPoint;
	}
}
