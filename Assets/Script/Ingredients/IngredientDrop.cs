using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientDrop : MonoBehaviour {

    [SerializeField]
    private string m_poolName;
    private Pool m_ingPool;
    [SerializeField]
    private float m_cant;

    private Life m_life;
    private bool m_droped;

    private void Start()
    {
        m_ingPool = GameObject.Find(m_poolName).GetComponent<Pool>();
    }

    public void Drop()
    {
        for (int i = 0; i < m_cant; i++)
        {

            m_ingPool = GameObject.Find(m_poolName).GetComponent<Pool>();
            PoolObject aux = m_ingPool.GetPooledObject();
            aux.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            aux.gameObject.GetComponent<Ingredient>().Jump();
        }
    }  
}
