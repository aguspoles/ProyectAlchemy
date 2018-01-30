using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger : MonoBehaviour {


	private IEnemyStrategy m_Strategy;
	private FireBallStrategy m_FireBallStrategy;
	private MeleeStrategy m_MeleeStrategy;
	private EnemyFollowerMovement m_Movement;
    private Transform m_player;

	private bool m_CanAttack;
	private float m_AtkTimer;
	private float m_FireBallTimer;
	[SerializeField]
	private float m_secondsPerFireBall;
	[SerializeField]
	private float m_IddleTimer = 1.0f;
	private float m_Timer = 0.0f;
    private float m_fireEffTimer = 0.0f;
    public float maxEffectTime;
    [SerializeField]
    private float m_minDist;
	[SerializeField]
	private GameObject m_fireBreathParticle;

    private Animator anim;

    private PlayerMovement playerMov;
    // Use this for initialization
    void Start ()
	{
		transform.LookAt(GameObject.Find("Player").transform);
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
		m_FireBallStrategy = GetComponent<FireBallStrategy> ();
		m_MeleeStrategy = GetComponent<MeleeStrategy> ();
		m_Movement = GetComponent<EnemyFollowerMovement> ();
		m_Strategy = m_FireBallStrategy;
		m_CanAttack = false;
        anim = GetComponentInChildren<Animator>();


        playerMov = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
	{
        if (m_fireBreathParticle.activeSelf)
        {
            m_fireEffTimer += Time.deltaTime;
            if (m_fireEffTimer >= maxEffectTime)
            {
                m_fireBreathParticle.SetActive(false);
                m_fireEffTimer -= maxEffectTime;
            }
        }
        if (!m_CanAttack)
        {
            m_Timer += Time.deltaTime;
            if (m_Timer >= m_IddleTimer)
            {
                m_CanAttack = true;
                m_Timer -= m_IddleTimer;
            }
            else
                return;
        }

        if (playerMov.GhostWalkActivated())
        {
            anim.Play("idle");
            return;
        }

        float distPlayer = Vector3.Distance(m_player.position, transform.position);

        if (m_CanAttack && distPlayer > m_minDist)
        {
			m_Strategy = m_FireBallStrategy;
			m_FireBallTimer += Time.deltaTime;
			if (m_FireBallTimer > m_secondsPerFireBall)
			{
                anim.Play("attack");
                m_fireBreathParticle.SetActive (true);
				m_FireBallTimer -= m_secondsPerFireBall;
				m_Strategy.Execute();
				m_CanAttack = false;
			} else
			{
                anim.Play("run");
                m_Movement.Move ();
				return;
			}
        }
        else if (m_CanAttack && distPlayer <= m_minDist)
        {
            anim.Play("attack");
            m_Strategy = m_MeleeStrategy;
			gameObject.transform.LookAt (m_player);
            m_Strategy.Execute();
            m_CanAttack = false;
        }

		/*OLD STRATEGY

		float distPlayer = Vector3.Distance(m_player.position, transform.position);
		 
		if (m_CanAttack && distPlayer > m_minDist)
        {
            m_Movement.Move();
            return;
        }

        else if (m_CanAttack && distPlayer <= m_minDist)
        {

            int rand = Random.Range(0, 5);
			if (rand == 0)
            {
				m_fireBreathParticle.SetActive (false);
				m_Strategy = m_FireBallStrategy;
			}
            else
            {
				m_fireBreathParticle.SetActive (true);
				m_Strategy = m_MeleeStrategy;
			}

            m_Strategy.Execute();
            m_CanAttack = false;
        }
		*/
	}
}
