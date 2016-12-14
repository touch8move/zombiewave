using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
	public GameObject target;
	public int CurrentWave;
	public SpawnManager spawnManager;
	public CountDownScript countdown;
	public int SpawnCount;
	//int SpawnCount;
	int KillCount;
	// Use this for initialization
	void Awake()
	{

	}
	void Start()
	{
		Invoke("StartGame", 0);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void StartGame()
	{
		countdown.CountDownStart();
		InitGame();
	}

	void InitGame()
	{
		CurrentWave = 1;
		spawnManager.SetSpawnValue(SpawnCount);
		spawnManager.StartGame();
	}

	public void ClearGame()
	{
		CurrentWave += 1;
	}

	public void FailGame()
	{
		
	}

	public void IncreaseKill()
	{
		KillCount += 1;
		if (KillCount == SpawnCount)
		{
			ClearGame();
		}
	}
}
