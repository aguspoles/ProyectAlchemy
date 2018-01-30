using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : MonoBehaviour {

	[SerializeField]
	private int count = 3;
	private ParticleSystem[] ps;
	private Light light;
	public bool activated = false;
	private GameObject checkPoint;

	private float duration = 2.0F;
	private Color color0 = Color.red;
	private Color color1 = Color.blue;

	void Awake () {
		ps = gameObject.GetComponentsInChildren<ParticleSystem> ();
		light = transform.Find ("Point light").GetComponent<Light> ();
		checkPoint = GameObject.Find ("CheckPoint");
	}
	
	// Update is called once per frame
	void Update () {
		if (count <= 0 && activated == false) {
			Activate ();
		} 
		else if (activated == true) {
			float t = Mathf.PingPong(Time.time, duration) / duration;
			light.color = Color.Lerp(color0, color1, t);
		}
	}

	private void Activate() {
		activated = true;
		for (int i = 0; i < ps.Length; i++) {
			var mainModule = ps[i].main;
			mainModule.startSize = new ParticleSystem.MinMaxCurve (10.0f, 15.0f);
		}
	    checkPoint.GetComponent<CheckPoint> ().checks--;
		FindObjectOfType<AudioManager> ().Play ("PilarActivation");
	    StartCoroutine (Effect ());
	}

	void OnTriggerStay(Collider other){
		if (other.tag == "Enemy" && activated == false) {
			Life life = other.GetComponent<Life> ();
			if (life.GetLife() <= 0)
				count--;
		}
	}

	IEnumerator Effect(){
		for (int i = 0; i < ps.Length; i++) {
			var mainModule = ps [i].main;
			mainModule.startSize = new ParticleSystem.MinMaxCurve (10.0f, 15.0f);
		}
		yield return new WaitForSeconds (2);
		for (int i = 0; i < ps.Length; i++) {
			ps [i].Stop ();	
		}
	}
		
}
