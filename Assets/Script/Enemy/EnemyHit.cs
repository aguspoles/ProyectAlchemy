using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour {

    [SerializeField]
    private float m_duration;
    [SerializeField]
    private float m_damage;
    [SerializeField]
    private float m_force;
    [SerializeField]
    private string m_target;

    private bool m_targetHit;
    private float m_timer;
	// Use this for initialization
	void Start () {
        m_timer = 0;
        m_targetHit = false; 
    }

    private void OnEnable()
    {
        m_timer = 0;
        m_targetHit = false;
    }

    // Update is called once per frame
    void Update () {
        m_timer += Time.deltaTime;
        if (m_timer >= m_duration)
            gameObject.SetActive(false);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!m_targetHit && other.gameObject.tag == m_target)
        {
            other.GetComponent<Life>().DealDamage(m_damage);
            other.GetComponent<Movement>().Push(m_force, transform.position);
            m_targetHit = false;
        }
    }
}
