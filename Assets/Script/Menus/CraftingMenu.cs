using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingMenu : MonoBehaviour {

    private Crafting crafting;
    private List<List<CraftingDataBase.IngredientList>> ingList;
    private IngredientButton[] ingButtons;

    private CraftingDataBase.IngredientList firstIng;
    private CraftingDataBase.IngredientList secondIng;
    private bool firstIngLoaded;
    private bool secIngLoaded;
    [SerializeField]
    private RawImage firstImageIng;
    [SerializeField]
    private RawImage secondImageIng;

    public Texture unknowImage;
    public Texture noneImage;

    private CraftingDataBase.PotionsList potToMake;
    [SerializeField]
    private RawImage potImage;

    private void Awake()
    {
        crafting = GameObject.FindGameObjectWithTag("Player").GetComponent<Crafting>();
        ingButtons = GetComponentsInChildren<IngredientButton>();
        firstIngLoaded = false;
    }

    private void Start()
    {
        ingList = crafting.GetIngredientList();
        firstImageIng.gameObject.SetActive(false);
        secondImageIng.gameObject.SetActive(false);
        potImage.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        ingList = crafting.GetIngredientList();

        firstImageIng.gameObject.SetActive(false);
        secondImageIng.gameObject.SetActive(false);
        potImage.gameObject.SetActive(false);

        firstIngLoaded = false;
        secIngLoaded = false;

        SetButtomInformation();
    }

    void Update ()
    {
        if (firstIngLoaded)
            firstImageIng.gameObject.SetActive(true);
        else
            firstImageIng.gameObject.SetActive(false);

        if (secIngLoaded)
            secondImageIng.gameObject.SetActive(true);
        else
            secondImageIng.gameObject.SetActive(false);

        if (firstIngLoaded && secIngLoaded)
        {
            potImage.gameObject.SetActive(true);
            CraftingDataBase.PotionsList pot = CraftingDataBase.GetPotionMadeOf(firstIng, secondIng);
            if (pot != CraftingDataBase.PotionsList.None)
            {
                if (CraftingDataBase.DiscoveredThisPotion(pot))
                    potImage.texture = CraftingDataBase.GetPotionImage(pot);
                else
                    potImage.texture = unknowImage;
            }
            else
                potImage.texture = noneImage;
        }
        else
        {
            potImage.gameObject.SetActive(false);
        }
    }

    private void SetButtomInformation()
    {
        if (ingList != null)
        {
            int restOfButIndex = 0;

            for (int i = 0; i < ingList.Count; i++)
            {
                ingButtons[i].gameObject.SetActive(true);
                ingButtons[i].SetingredientID(ingList[i][0]);
                ingButtons[i].SetCount(ingList[i].Count);
                ingButtons[i].SetName(CraftingDataBase.GetIngredientName(ingList[i][0]));

                restOfButIndex = i + 1;
            }

            for (int i = restOfButIndex; i < ingButtons.Length; i++)
            {
                ingButtons[i].gameObject.SetActive(false);
            }
        }

    }

    public void AddIngredient (CraftingDataBase.IngredientList ing)
    {
        if (!firstIngLoaded)
        {
            firstIng = ing;
            firstIngLoaded = true;
            firstImageIng.texture = CraftingDataBase.GetIngredientImage(ing);
            return;
        }
        secondIng = ing;
        secIngLoaded = true;
        secondImageIng.texture = CraftingDataBase.GetIngredientImage(ing);
    }

    public void CancelSelection()
    {
        for (int i = 0; i < ingButtons.Length; i++)
        {
            ingButtons[i].resetCant();
        }
        firstIngLoaded = false;
        secIngLoaded = false;
    }

    public void Craft()
    {
        if (firstIngLoaded && secIngLoaded)
        {
            crafting.CraftPotion(firstIng, secondIng);
            firstIngLoaded = false;
            secIngLoaded = false;
            SetButtomInformation();
        }
    }
}
