using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour {

    [SerializeField]
    protected float speed;
    protected bool moveImpediment;
    protected Rigidbody rigidB;
    // Use this for initialization
    virtual protected void Start() {
        moveImpediment = false;
        rigidB = GetComponent<Rigidbody>();
    }

    public void Move()
    {
   
        if (!moveImpediment)
            SpecificMovement();
    }

    public virtual void SpecificMovement()
    {

    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetAbilityToMove(bool move)
    {
        moveImpediment = move;
    }

    public bool CanMove()
    {
        return moveImpediment;
    }

    public void Push(float force, Vector3 dir)
    {
        Vector3 pos =  transform.position - dir;
        pos.y = 0;
        pos.Normalize();
        //rigidB.AddForce(pos * force, ForceMode.Force);
    }
}
