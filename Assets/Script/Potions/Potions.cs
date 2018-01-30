using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potions : MonoBehaviour {

    [SerializeField]
    protected string PotionName;
    [SerializeField]
    protected Texture image;
    [SerializeField]
    protected float timeOfEffect;
    protected float accumTime = 0.0f;
    protected int Code;
    [SerializeField]
    protected bool hasIndicator;

    // Use this for initialization
    void Awake () {
        
	}
	
    protected virtual void OnEnable()
    {
    }

	// Update is called once per frame
	protected virtual void Update () {
    }

    public virtual int GetCode()
    {
        return Code;
    }

    public virtual void Effect(GameObject target) { }

    public string GetName()
    {
        return PotionName;
    }

    public Texture GetImage()
    {
        return image;
    }

    public void SetName(string _name)
    {
        PotionName = _name;
    }

    public bool HasIndicator()
    {
        return hasIndicator;
    }
}
