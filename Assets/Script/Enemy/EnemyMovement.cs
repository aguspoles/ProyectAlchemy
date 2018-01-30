using UnityEngine;
using System.Collections;

public class EnemyMovement : Movement
{
    [SerializeField]
    int distance = 20;
    [SerializeField]
    int damage = 10;

    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;

	protected override void Start()
    {
		base.Start ();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nav.enabled = true;
        nav.speed = speed;
    }


    void Update()
    {
		Move ();
    }

    public override void SpecificMovement()
    {
        nav.speed = speed;
        if (player)
        {
			if (enemyHealth.currentHealth > 0 && playerHealth.GetLife() > 0)
            {
                nav.SetDestination(player.transform.position);
            }
            else
            {
                nav.enabled = false;
            }
            if (Vector3.Distance(player.transform.position, transform.position) <= distance)
                nav.enabled = true;
            else
                nav.enabled = false;
        }
    }
    void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.tag == "Bullet")
        {
			enemyHealth.TakeDamage(damage);
            Destroy(other.gameObject);
        }


        else if (other.gameObject.tag == "Potion")
            other.GetComponent<Potions>().Effect(gameObject);
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "IcePotion")
            other.GetComponent<Potions>().Effect(gameObject);
    }

}
