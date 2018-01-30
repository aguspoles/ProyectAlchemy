using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour {

    /*[SerializeField]
	private float m_Timer;
	private float m_initialTime;*/
    protected GameObject m_player;
	protected bool m_activeOnce = false;

	protected virtual void Start ()
	{
        m_player = GameObject.FindGameObjectWithTag("Player");
		//m_initialTime = m_Timer;        
	}

	void Update () 
	{

	}

	protected virtual void OnTriggerEnter(Collider other)
	{
		/*//Ejemplo
		if (!m_activeOnce) {
			m_activeOnce = true;
			//evento no repetible
		}                           */
	}

	protected virtual void OnTriggerStay(Collider other)
	{
        /*//Ejemplos
		if (!m_activeOnce) {
			m_activeOnce = true;
			//evento no repetible
		}
		//eventos repetibles		*/
	}

	protected virtual void OnTriggerExit(Collider other)
	{
		
	}
}