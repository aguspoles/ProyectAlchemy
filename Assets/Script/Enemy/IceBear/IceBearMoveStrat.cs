using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBearMoveStrat : Movement, IEnemyStrategy {

	//[SerializeField]
	//int distance = 20;

	GameObject player;
	PlayerHealth playerHealth;
	EnemyHealth enemyHealth;
	//UnityEngine.AI.NavMeshAgent nav;

	// Use this for initialization
	protected override void Start () 
	{
		base.Start ();
		player = GameObject.FindGameObjectWithTag("Player");
		playerHealth = player.GetComponent<PlayerHealth>();
		enemyHealth = GetComponent<EnemyHealth>();
		/*nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
		nav.enabled = true;
		nav.speed = speed;*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Execute()
	{

	}
		
	public override void SpecificMovement()
	{
		/*nav.speed = speed;
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
		}*/
	}
}
