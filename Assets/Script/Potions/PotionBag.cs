using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionBag : MonoBehaviour {

    int activeBag;
    private int maxBags;
    List<List<CraftingDataBase.PotionsList>> potsBag;
    private PotionsSelectorHUD potsHUD;
    Pool pool;
    private MousePoint m_mousePoint;
    private Animator anim;

	void Start ()
    {
        activeBag = 0;

        potsBag = new List<List<CraftingDataBase.PotionsList>>();
        potsHUD = GameObject.Find("PotionsHUD").GetComponent<PotionsSelectorHUD>();
        anim = GetComponentInChildren<Animator>();
        m_mousePoint = GameObject.Find("MousePoint").GetComponent<MousePoint>();


        //AddPotion(CraftingDataBase.PotionsList.FirePotion);

       
    }

    void Update ()
    {
        if (!LevelStartingPoint.GetInstance().IsFinished() || LevelEndingPoint.GetInstance().isFinishing())
            return;

        CheckForEmpthyBags();
        if (potsBag.Count > 0)
            inputMannager();


    }

    void inputMannager()
    {
        maxBags = potsBag.Count - 1;

        if (Input.GetKeyDown(KeyCode.E))
        {
            potsHUD.MoveNext();
            activeBag--;
            if (activeBag < 0)
                activeBag = maxBags;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            potsHUD.MovePrev();
            activeBag++;
            if (activeBag > maxBags)
                activeBag = 0;   
        }
			 
        if (Input.GetMouseButton(1) && potsBag.Count > 0) 
        {
            m_mousePoint.ActivateIndicator((int)potsBag[activeBag][0]);
        }
        if (Input.GetMouseButtonUp(1) && potsBag.Count > 0)
        {
            m_mousePoint.DeactivateIndicator();
            if (potsBag[activeBag].Count > 0)
            {
                Vector3 pos = GameObject.Find("ShootPoint").transform.position;

                anim.SetBool("Attacking", true);
                CraftingDataBase.ActivatePotion(potsBag[activeBag][0], pos);
                potsBag[activeBag].RemoveAt(0);
            }
        }
    }

    public bool AddPotion(CraftingDataBase.PotionsList potion)
    {
        if (potion == CraftingDataBase.PotionsList.None)
            return false;

        for (int i = 0; i < potsBag.Count; i++)
        {
            if (potsBag[i][0] == potion)
            {
                potsBag[i].Add(potion);
                potsHUD.InitializePotionsImage();
                return true;
            }
        }

        List<CraftingDataBase.PotionsList> aux = new List<CraftingDataBase.PotionsList>();
        aux.Add(potion);
        potsBag.Add(aux);
        potsHUD.InitializePotionsImage();
        return true;
    }

    private void CheckForEmpthyBags()
    {
        for (int i = 0; i < potsBag.Count; i++)
        {
            if (potsBag[i].Count == 0)
            {
                if (activeBag == potsBag.Count - 1)
                {
                    activeBag = 0;
                }
                potsBag.RemoveAt(i);
                potsHUD.InitializePotionsImage();
            }
        }
    }

    public int GetActivePot()
    {
        return activeBag;
    }

    public List<List<CraftingDataBase.PotionsList>> GetPotiosBag()
    {
        return potsBag;
    }
}
