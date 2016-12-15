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
	public ClearScript cl;
	public FailScript fl;

	//public RectTransform DamageText;
	int KillCount;
	// Use this for initialization
	void Awake()
	{

	}
	void Start()
	{
		CurrentWave = 1;
		Invoke("StartGame", 0);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void StartGame()
	{
		//countdown.CountDownStart();

		InitGame();
	}

	void InitGame()
	{
		spawnManager.SetSpawnValue(SpawnCount+(CurrentWave*2));
		spawnManager.StartGame();
	}

	public void ClearGame()
	{
		CurrentWave += 1;
		cl.ShowPanel();
	}

	public void FailGame()
	{
		fl.ShowPanel();
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
