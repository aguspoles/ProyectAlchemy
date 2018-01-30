using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePhantom : MonoBehaviour
{
	private GameObject player;

	private IEnemyStrategy strategy;

	private FirePhantomAtkStrat atkStrat;
	private Movement movement;

	[SerializeField]
	private float m_rotSpeed;
	[SerializeField]
	private float m_distance;
	protected Vector3 m_playerDir;
	private bool m_isAttacking;

    private PlayerMovement playerMov;

    void Start()
	{
        player = GameObject.FindGameObjectWithTag("Player");
        playerMov = GameObject.Find("Player").GetComponent<PlayerMovement>();
        atkStrat = GetComponent<FirePhantomAtkStrat>();
		movement = GetComponent<Movement>();
		m_isAttacking = false;
	}

	void Update() {

        if (playerMov.GhostWalkActivated())
            return;

        m_playerDir = player.transform.position - transform.position;

		if (m_playerDir.magnitude <= m_distance || m_isAttacking) 
		{
			m_isAttacking = true;
            atkStrat.Execute();
            return;
		}
        if (!m_isAttacking)
            movement.Move();

        
	}

	public float GetDistance()
	{
		return m_playerDir.magnitude;
	}

}


