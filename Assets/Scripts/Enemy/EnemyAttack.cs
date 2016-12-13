using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    //GameObject player;
    //PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    //bool playerInRange;
    float timer;
	Controller con;

	//Ray shootRay;
	//public Transform ShootPos;
	//RaycastHit shootHit;
	//int shootableMask;
	ParticleSystem gunParticles;
	public GameObject GunbarrelEnd;
	LineRenderer gunLine;
	AudioSource gunAudio;
	Light gunLight;
	float effectsDisplayTime = 0.2f;
	//public int damagePerShot = 20;
	public float timeBetweenBullets = 0.15f;
	//public float range = 100f;

    void Awake ()
    {
		//player = GameObject.FindGameObjectWithTag ("Player");
		con = FindObjectOfType<Controller>();
        //playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();


		//shootableMask = LayerMask.GetMask("Shootable");
		gunParticles = GunbarrelEnd.GetComponent<ParticleSystem>();
		gunLine = GunbarrelEnd.GetComponent<LineRenderer>();
		gunAudio = GunbarrelEnd.GetComponent<AudioSource>();
		gunLight = GunbarrelEnd.GetComponent<Light>();
    }
    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks)
        {
            Attack ();
        }

		//if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
		//{
			
		//}

		if (timer >= timeBetweenBullets * effectsDisplayTime)
		{
			DisableEffects();
		}
    }


    void Attack ()
    {
        timer = 0f;

		if(con.IsGameOn && enemyHealth.isAlive())
        {
			Shoot();
			//playerHealth.TakeDamage (attackDamage)
			anim.SetTrigger("Attack");;
			con.TakeDamage(attackDamage);
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

		//shootRay.origin = transform.position;
		//shootRay.direction = transform.forward;

		//gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		gunLine.SetPosition(1, con.TargetTransform.position);
    }
}
