using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

	[SerializeField]
	private float m_Damage = 5f;
	[SerializeField]
	private float m_Speed = 30.0f;

	PlayerHealth m_PlayerHealth;
	// Use this for initialization
	void Start () {
		m_PlayerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth> ();
		Destroy (gameObject, 5);//CREAR/CAMBIAR A POOL DE FIREBALLS
	}
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate (Vector3.forward * m_Speed * Time.deltaTime); 
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player")
		{
			m_PlayerHealth.DealDamage(m_Damage);
			Destroy (gameObject);
		}
	}
}