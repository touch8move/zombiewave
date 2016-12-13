using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
	//CapsuleCollider capsuleCollider;
	SphereCollider[] colliders;
    bool isDead;
    bool isSinking;
	Controller con;

	//public GameObject Body;
	//public GameObject Head;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
		//capsuleCollider = GetComponent <CapsuleCollider> ();
		colliders = GetComponentsInChildren<SphereCollider>();
		con = FindObjectOfType<Controller>();
        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

		//capsuleCollider.isTrigger = true;
		for (int i = 0; i < colliders.Length; i++)
		{
			colliders[i].isTrigger = true;
		}
		anim.SetTrigger("Dead");

		enemyAudio.clip = deathClip;
        enemyAudio.Play ();
		con.Point += scoreValue;
		StartSinking();
    }

	public void LineDeath()
	{
		isDead = true;

		//capsuleCollider.isTrigger = true;
		for (int i = 0; i < colliders.Length; i++)
		{
			colliders[i].isTrigger = true;
		}
		currentHealth = 0;
		anim.SetTrigger("Dead");

		enemyAudio.clip = deathClip;
		enemyAudio.Play();
		StartSinking();
	}

    public void StartSinking ()
    {
		GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        
        Destroy (gameObject, 2f);
    }

	public bool isAlive(){
		return !isDead;
	}
}
