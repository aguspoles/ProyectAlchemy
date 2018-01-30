using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilePot : MonoBehaviour {

    private Transform shootPos;
    public string poolName;
    public float speed;

    private Pool pool;
    private PoolObject effect;
    private Vector3 mousePos;
    private Vector3 dir;
    
    AudioManager audioManager;

    void Start () {

    }

    private void OnEnable()
    {
        //Debug.Break();
        mousePos = GameObject.Find("MousePoint").transform.position;
        pool = PoolManager.GetInstance().GetPool(poolName);
        shootPos = GameObject.Find("ShootPoint").transform;
        audioManager = FindObjectOfType<AudioManager>();
        //mousePos.y = shootPos.position.y - 1.5f;
        transform.position = shootPos.position;
        dir = mousePos - transform.position;
        transform.rotation = Quaternion.LookRotation(dir.normalized);
    }

    void Update () {
        transform.position += this.transform.forward * speed * Time.deltaTime; 
        //Vector3.MoveTowards(transform.position, mousePos, speed * Time.deltaTime);       
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor")
        {
            audioManager.Play("GlassBreak");
            effect = pool.GetPooledObject();
            effect.transform.position = transform.position;
            GetComponent<PoolObject>().Recycle();
        }
    }   
}
