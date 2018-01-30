using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeStrategy : MonoBehaviour, IEnemyStrategy {

    [SerializeField]
    private GameObject m_attack;
	[SerializeField]
	private float m_chargeSpeed;
	private Rigidbody m_rigidbody;
	private Vector3 m_chargeDirection;
	private GameObject m_player;


	// Use this for initialization
	void Start () {
		m_player = GameObject.FindGameObjectWithTag ("Player");
		m_rigidbody = gameObject.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		m_chargeDirection = (transform.position - m_player.transform.position) * -1;
	}

	public void Execute()
	{
        m_attack.SetActive(true);
		m_rigidbody.velocity = m_chargeDirection * m_chargeSpeed * Time.deltaTime;
	}
}