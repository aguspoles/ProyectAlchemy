using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour {

    private PoolObject m_poolObj;
    private Life m_life;
    private IngredientDrop m_drop;

    private Animation anim;
    [SerializeField]
    private bool isPoolObj = true;
	void Start () {
        m_poolObj = GetComponent<PoolObject>();
        m_life = GetComponent<Life>();
        m_drop = GetComponent<IngredientDrop>();
        anim = GetComponentInChildren<Animation>();
	}
	
	void Update () {
        if (m_life.GetLife() > 0)
            return;


        m_drop.Drop();
        if (isPoolObj)
            m_poolObj.Recycle();
        else
            gameObject.SetActive(false);
	}
}
