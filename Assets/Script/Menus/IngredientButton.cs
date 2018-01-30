using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientButton : MonoBehaviour {

    private CraftingDataBase.IngredientList ingredientID;
    private Text nameText;
    private Text countText;
    private CraftingMenu menu;
    private Button but;
    private bool initialized = false;
    private float m_cant;
    private float m_maxCant;
	void Awake () {
        Text[] texts = GetComponentsInChildren<Text>();
        if (texts[0].gameObject.name == "Name")
        {
            nameText = texts[0];
            countText = texts[1];
        }
        else
        {
            nameText = texts[1];
            countText = texts[0];
        }
        but = GetComponent<Button>();
        but.onClick.AddListener(Clicked);
        menu = GetComponentInParent<CraftingMenu>();
	}

    // Update is called once per frame
    void Update () {
		
	}

    public void SetingredientID(CraftingDataBase.IngredientList ing)
    {
        ingredientID = ing;
    }

    public void SetName(string _text)
    {
        nameText.text = _text;
    }
    
    public void SetCount (int cant)
    {
        m_maxCant = cant;
        m_cant = cant;
        countText.text = cant.ToString();
    }

    public void Clicked()
    {
        if (m_cant >= 1)
        {
            menu.AddIngredient(ingredientID);
            m_cant--;
        }
    }

    public void resetCant()
    {
        m_cant = m_maxCant;
    }
}
