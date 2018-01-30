using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HastePotion : ConsumablePotion {

	PoolObject m_hastePotionPO;
	Pool pool;
	PoolObject effect;

	void Awake () {
		Code = (int)CraftingDataBase.PotionsList.HastePotion;
		pool = PoolManager.GetInstance ().GetPool ("HasteEffectPool");
	}

	void Update()
	{
		if(effect)
			effect.gameObject.transform.position = m_Player.transform.position;
	}

	
	public override void Effect(GameObject target)
	{
		if(m_hastePotionPO == null)
			m_hastePotionPO = GetComponent<PoolObject> ();
		if (target == m_Player) {
			target.GetComponent<Player>().BuffFireSpeed(timeOfEffect);
		}
		if (m_hastePotionPO) {
			m_hastePotionPO.Recycle ();
		}
		effect = pool.GetPooledObject ();
		effect.gameObject.transform.position = m_Player.transform.position;
	}
}