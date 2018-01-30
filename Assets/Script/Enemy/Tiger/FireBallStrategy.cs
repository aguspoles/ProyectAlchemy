using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallStrategy : MonoBehaviour, IEnemyStrategy {

	[SerializeField]
	GameObject m_FireBall;
	[SerializeField]
	private string bullPoolName;
	private Pool pool;

	void Start () {
		pool = GameObject.Find(bullPoolName).GetComponent<Pool>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Execute()
	{
		Debug.Log ("shooting fireball");
		PoolObject obj = pool.GetPooledObject();
		obj.transform.position = gameObject.transform.position;
		obj.transform.rotation = gameObject.transform.rotation;
	}
}
