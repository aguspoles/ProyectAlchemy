using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour {

    private IEnemyStrategy strategy;
    private LightJumpStrategy strategyA;
    private HeavyJumpStrategy strategyB;
    private EnemyFollowerMovement movement;
    private Rigidbody rigidB;

    [SerializeField]
    private float idleTime;
    private float timer;

    private bool isAttacking;
    private bool readyToAttack;
    public bool spawnAnimation;
    private bool spawnFinished;
    private bool jumping;
    private bool goingDown;
    private float prevY;
    [SerializeField]
    private float initialAngle;

    private Animation anim;

    [SerializeField]
    private Transform pointToJump;

    private PlayerMovement playerMov;

    void Start () {
        //transform.LookAt(GameObject.Find("Player").transform);

        strategyA = GetComponent<LightJumpStrategy>();
        strategyB = GetComponent<HeavyJumpStrategy>();
        movement = GetComponent<EnemyFollowerMovement>();
        rigidB = GetComponent<Rigidbody>();

        playerMov = GameObject.Find("Player").GetComponent<PlayerMovement>();

        timer = 0;

        isAttacking = false;
        readyToAttack = true;
        if (spawnAnimation)
            spawnFinished = false;
        else
            spawnFinished = true;

        jumping = false;
        goingDown = false;
        prevY = transform.position.y;

        anim = GetComponentInChildren<Animation>();
    }

    void FixedUpdate() {

        if (!spawnFinished)
        {
            gameObject.layer = LayerMask.NameToLayer("IgnoreWallsAndFloor");

            if (transform.position.y < prevY)
            {
                gameObject.layer = LayerMask.NameToLayer("IgnoreWalls");
                goingDown = true;
            }
            prevY = transform.position.y;

            if (!jumping)
                Jump();
            return;
        }

        if (playerMov.GhostWalkActivated())
            return;

        if (isAttacking)
        {       
            strategy.Execute();
            return;
        }


        anim.Play("Walk");
        movement.RotateToPlayer();

        if (!readyToAttack)
        {
            timer += Time.deltaTime;
            if (timer >= idleTime)
            {
                readyToAttack = true;
                timer -= idleTime;
            }
        }
        else
        {
            strategy = strategyA;
            isAttacking = true;
            readyToAttack = false;
        }
    }
    public void AttackFinished()
    {
        isAttacking = false;
    }

    private void Jump()
    {
        Vector3 destPos = pointToJump.position;

        float gravity = Physics.gravity.magnitude;

        float angle = initialAngle * Mathf.Deg2Rad;

        Vector3 planarDest = new Vector3(destPos.x, 0, destPos.z);
        Vector3 planarPos = new Vector3(transform.position.x, 0, transform.position.z);

        float distance = Vector3.Distance(planarDest, planarPos);

        float yOffset = transform.position.y - planarPos.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 vel = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarDest - planarPos) * (destPos.x > transform.position.x ? 1 : -1);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * vel;

        rigidB.velocity = finalVelocity;

        jumping = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" && goingDown)
        {
            spawnFinished = true;
            gameObject.layer = LayerMask.NameToLayer("Enemy");
        }
    }

    public bool SpawnFinished()
    {
        return spawnFinished;
    }
}
