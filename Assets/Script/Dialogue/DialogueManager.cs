using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	private static DialogueManager instance;

	private Queue<string> sentences;

	private Text nameText;
	private Text dialogueText;

	private Animator animator;

	void Awake(){
		if (instance == null)
			instance = this;
		else {
			Destroy (gameObject);
			return;
		}

		GameObject dialogue = GameObject.Find ("Dialogue");
		nameText = dialogue.transform.Find ("Name text").GetComponent<Text> ();
		dialogueText = dialogue.transform.Find ("Body text").GetComponent<Text> ();

		animator = dialogue.GetComponent<Animator> ();
	}
		
	void Start () {
		sentences = new Queue<string> ();
	}

	void Update () {
	}

	public void StartDialogue(Dialogue d){
		Cursor.visible = true;
		animator.SetBool ("IsOpen", true);

		nameText.text = d.name;

		sentences.Clear ();

		foreach (string sentence in d.sentences) {
			sentences.Enqueue (sentence);
		}
		DisplayNextSentence ();
	}

	public void DisplayNextSentence(){
		if (sentences.Count == 0) {
			EndDialogue ();
			return;
		}

		string sentence = sentences.Dequeue ();
		StopAllCoroutines ();
		StartCoroutine (TypeSentence (sentence));
	}

	public void EndDialogue(){
		animator.SetBool ("IsOpen", false);
		Cursor.visible = false;
	}

	//show sentence letter by letter, one letter per frame 
	IEnumerator TypeSentence(string sentence){
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray()) {
			dialogueText.text += letter;
			yield return null;
		}
	}
}
