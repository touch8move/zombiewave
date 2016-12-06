using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	//    public PlayerHealth playerHealth;
	public Controller controller;
    public GameObject[] enemy;
    public float spawnTime = 0.5f;
    public Transform[] spawnPoints;

	void Awake()
	{
		controller = FindObjectOfType<Controller>();
	}

    void Start ()
    {
		InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn ()
    {
		if (controller.IsGameOn)
		{
			int spawnPointIndex = Random.Range(0, spawnPoints.Length);
			int spawnEnemyIndex = Random.Range(0, enemy.Length);
			Instantiate(enemy[spawnEnemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
			Debug.Log("Generate Enemy");
		}
    }
}
