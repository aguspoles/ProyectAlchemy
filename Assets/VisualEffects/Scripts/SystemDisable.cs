using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemDisable : MonoBehaviour {

	float acumTime = 0;
	private ParticleSystem m_particleSystem;

	// Use this for initialization
	void Start () {
		m_particleSystem = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		acumTime += Time.deltaTime;
		if (acumTime >= m_particleSystem.main.duration)
			GetComponent<PoolObject> ().Recycle ();
	}
}
