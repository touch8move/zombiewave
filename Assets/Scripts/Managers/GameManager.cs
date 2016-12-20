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
	public int SpawnInitCount;
	int SpawnTotalCount;
	ZombieCountScript zombieCountScript;
	public ClearScript cl;
	public FailScript fl;
	public enum GamePhase
	{
		Init,
		Playing,
		EndGame,
		Clear,
		Fail
	}
	public GamePhase phase;

	//public RectTransform DamageText;
	int KillCount;
	// Use this for initialization
	void Awake()
	{
		zombieCountScript = FindObjectOfType<ZombieCountScript>();

	}
	void Start()
	{
		InitGame(1);
	}
	public void RestartGame()
	{
		InitGame(1);
	}

	void InitGame(int wave)
	{
		CurrentWave = wave;
		phase = GamePhase.Init;
		//Invoke("StartGame", 0);
		KillCount = 0;
		//countdown.CountDownStart(CurrentWave);
		SpawnTotalCount = SpawnInitCount + (CurrentWave * 2);
		//zombieCountScript.SetZombieCount(SpawnTotalCount);
		//spawnManager.SetSpawnValue(SpawnTotalCount);
		//spawnManager.StartGame();
		phase = GamePhase.Playing;
	}

	public void ClearGame()
	{
		CurrentWave += 1;
		phase = GamePhase.Clear;
		//Invoke
		cl.ShowPanel();
	}
	public void EndGame()
	{
		phase = GamePhase.EndGame;
	}
	public void FailGame()
	{
		fl.ShowPanel();
		phase = GamePhase.Fail;
	}

	public void KillZombie()
	{
		KillCount += 1;
		zombieCountScript.SetZombieCount(SpawnTotalCount - KillCount);
		if (KillCount == SpawnTotalCount)
		{
			//ClearGame();
			Invoke("ClearGame", 2);
		}
	}

	public void Next()
	{
		if (phase == GamePhase.Clear)
		{
			cl.HidePanel();

		}
		else if (phase == GamePhase.Fail)
		{
			fl.HidePanel();
		}
		InitGame(CurrentWave);
	}
}
