using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravPot : ThrowablePotion {

	[SerializeField]
	float damage;
	Pool pool;
	PoolObject effect;

	// Use this for initialization
	void Awake () {
		Code = (int)CraftingDataBase.PotionsList.GravPotion;
	}

	void Start()
	{
		pool = PoolManager.GetInstance ().GetPool ("GravExplosionPool");
		effect = pool.GetPooledObject ();
		effect.gameObject.transform.position = transform.position;
	}

	public override void Effect(GameObject target)
	{
		if (target.GetComponent<GravityEffect> () == null) 
		{
			target.AddComponent<GravityEffect> ();
			GravityEffect eff = target.GetComponent<GravityEffect> ();
			eff.SetDuration (3);
		}
	}
}
