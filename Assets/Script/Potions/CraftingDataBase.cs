using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingDataBase : MonoBehaviour {

    public enum PotionsList
    {
        None = -1,
        FirePotion = 0,
        IceFloor = 1,
		GravPotion = 2,
		HealPotion = 3,
		HastePotion = 4
    }

    public enum IngredientList
    {
        None = -1,
        OmniaStone = 0,
        FireStone = 1,
        Snowflake = 2,
        IceStone = 3,
        MedHerb = 4,
        SpeedFlower = 5,
        MeteoriteShard = 6,
        Ash = 7
    }

    static private PotionsList[,] potionAtlas;

    static public List<Potions> potionsData;
    static public List<Ingredient> ingredientsData;

    static public List<PotionsList> discoveredPotions;

    // Use this for initialization
    void Awake() {

        int dataBaseLayerMask = LayerMask.NameToLayer("CraftingDataBase");
        Transform[] childrens = GetComponentsInChildren<Transform>();
        for (int i = 0; i < childrens.Length; i++)
            childrens[i].gameObject.layer = dataBaseLayerMask;

        potionAtlas = new PotionsList[Ingredient.GetIngredientsCount(), Ingredient.GetIngredientsCount()];
        for (int i = 0; i < potionAtlas.GetLength(0); i++)
        {
            for (int j = 0; j < potionAtlas.GetLength(1); j++)
                potionAtlas[i, j] = PotionsList.None;
        }

        //Combinations
        /*
        //Ash
		potionAtlas[(int)IngredientList.Ash, (int)IngredientList.FireStone] = PotionsList.FirePotion;

        //fireStore
		potionAtlas[(int)IngredientList.FireStone, (int)IngredientList.Ash] = PotionsList.FirePotion;

        //Snowflake
		potionAtlas[(int)IngredientList.Snowflake, (int)IngredientList.IceStone] = PotionsList.IceFloor;

        //IceStone
		potionAtlas[(int)IngredientList.IceStone, (int)IngredientList.Snowflake] = PotionsList.IceFloor;

        //MedHerb
        potionAtlas[(int)IngredientList.MedHerb, (int)IngredientList.MedHerb] = PotionsList.HealPotion;
        potionAtlas[(int)IngredientList.MedHerb, (int)IngredientList.SpeedFlower] = PotionsList.HastePotion;

        //SpeedFlower
        potionAtlas[(int)IngredientList.SpeedFlower, (int)IngredientList.MedHerb] = PotionsList.HastePotion;

        //MeteoriteShard
        potionAtlas[(int)IngredientList.MeteoriteShard, (int)IngredientList.MeteoriteShard] = PotionsList.GravPotion;
        */


        potionAtlas[(int)IngredientList.OmniaStone, (int)IngredientList.FireStone] = PotionsList.FirePotion;
        potionAtlas[(int)IngredientList.FireStone, (int)IngredientList.OmniaStone] = PotionsList.FirePotion;

        potionAtlas[(int)IngredientList.OmniaStone, (int)IngredientList.IceStone] = PotionsList.IceFloor;
        potionAtlas[(int)IngredientList.IceStone, (int)IngredientList.OmniaStone] = PotionsList.IceFloor;

        potionAtlas[(int)IngredientList.OmniaStone, (int)IngredientList.MeteoriteShard] = PotionsList.GravPotion;
        potionAtlas[(int)IngredientList.MeteoriteShard, (int)IngredientList.OmniaStone] = PotionsList.GravPotion;

        potionAtlas[(int)IngredientList.OmniaStone, (int)IngredientList.SpeedFlower] = PotionsList.HastePotion;
        potionAtlas[(int)IngredientList.SpeedFlower, (int)IngredientList.OmniaStone] = PotionsList.HastePotion;

        potionAtlas[(int)IngredientList.OmniaStone, (int)IngredientList.MedHerb] = PotionsList.HealPotion;
        potionAtlas[(int)IngredientList.MedHerb, (int)IngredientList.OmniaStone] = PotionsList.HealPotion;

        potionsData = new List<Potions>();
  
        potionsData.Insert((int)PotionsList.FirePotion, GetComponentInChildren<FireExpPot>());
        potionsData.Insert((int)PotionsList.IceFloor, GetComponentInChildren<IceFloorPot>());
        potionsData.Insert((int)PotionsList.GravPotion, GetComponentInChildren<GravPot>());
        potionsData.Insert((int)PotionsList.HealPotion, GetComponentInChildren<HealPotion>());
        potionsData.Insert((int)PotionsList.HastePotion, GetComponentInChildren<HastePotion>());

        ingredientsData = new List<Ingredient>();

        ingredientsData.Insert((int)IngredientList.OmniaStone, GetComponentInChildren<OmniaStone>());
        ingredientsData.Insert((int)IngredientList.FireStone, GetComponentInChildren<FireStone>());
        ingredientsData.Insert((int)IngredientList.Snowflake, GetComponentInChildren<Snowflake>());
        ingredientsData.Insert((int)IngredientList.IceStone, GetComponentInChildren<IceStone>());
        ingredientsData.Insert((int)IngredientList.MedHerb, GetComponentInChildren<MedHerb>());
        ingredientsData.Insert((int)IngredientList.SpeedFlower, GetComponentInChildren<SpeedFlower>());
        ingredientsData.Insert((int)IngredientList.MeteoriteShard, GetComponentInChildren<MeteoriteShard>());
        ingredientsData.Insert((int)IngredientList.Ash, GetComponentInChildren<Ash>());

        GameObject.Find("PotionsData").SetActive(false);
        GameObject.Find("IngredientsData").SetActive(false);

        discoveredPotions = new List<PotionsList>();



        for (int i = 0; i < potionsData.Count; i++)
        {
            potionsData[i].GetCode();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    static public PotionsList GetPotionMadeOf(IngredientList ingA, IngredientList ingB) {
        return potionAtlas[(int)ingA, (int)ingB];
    }

    static public void ActivatePotion(PotionsList pot, Vector3 pos)
    {
        switch (pot)
        {
            case PotionsList.FirePotion:
                PoolManager.GetInstance().GetPool("FirePool").GetPooledObject(pos);
                break;
            case PotionsList.IceFloor:
                PoolManager.GetInstance().GetPool("IcePool").GetPooledObject(pos);
                break;
			case PotionsList.GravPotion:
				PoolManager.GetInstance ().GetPool ("GravPool").GetPooledObject (pos);
				break;
			case PotionsList.HealPotion:
				PoolManager.GetInstance ().GetPool ("HealPotionPool").GetPooledObject (pos);
				break;
			case PotionsList.HastePotion:
				PoolManager.GetInstance ().GetPool ("HastePotionPool").GetPooledObject (pos);
				break;
			default:
                break;
        }
    }

    static public string GetPotionName(PotionsList potion)
    {
        return potionsData[(int)potion].GetName();
    }

    static public Texture GetPotionImage(PotionsList potion)
    {
        return potionsData[(int)potion].GetImage();
    }

    static public int GetPotionCode(string name)
    {
        for (int i = 0; i < potionsData.Count; i++)
        {
            if (potionsData[i].GetName() == name)
                return potionsData[i].GetCode();
        }

        return -1;
    }

    static public string GetIngredientName(IngredientList ing)
    {
        return ingredientsData[(int)ing].GetName();
    }

    static public Texture GetIngredientImage(IngredientList ing)
    {
        return ingredientsData[(int)ing].GetImage();
    }

    static public bool HasIndicator(PotionsList potion)
    {
        return potionsData[(int)potion].HasIndicator();
    }

    static public bool HasIndicator(int potion)
    {
        return potionsData[potion].HasIndicator();
    }

    static public bool DiscoverPotion(PotionsList pot)
    {
        if (!discoveredPotions.Contains(pot))
        {
            discoveredPotions.Add(pot);
            return true;
        }
        return false;
    }

    static public bool DiscoveredThisPotion(PotionsList pot)
    {
        return discoveredPotions.Contains(pot);
    }
}
