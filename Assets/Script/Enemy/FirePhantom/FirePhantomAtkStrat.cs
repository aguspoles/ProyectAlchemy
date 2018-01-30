using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePhantomAtkStrat : MonoBehaviour, IEnemyStrategy {

	private float m_time;
	private bool explode = false;
	[SerializeField]
	float explosionFroce = 500;
	[SerializeField]
	float explosionRadius = 5;
	[SerializeField]
	int damage = 10;
	[SerializeField]
	float explodeTime = 2;
	//float recycleTime = 0.3f;
	GameObject player;
	private Life m_playerLife;
    private Life m_life;
	FirePhantom phantom;
	[SerializeField]
	private GameObject m_explosionEffect;

	void OnEnable()
	{
		explode = false;
	}
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		m_playerLife = player.GetComponent<Life> ();
        m_life = GetComponent<Life>();
		phantom = GetComponent<FirePhantom> ();
	}
	
	// Update is called once per frame
	void Update () {
			m_time += Time.deltaTime;
	}

	public void Execute()
	{
		if (m_time >= explodeTime) 
		{
			player.GetComponent<Rigidbody> ().AddExplosionForce (explosionFroce, transform.position, explosionRadius);
			if (phantom.GetDistance () <= explosionRadius && !explode) {
				Debug.Log ("aaaaaa");
				m_playerLife.DealDamage (damage);
				explode = true;
			}
			m_explosionEffect.SetActive(true);

			StartCoroutine (EFFECT());
		}
	}

	IEnumerator EFFECT()
	{
		yield return new WaitForSeconds (1);
		m_life.DealDamage(m_life.GetLife());
	}
		
}
