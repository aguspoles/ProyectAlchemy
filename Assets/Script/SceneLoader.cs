using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    
	GameObject howToPlayObject;
	Animator fadeAnimator;
 
    void Start()
    {
		fadeAnimator = GameObject.Find ("Fade").GetComponent<Animator> ();
		howToPlayObject = GameObject.Find ("HowToPlayBG");
		if(howToPlayObject)
			howToPlayObject.SetActive (false);
    }

	void Update ()
    {
	}

    public void ChangeScene(string sceneName)
    {
		SceneManager.LoadScene(sceneName);
    }

	public void ExitGame()
	{
		Application.Quit ();
		Debug.Log ("quit");
	}

	public void HowToPlayScreen(bool setActiveGO)
	{
		howToPlayObject.SetActive (setActiveGO);
	}

    public void Restart()
    {
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
		if(fadeAnimator)
			fadeAnimator.SetBool ("fade", true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}