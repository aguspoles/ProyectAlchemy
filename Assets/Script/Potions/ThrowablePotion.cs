using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowablePotion : Potions {

    private GameObject mousePoint;
    public float floorOffset;

    // Use this for initialization
    protected override void OnEnable () {
        base.OnEnable();
        accumTime = 0.0f;
        mousePoint = GameObject.Find("MousePoint");
        //transform.position = mousePoint.transform.position;
        //transform.position = new Vector3(transform.position.x, GameObject.Find("Floor").transform.position.y + floorOffset, transform.position.z);
    }

    protected override void Update()
    {
        base.Update();
        accumTime += Time.deltaTime;
        if (accumTime >= timeOfEffect)
        {
            gameObject.GetComponent<PoolObject>().Recycle();
            return;
        }
    }
}
