using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inspect : MonoBehaviour {

	public Text inspectText;
	private DialogueTrigger trigger;

	// Use this for initialization
	void Start () {
		trigger = GetComponent<DialogueTrigger> ();
	}

	void Update () {
	}

	void OnTriggerEnter(Collider other){
		if (other.name == "Player") {
			inspectText.gameObject.SetActive (true);
		}
	}

	void OnTriggerExit(Collider other){
		if (other.name == "Player") {
			FindObjectOfType<DialogueManager> ().EndDialogue ();
			inspectText.gameObject.SetActive (false);
		}
	}

	void OnTriggerStay(){
		if (Input.GetKeyDown (KeyCode.R) && inspectText.gameObject.activeSelf) {
			trigger.TriggerDialogue ();
		}
	}
}
