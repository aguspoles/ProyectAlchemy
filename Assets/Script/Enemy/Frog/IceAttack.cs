using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAttack : MonoBehaviour {

    [SerializeField]
    private float m_freezeDur;

    [SerializeField]
    private float m_effectDur;
    private float m_timer;
    private bool m_effectAdded;

    private void OnEnable()
    {
        m_timer = 0;
        m_effectAdded = false;
    }

    // Update is called once per frame
    void Update () {
        m_timer += Time.deltaTime;
        if (m_timer >= m_effectDur)
            gameObject.SetActive(false);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !m_effectAdded)
        {
            other.gameObject.AddComponent<SlowEffect>();
            SlowEffect slow = other.gameObject.GetComponent<SlowEffect>();
            slow.SetDuration(m_freezeDur);
            slow.SetSlowAmount(100);
            m_effectAdded = true;
        }
    }
}
