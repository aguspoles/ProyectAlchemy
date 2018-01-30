using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Events
{

    Transform[] posiciones;
    [SerializeField]
    Pool m_pool;
	public bool respawn = false;
	public float timeToSpawn = 2;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        posiciones = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!m_activeOnce)
            {
                for (int i = 0; i < posiciones.Length; i++)
                {
                    if (this.transform != posiciones[i])
                    {
                        PoolObject aux = m_pool.GetPooledObject();
                        aux.transform.position = posiciones[i].position;
                        aux.transform.rotation = posiciones[i].rotation;
                    }
                }
				if (!respawn)
					m_activeOnce = true;
				else
					StartCoroutine (Wait ());
            }
        }
    }

	IEnumerator Wait(){
		m_activeOnce = true;
		yield return new WaitForSeconds (timeToSpawn);
		m_activeOnce = false;
	}
}
