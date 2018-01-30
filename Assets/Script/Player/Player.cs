using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	Pool m_bulletPool;
	[SerializeField]
	float m_FirePerSecond = 2.0f;
	float m_SecondsLimiter = 1.0f;
	float m_FireTime = 0.0f;
	float m_FireSpeedBuffTimer = 0.0f;
	bool m_FireBuffTrigger = false;
	float m_InitialFirePerSecond;

    float m_shootAnimTimer = 0.0f;
    float m_shootAnimMaxTimer = 0.25f;

	Transform m_MousePos;
    Animator anim;
	AudioManager audioManager;
    PlayerMovement movement;

	// Use this for initialization
	void Awake ()
	{
		m_MousePos = GameObject.Find ("MousePoint").transform;
		m_InitialFirePerSecond = m_FirePerSecond;
		audioManager = FindObjectOfType<AudioManager> ();
	}

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        anim.SetFloat("AttackSpeed", m_FirePerSecond * 2.5f);
        m_bulletPool = PoolManager.GetInstance().GetPool("BulletPool");
        movement = GetComponent<PlayerMovement>();
    }
    void Update()
	{
        if (!LevelStartingPoint.GetInstance().IsFinished() || LevelEndingPoint.GetInstance().isFinishing())
            return;

        if (m_FireSpeedBuffTimer > 0)
		{
			m_FireSpeedBuffTimer -= Time.deltaTime;
			if (m_FireBuffTrigger)
			{
				m_FirePerSecond = m_InitialFirePerSecond * 2;
				m_FireBuffTrigger = false;
			}
			if (m_FireSpeedBuffTimer <= 0)//tiene que ir despues del -= Time.deltaTime
			{
				m_FirePerSecond = m_InitialFirePerSecond;
				m_FireSpeedBuffTimer = 0;
			}
		}
	}
	void FixedUpdate ()
	{
        if (!LevelStartingPoint.GetInstance().IsFinished() || LevelEndingPoint.GetInstance().isFinishing())
            return;

     

		if (m_FireTime < m_SecondsLimiter)
			m_FireTime += Time.deltaTime * m_FirePerSecond;

		if (Input.GetMouseButton(0) && !movement.GhostWalkActivated())
		{
            anim.SetBool("Attacking", true);
            movement.IsAttacking(true);
            if (m_shootAnimTimer < m_shootAnimMaxTimer)
            {
                m_shootAnimTimer += Time.deltaTime;
            }
            else
            {
                anim.SetBool("Attacking", true);
                movement.IsAttacking(true);
                if (m_FireTime >= m_SecondsLimiter)
                {
                    PoolObject bul = m_bulletPool.GetPooledObject ();
                    Vector3 dir = m_MousePos.position - transform.position;
                    dir.y = 0.0f;
                    bul.gameObject.transform.rotation = Quaternion.LookRotation(dir);
                    m_FireTime = 0.0f;

					audioManager.Play ("Shoot");
                }

            }
		}
        else
        {
            m_shootAnimTimer = 0;
            anim.SetBool("Attacking", false);
            movement.IsAttacking(false);
        }
    }
	public void BuffFireSpeed(float value)
	{
		m_FireSpeedBuffTimer += value;
		m_FireBuffTrigger = true;
	}


}