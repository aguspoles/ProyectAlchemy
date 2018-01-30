using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//test
using UnityEngine.UI;

public class Crafting : MonoBehaviour {

    private List<List<CraftingDataBase.IngredientList>> ingredientsBag;

    private PotionBag potionBag;

    // Use this for initialization
    void Start ()
    {
        potionBag = GetComponent<PotionBag>();

        ingredientsBag = new List<List<CraftingDataBase.IngredientList>>();

        //Test
        //PickUpIngredient(CraftingDataBase.IngredientList.Ash, 2);
        //PickUpIngredient(CraftingDataBase.IngredientList.FireStone, 2);
        //PickUpIngredient(CraftingDataBase.IngredientList.Snowflake, 2);
        //PickUpIngredient(CraftingDataBase.IngredientList.IceStone, 2);
    }
	
	// Update is called once per frame
	void Update ()
    {
       
    }

    bool CheckfIfIngAvailable(CraftingDataBase.IngredientList ingCode)
    {
        for (int i = 0; i < ingredientsBag.Count; i++)
        {
            if (ingredientsBag[i][0] == ingCode)
                return true;
        }
        return false;
    }

    public void PickUpIngredient(CraftingDataBase.IngredientList ingCode, int cant)
    {
        for (int i = 0; i < ingredientsBag.Count; i++)
        {
            if (ingredientsBag[i][0] == ingCode)
            {
                for (int j = 0; j < cant; j++)
                    ingredientsBag[i].Add(ingCode);
                return;
            }
        }
        List<CraftingDataBase.IngredientList> listAux = new List<CraftingDataBase.IngredientList>();
        for (int i = 0; i < cant; i++)
            listAux.Add(ingCode);
        ingredientsBag.Add(listAux);
    }

    public void RemoveIngredient(CraftingDataBase.IngredientList ingCode)
    {
        for (int i = 0; i < ingredientsBag.Count; i++)
        {
            if (ingredientsBag[i][0] == ingCode)
            {
                ingredientsBag[i].RemoveAt(0);
                if (ingredientsBag[i].Count == 0)
                    ingredientsBag.RemoveAt(i);
                return;
            }
        }
        return;
    }

    public List<List<CraftingDataBase.IngredientList>> GetIngredientList()
    {
        return ingredientsBag;
    }

    public void CraftPotion(CraftingDataBase.IngredientList ingA, CraftingDataBase.IngredientList ingB)
    {
        if (potionBag.AddPotion((CraftingDataBase.GetPotionMadeOf(ingA, ingB))))
        {
            RemoveIngredient(ingA);
            RemoveIngredient(ingB);
            CraftingDataBase.DiscoverPotion((CraftingDataBase.GetPotionMadeOf(ingA, ingB)));
        }
    }
}
