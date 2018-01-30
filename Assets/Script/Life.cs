using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {

	[SerializeField]
	protected float m_MaxLife;
	protected float m_CurrentHealth;
	protected bool m_IsAlive = true;


	void Awake ()
	{
		m_CurrentHealth = m_MaxLife;
		m_IsAlive = true;//esta linea no hace nada
	}

	protected virtual void Update()
	{
        if (m_CurrentHealth <= 0) 
		{ 
            m_CurrentHealth = 0;
			m_IsAlive = false;
		}
		else
			m_IsAlive = true;
	}

	public bool IsAlive()
	{
		return m_IsAlive;
	}

	public virtual void DealDamage(float value)
	{
		m_CurrentHealth -= value;
	}

	public void SetLife(float value)
	{
		m_CurrentHealth = value;
	}

	public float GetLife()
	{
		return m_CurrentHealth;
	}

    private void OnEnable()
    {
        m_CurrentHealth = m_MaxLife;    
    }
}