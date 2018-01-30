using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : ConsumablePotion {

	[SerializeField]
	float m_HealValue;
	PoolObject m_healPotionPO;
	Pool pool;
	PoolObject effect;

	void Awake () {
		Code = (int)CraftingDataBase.PotionsList.HealPotion;
		pool = PoolManager.GetInstance ().GetPool ("HealthEffectPool");
	}

	void Start()
	{

	}

	void Update()
	{
		if(effect)
			effect.gameObject.transform.position = m_Player.transform.position;
	}

	public override void Effect(GameObject target)
	{
		PlayerHealth ph = target.GetComponent<PlayerHealth> ();
		if (ph)
		{
			ph.SetLife (ph.GetLife() + m_HealValue);
			if (ph.GetLife () > 100)
				ph.SetLife (100);
			m_healPotionPO = GetComponent<PoolObject> ();
			if (m_healPotionPO == null)
				Debug.Log ("poolobject nulo");
			if(m_healPotionPO)
				m_healPotionPO.Recycle ();
		}
		effect = pool.GetPooledObject ();
		effect.gameObject.transform.position = m_Player.transform.position;
	}
}
