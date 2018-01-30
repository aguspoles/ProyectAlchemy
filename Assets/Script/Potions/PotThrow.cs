using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotThrow : MonoBehaviour {

    public float firingAngle;
    private GameObject effect;
    private GameObject glass;
    private Transform playerT;
    private Transform mousePoint;
    private float elapsedTime = 0;
    private bool onFloor;
    private float Vx;
    private float Vy;
    private float gravity;
    private float timer = 0;

    private void OnEnable()
    {
        if (!effect)
            effect = GetComponentInChildren<FireExpPot>().gameObject;
        if (!glass)
            glass = transform.Find("Glass").gameObject;
        if (!playerT)
            playerT = GameObject.FindGameObjectWithTag("Player").transform;

        onFloor = false;     
        glass.SetActive(true);

        mousePoint = GameObject.Find("MousePoint").transform;
        transform.position = mousePoint.position;

        float target_Distance = Vector3.Distance(transform.position, mousePoint.position);

        gravity = Physics.gravity.magnitude;

        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        float flightDuration = target_Distance / Vx;
       
        transform.rotation = Quaternion.LookRotation(mousePoint.position - transform.position);
    }

    void Update () {
        if (onFloor)
        {
            timer += Time.deltaTime;
            if (timer >= 3)
            {
                gameObject.GetComponent<PoolObject>().Recycle();
                return;
            }
            return;
        }
           

        transform.Translate(0, (Vy - (gravity * elapsedTime)) * Time.deltaTime, Vx * Time.deltaTime);
        elapsedTime += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor")
        {
            effect.SetActive(true);
            glass.SetActive(false);
            onFloor = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            effect.SetActive(true);
            glass.SetActive(false);
            onFloor = true;
        }
    }
}
