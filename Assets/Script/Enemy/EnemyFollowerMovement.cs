using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowerMovement : Movement {

    [SerializeField]
    private float m_rotSpeed;
    private Transform player;


    private Vector3 m_playerDir;

    // Use this for initialization
    override protected void Start () {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {

        m_playerDir = player.position - transform.position;
    }

    public override void SpecificMovement()
    {

        RotateToPlayer();

        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    public void RotateToPlayer()
    {
        float step = m_rotSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, m_playerDir, step, 0);

        transform.rotation = Quaternion.LookRotation(newDir);
    }

    public void RotateToPlayer(float rotSpd)
    {
        float step = rotSpd * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, m_playerDir, step, 0);

        transform.rotation = Quaternion.LookRotation(newDir);
    }

    public Vector3 GetPlayerDir()
    {
        return m_playerDir;
    }
}
