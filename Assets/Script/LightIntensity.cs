using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensity : MonoBehaviour {

	private Light light;
	private bool flag = true;
	private float lightIntensity;
	private float num;
	public float changeFactor;
	public float min;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light> ();
		num = light.intensity;
		lightIntensity = light.intensity;
	}
	
	// Update is called once per frame
	void Update () {
		if (num <= lightIntensity && flag) 
			num += changeFactor;
		else
			flag = false;
		if (num >= 0 && !flag && num >= min)
			num -= changeFactor;
		else
			flag = true;
		
		light.intensity = num;
	}

	public void SetLightIntensity(float num){
		lightIntensity += num;
	}
}
