using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFloorPot : ThrowablePotion
{
    [SerializeField]
    float damage;
	Pool pool;
	PoolObject effect;

    public float slowDuration;
    public float slowAmount;
    
    void Awake() {
        Code = (int)CraftingDataBase.PotionsList.IceFloor;
    }

	void Start()
	{
		pool = PoolManager.GetInstance ().GetPool ("PlasmaExplosionPool");
		effect = pool.GetPooledObject ();
		effect.gameObject.transform.position = transform.position;
    }




    public override void Effect(GameObject target)
    {
        target.GetComponent<Life>().DealDamage(damage);
        target.AddComponent<SlowEffect>();
        SlowEffect eff = target.GetComponent<SlowEffect>();
        eff.SetDuration(slowDuration);
        eff.SetSlowAmount(slowAmount);
    }
}
