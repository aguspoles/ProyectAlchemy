using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Life {

    private SceneLoader sceneloader;
	Slider m_HealthBar;

	void Awake () {
		m_CurrentHealth = m_MaxLife;
		m_HealthBar = GameObject.FindGameObjectWithTag("SliderHealth").GetComponent<Slider>();
		m_HealthBar.maxValue = m_MaxLife;
        sceneloader = GameObject.Find("SceneManager").GetComponent<SceneLoader>();
	}

	// Update is called once per frame
	protected override void Update ()
	{
        if (!LevelStartingPoint.GetInstance().IsFinished())
            return;

        base.Update();
		LifeUpdate ();
        if (!IsAlive())
        {
            Debug.Log("muerto");
            LifeUpdate();
            //Destroy(gameObject);
            sceneloader.Restart();
            gameObject.SetActive (false);
        }
    }

	override public void DealDamage(float x)
	{
		m_CurrentHealth -= x;
	}

	private void LifeUpdate()
	{
		m_HealthBar.value = m_CurrentHealth;
	}


}