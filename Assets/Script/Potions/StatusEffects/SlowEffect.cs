using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : MonoBehaviour {

    private Movement movement;
    private float slowAmount;
    private float duration;
    private float timer;
    private float originalSpeed;
    // Use this for initialization
    void Start() {
        timer = 0;

        movement = gameObject.GetComponent<Movement>();

        originalSpeed = movement.GetSpeed();
        movement.SetSpeed(originalSpeed / slowAmount);
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            movement.SetSpeed(originalSpeed);
            Destroy(this);
        }
    }

    public void SetDuration(float dur)
    {
        duration = dur;
    }

    public void SetSlowAmount(float percent)
    {
        slowAmount = percent;
    }

    private void OnDisable()
    {
        movement.SetSpeed(originalSpeed);
    }

    private void OnDestroy()
    {
        movement.SetSpeed(originalSpeed);
    }
}
