using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager instance = null;

    private Dictionary<string, Pool> pools = new Dictionary<string, Pool>();

    public static PoolManager GetInstance()
    {
        if (instance == null)
            instance = FindObjectOfType<PoolManager>();

        return instance;
    }

    // Use this for initialization
    void Awake()
    {
        instance = this;

        Pool[] ps = GetComponentsInChildren<Pool>();

        foreach (Pool p in ps)
        {
            if (pools.ContainsKey(p.gameObject.name))
            {
                Debug.LogError("El pool " + p.gameObject.name + " ya existe.");
            }
            else
            {
                pools[p.gameObject.name] = p;
            }
        }
    }

    public Pool GetPool(string name)
    {
        if (pools.ContainsKey(name))
            return pools[name];

        return null;
    }

}
