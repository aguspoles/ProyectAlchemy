using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExpPot : ThrowablePotion
{
    [SerializeField]
    float damage;
	Pool pool;
	PoolObject effect;

    // Use this for initialization
    void Awake () {
        Code = (int)CraftingDataBase.PotionsList.FirePotion;
    }

	void Start()
	{
		pool = PoolManager.GetInstance ().GetPool ("FireExplosionPool");
	    effect = pool.GetPooledObject ();
		effect.gameObject.transform.position = transform.position;
	}

    public override void Effect(GameObject target)
    {
        target.GetComponent<Life>().DealDamage(damage);
    }

    private void OnEnable()
    {
		FindObjectOfType<AudioManager> ().Play ("FirePotion");
    }
}
