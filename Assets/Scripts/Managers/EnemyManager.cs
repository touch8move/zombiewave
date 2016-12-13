using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	//    public PlayerHealth playerHealth;
	public Controller controller;
    public GameObject[] enemy;
	public GameObject Sniper;
	public Transform[] SniperPoint;
    public float spawnTime = 0.5f;
	public float spawnTimeSniper;
    public Transform[] spawnPoints;

	void Awake()
	{
		//controller = FindObjectOfType<Controller>();
	}

    void Start ()
    {
		Debug.Log("EnemyManager");
		InvokeRepeating("Spawn", 0, spawnTime);
		//InvokeRepeating("SpawnSniper", 1, spawnTimeSniper);
    }

    void Spawn ()
    {
		//Debug.Log("Spawn");
		if (controller.IsGameOn)
		{
			int spawnPointIndex = Random.Range(0, spawnPoints.Length);
			int spawnEnemyIndex = Random.Range(0, enemy.Length);
			Instantiate(enemy[spawnEnemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
			//Debug.Log("Generate Enemy");
		}
    }

	void SpawnSniper()
	{
		//Debug.Log("SpawnSniper");
		if (controller.IsGameOn)
		{
			int spawnPointIndex = Random.Range(0, SniperPoint.Length);
			//int spawnEnemyIndex = Random.Range(0, enemy.Length);
			Instantiate(Sniper, SniperPoint[spawnPointIndex].position, SniperPoint[spawnPointIndex].rotation);
			//Debug.Log("Generate Enemy");
		}
	}
}
