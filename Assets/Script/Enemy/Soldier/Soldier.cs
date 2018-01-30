using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour {

    [SerializeField]
    private float m_minDist;
    private Transform playerTrans;
    private Movement movement;
    //private Life life;
    [SerializeField]
    private GameObject attack;
    private bool m_isWaiting;
    private float m_timer;
    [SerializeField]
    private float m_idleTime;
    private Animator anim;
    private float m_animTimer = 0;

    private PlayerMovement playerMov;

    void Start ()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        movement = GetComponent<Movement>();
        anim = GetComponentInChildren<Animator>();
        m_isWaiting = false;

        playerMov = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
	
	void FixedUpdate ()
    {
        if (playerMov.GhostWalkActivated())
        {
            anim.Play("Idle");
            return;
        }

        if (Vector3.Distance(playerTrans.position, transform.position) <= m_minDist && !m_isWaiting)
        {
            anim.Play("Attack02");
            m_animTimer += Time.deltaTime;
            if (m_animTimer >= 0.40f)
            {
                m_animTimer = 0;
                attack.SetActive(true);
                m_isWaiting = true;
            }

            return;
        }
        
        if (m_isWaiting)
        {
            m_timer += Time.deltaTime;
            if (m_timer >= m_idleTime)
            {
                m_isWaiting = false;
                m_timer -= m_idleTime;
            }
            return;
        }

        anim.Play("Run");
        movement.Move();
    }
}
