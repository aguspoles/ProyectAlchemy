using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RollCredits : MonoBehaviour {

    public Transform credits;
    public Transform endPoint;
    public float speed;
	void Start () {
		
	}

    void Update() {
        credits.Translate(Vector3.up * speed * Time.deltaTime);

        if (endPoint.position.y >= 0 || Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");
	}
}
