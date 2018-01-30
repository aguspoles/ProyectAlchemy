using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablePotion : Potions {

	protected GameObject m_Player;
	bool m_initialized;

	void Start () {
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		
	}
	protected override void OnEnable()
	{
		m_Player = GameObject.FindGameObjectWithTag ("Player");
		if (m_Player && m_initialized) 
		{
			Effect (m_Player);
		}
		m_initialized = true;
	}
}
