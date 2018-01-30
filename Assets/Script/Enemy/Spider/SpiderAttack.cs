using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAttack : MonoBehaviour, IEnemyStrategy
{
    [SerializeField]
    private GameObject m_bulletPreFab;
    [SerializeField]
    private Transform shootPos;
    private Transform player;
    [SerializeField]
    private float m_rateOfFire;
    [SerializeField]
    private float m_delayOfShoots;
    [SerializeField]
    private int m_maxBullets;
    private int m_bulletsFired;
    private float m_burstTimer;
    private float m_shootsTimer;
    private bool m_isWaiting;
    private bool m_isLooking;

    [SerializeField]
    private string bullPoolName;
    private Pool pool;

    private float animTimer = 0;

    private void Start()
    {
        pool = GameObject.Find(bullPoolName).GetComponent<Pool>();
        m_bulletsFired = 0;
        m_shootsTimer = m_rateOfFire;
        m_burstTimer = m_delayOfShoots;
        m_isWaiting = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Execute()
    {
        animTimer += Time.deltaTime;
        if (animTimer < 0.15f)
            return;

        if (m_isWaiting)
        {
            m_burstTimer += Time.deltaTime;
            if (m_burstTimer >= m_delayOfShoots)
            {
                m_isWaiting = false;
                m_burstTimer -= m_delayOfShoots;
            }
        }
        else
        {
            if (!m_isLooking)
                transform.LookAt(player);
            m_isLooking = true;
            Shoot();
        }
    }

    private void Shoot()
    {
        m_shootsTimer += Time.deltaTime;

        if (m_shootsTimer >= m_rateOfFire)
        {
            PoolObject obj = pool.GetPooledObject();
            obj.gameObject.GetComponent<Bullet>().SetShootPoint(shootPos);
            m_bulletsFired++;
            m_shootsTimer -= m_rateOfFire;
        }

        if (m_bulletsFired >= m_maxBullets)
        {
            m_bulletsFired = 0;
            m_shootsTimer = m_rateOfFire;
            m_isWaiting = true;
            m_isLooking = false;
            animTimer = 0;
        }
    }

    public void ResetAttack()
    {
        m_bulletsFired = 0;
        animTimer = 0;
    }
}
