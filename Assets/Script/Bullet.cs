using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	[SerializeField]
	float speed;
	[SerializeField]
	int damage = 1;
    [SerializeField]
    private string target;
    [SerializeField]
    private float m_force;
	private Transform m_ShootPoint;
	[SerializeField]
	private string m_ShootPointName;
	protected PoolObject m_bulletPO;
	[SerializeField]
	private float m_bulletDuration;
	private float m_timer;
    
	void Awake () {

	}

	void Start()
	{
        m_bulletPO = GetComponent<PoolObject> ();
		m_timer = m_bulletDuration;
	}
	
	void Update () {
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
		m_timer -= Time.deltaTime;
		if (m_timer <= 0) 
		{
			m_timer = m_bulletDuration;
			m_bulletPO.Recycle ();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == target || other.gameObject.tag == "Bush")
        {
            Life otherLife = other.GetComponent<Life>();

            otherLife.DealDamage (damage);
            //if (otherLife.GetLife() > 0)
            //    other.GetComponent<Movement>().Push(m_force, transform.position);
			m_bulletPO.Recycle ();
        }
    }

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Walls" || other.gameObject.tag == "Obstacles")
			m_bulletPO.Recycle ();
	}

	void OnEnable()
	{
        if (m_ShootPoint == null)
        {
			GameObject shootPoint = GameObject.FindGameObjectWithTag("playerBulletPos");
			if (shootPoint != null) {
				m_ShootPoint = shootPoint.transform;
			}
            else
                return;
        }

        transform.position = m_ShootPoint.position;
		transform.rotation = m_ShootPoint.rotation;

	}

    public void SetShootPoint(Transform shootPos)
    {
        m_ShootPoint = shootPos;
        transform.position = m_ShootPoint.position;
        transform.rotation = m_ShootPoint.rotation;
    }
}