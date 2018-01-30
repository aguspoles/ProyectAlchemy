using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusManager : MonoBehaviour {

    [SerializeField]
    private GameObject craftingMenu;
    [SerializeField]
    private GameObject HUD;
    [SerializeField]
    private GameObject pauseMenu;

	// Use this for initialization
	void Start () {
        pauseMenu.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!craftingMenu.activeSelf)
            {
                Cursor.visible = true;
                craftingMenu.SetActive(true);
                HUD.SetActive(false);
                Time.timeScale = 0;
            }
            else
            {
                Cursor.visible = false;
                craftingMenu.SetActive(false);
                HUD.SetActive(true);
                Time.timeScale = 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu.activeSelf)
            {
                Cursor.visible = true;
                pauseMenu.SetActive(true);
                HUD.SetActive(false);
                Time.timeScale = 0;
            }
            else
            {
                Cursor.visible = false;
                pauseMenu.SetActive(false);
                HUD.SetActive(true);
                Time.timeScale = 1;
            }
        }
	}

    public void ResumeGame()
    {
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        HUD.SetActive(true);
        Time.timeScale = 1;
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    } 

    public void Exit()
    {
        Application.Quit(); 
    }

}
