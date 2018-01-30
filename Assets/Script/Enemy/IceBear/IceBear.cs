using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBear : MonoBehaviour {

	private GameObject player;

    private PlayerMovement playerMov;

    private IEnemyStrategy strategy;

	private IceBearAtkStrat atkStrat;
	private EnemyFollowerMovement movement;

	protected Vector3 m_playerDir;

	[SerializeField]
	private float m_rotSpeed;
	[SerializeField]
	private float m_distance;
    private Animator anim;
    
    void Start()
	{
        player = GameObject.FindGameObjectWithTag("Player");
		atkStrat = GetComponent<IceBearAtkStrat>();
		movement = GetComponent<EnemyFollowerMovement>();
        anim = GetComponentInChildren<Animator>();

        playerMov = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

	void FixedUpdate() 
	{
        if (playerMov.GhostWalkActivated())
        {
            anim.Play("Idle");
            return;
        }

        m_playerDir = player.transform.position - transform.position;

        if (m_playerDir.magnitude <= m_distance)
        {
			atkStrat.Execute();
        }
		else
        {
            anim.Play("Walk");
			movement.Move ();
        }
	}

	public void RotateToPlayer()
	{
		float step = m_rotSpeed * Time.deltaTime;

		Vector3 newDir = Vector3.RotateTowards(transform.forward, m_playerDir, step, 0);

		transform.rotation = Quaternion.LookRotation(newDir);
	} 

	public float GetDistance()
	{
		return m_distance;
	}
}
