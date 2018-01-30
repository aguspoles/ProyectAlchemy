using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour {

    private Transform player;
    private PlayerMovement playerMov;
    private EnemyFollowerMovement movement;

    private IEnemyStrategy strategy;

    private SpiderAttack attack;

    private Animation anim;

    [SerializeField]
    private float m_distance;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMov = player.gameObject.GetComponent<PlayerMovement>();
        movement = GetComponent<EnemyFollowerMovement>();
        attack = GetComponent<SpiderAttack>();
        anim = GetComponentInChildren<Animation>();
    }

    void Update()
    {
        if (playerMov.GhostWalkActivated())
        {
            anim.Play("Idle");
            return;
        }

        if (Vector3.Distance(player.position, transform.position) <= m_distance)
        {

            anim.Play("Attack");
            attack.Execute();
        }
        else
        {
            anim.Play("Run");
            attack.ResetAttack();
            movement.Move();
        }
    }
}
