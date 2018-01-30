using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyJumpStrategy : MonoBehaviour, IEnemyStrategy
{
    private Transform player;
    private Frog frog;
    private Rigidbody rigidB;
    private EnemyFollowerMovement movement;
    [SerializeField]
    private GameObject iceAtk;

    [SerializeField]
    private float initialAngle;
    [SerializeField]
    private float m_minDist;

    private bool Jumping;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        frog = GetComponent<Frog>();
        rigidB = GetComponent<Rigidbody>();
        movement = GetComponent<EnemyFollowerMovement>();
        iceAtk.SetActive(false);

        Jumping = false;
    }

    public void Execute()
    {
        if (Vector3.Distance(player.position, transform.position) <= m_minDist)
        {
            if (!Jumping)
            {
                Jump();
            }
        }
        else
            movement.Move();

    }

    private void Jump()
    {
        Vector3 playerPos = player.position;

        float gravity = Physics.gravity.magnitude;

        float angle = initialAngle * Mathf.Deg2Rad;

        Vector3 planarPlayer = new Vector3(playerPos.x, 0, playerPos.z);
        Vector3 planarPos = new Vector3(transform.position.x, 0, transform.position.z);

        float distance = Vector3.Distance(planarPlayer, planarPos);

        float yOffset = transform.position.y - planarPos.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 vel = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarPlayer - planarPos) * (playerPos.x > transform.position.x ? 1 : -1);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * vel;

        rigidB.velocity = finalVelocity;

        Jumping = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Jumping && collision.gameObject.tag == "Floor")
        {
            Jumping = false;
            frog.AttackFinished();
            iceAtk.SetActive(true);
        }

        else if (Jumping && collision.gameObject.tag == "Player")
        {
            //hit y empujar
            Jumping = false;
            frog.AttackFinished();
        }

    }
}
