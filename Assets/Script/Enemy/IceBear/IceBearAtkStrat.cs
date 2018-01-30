using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBearAtkStrat : MonoBehaviour, IEnemyStrategy {

	private IceBear m_icebear;
	private Rigidbody m_rigidbody;
	private Vector3 m_chargeDirection;
	private float m_time;
	private GameObject m_player;
	private Life m_life;
	[SerializeField]
	private float m_damage;
	[SerializeField]
	private float m_minVelocity;
	private bool m_isAttacking;

	public float chargeTime;
	public float chargeVelocity;
    private Animator anim;

    // Use this for initialization
    void Start () {
		m_icebear = GetComponent<IceBear> ();
		m_rigidbody = GetComponent<Rigidbody> ();
		m_player = GameObject.FindGameObjectWithTag ("Player");
		m_life = m_player.GetComponent<Life> ();
		m_isAttacking = false;
        anim = GetComponentInChildren<Animator>();
    }

	void Update()
	{
		m_chargeDirection = (transform.position - m_player.transform.position) * -1;
		//reset del tiempo para la carga
		if (m_chargeDirection.magnitude >= m_icebear.GetDistance ())
			m_time = 0;
		if (m_rigidbody.velocity.magnitude <= m_rigidbody.drag)
			m_isAttacking = false;
	}

	public void Execute()
	{
		m_icebear.RotateToPlayer ();
        if (m_time >= chargeTime)
        {   
            anim.Play("Attack");
            m_isAttacking = true;
            m_rigidbody.velocity = m_chargeDirection * chargeVelocity * Time.deltaTime;
            m_time = 0;
        }

        m_time += Time.deltaTime;
	}

	void OnCollisionEnter(Collision collision)
	{
		if (m_isAttacking)
		{
			if (collision.gameObject.tag == "Player") {
				m_life.DealDamage (m_damage);
				Debug.Log (m_player.GetComponent<PlayerHealth> ().GetLife ());
			}
		}
	}
		
}
