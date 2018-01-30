using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionsSelectorHUD : MonoBehaviour {

    private PotSelectImg[] m_potsImg;

    private RawImage m_mainPotImg;
    private RawImage m_leftPotImg;
    private RawImage m_rightPotImg;
    private RawImage m_auxPotImg;
    private RawImage m_aux2PotImg;

    private List<List<CraftingDataBase.PotionsList>> m_potsList;

    private PotionBag m_pots;

    private int m_activeBag;

    private bool m_initilized;
    private bool m_alreadySet;

    void Awake()
    {
        m_potsImg = new PotSelectImg[5];

        m_potsImg[0] = GameObject.Find("Img1").GetComponent<PotSelectImg>();
        m_potsImg[1] = GameObject.Find("Img2").GetComponent<PotSelectImg>();
        m_potsImg[2] = GameObject.Find("Img3").GetComponent<PotSelectImg>();
        m_potsImg[3] = GameObject.Find("Img4").GetComponent<PotSelectImg>();
        m_potsImg[4] = GameObject.Find("Img5").GetComponent<PotSelectImg>();

        m_pots = GameObject.FindGameObjectWithTag("Player").GetComponent<PotionBag>();
        m_potsList = m_pots.GetPotiosBag();

        m_initilized = false;
        m_alreadySet = false;
    }

    void Update()
    {
        if (!m_initilized)
            InitializePotionsImage();
    }

    public void InitializePotionsImage()
    {
        m_potsList = m_pots.GetPotiosBag();
        m_activeBag = m_pots.GetActivePot();

        if (m_potsList.Count > 0)
        {
            for (int i = 0; i < m_potsImg.Length; i++)
            {
                m_potsImg[i].gameObject.SetActive(true);
            }

            m_activeBag = m_pots.GetActivePot();
            int leftBag = (m_activeBag - 1 < 0) ? m_potsList.Count - (0 - (m_activeBag - 1)) : m_activeBag - 1;
            int rightBag = (m_activeBag + 1 > m_potsList.Count - 1) ? 0 + ((m_activeBag + 1) - m_potsList.Count) : m_activeBag + 1;
            int auxBag = (m_activeBag - 2 < 0) ? m_potsList.Count - (0 - (m_activeBag - 2)) : m_activeBag - 2;
            int auxBag2 = (m_activeBag + 2 > m_potsList.Count - 1) ? 0 + ((m_activeBag + 2) - m_potsList.Count) : m_activeBag + 2;

            if (m_potsList.Count <= 1)
            {
                auxBag = 0;
                auxBag2 = 0;
            }

            for (int i = 0; i < m_potsImg.Length; i++)
            {
                switch (m_potsImg[i].GetPos())
                {
                    case 1:
                        m_potsImg[i].SetImage(CraftingDataBase.GetPotionImage(m_potsList[m_activeBag][0]));
                        break;
                    case 2:
                        m_potsImg[i].SetImage(CraftingDataBase.GetPotionImage(m_potsList[rightBag][0]));
                        break;
                    case 3:
                        m_potsImg[i].SetImage(CraftingDataBase.GetPotionImage(m_potsList[auxBag2][0]));
                        break;
                    case 4:
                        m_potsImg[i].SetImage(CraftingDataBase.GetPotionImage(m_potsList[auxBag][0]));
                        break;
                    case 5:
                        m_potsImg[i].SetImage(CraftingDataBase.GetPotionImage(m_potsList[leftBag][0]));
                        break;
                    default:
                        break;
                }
            }
        }
        else if (m_potsList.Count == 0)
        {
            for (int i = 0; i < m_potsImg.Length; i++)
            {
                m_potsImg[i].gameObject.SetActive(false);
            }
        }


        m_initilized = true;
        m_alreadySet = true;
    }

    public void MoveNext()
    {
        if (!m_alreadySet)
        {
            for (int i = 0; i < m_potsImg.Length; i++)
            {
                if (m_potsImg[i].GetPos() == 3)
                    UpdateFourthImage(m_potsImg[i]);
                else if (m_potsImg[i].GetPos() == 4)
                    UpdateThirdImage(m_potsImg[i]);
            }
        }

        for (int i = 0; i < m_potsImg.Length; i++)
        {
            m_potsImg[i].GoToNext();
        }

        m_alreadySet = false;
    }

    public void MovePrev()
    {
        if (!m_alreadySet)
        {
            for (int i = 0; i < m_potsImg.Length; i++)
            {
                if (m_potsImg[i].GetPos() == 3)
                    UpdateFourthImage(m_potsImg[i]);
                else if (m_potsImg[i].GetPos() == 4)
                    UpdateThirdImage(m_potsImg[i]);
            }
        }

        for (int i = 0; i < m_potsImg.Length; i++)
        {
            m_potsImg[i].GoToPrev();
        }

        m_alreadySet = false;
    }

    private void UpdateThirdImage(PotSelectImg potImg)
    {
        if (m_potsList.Count == 1)
        {
            potImg.SetImage(CraftingDataBase.GetPotionImage(m_potsList[0][0]));
            return;
        }
        m_potsList = m_pots.GetPotiosBag();
        m_activeBag = m_pots.GetActivePot();

        int num = (m_activeBag - 2 < 0) ? m_potsList.Count - (0 - (m_activeBag - 2)) : m_activeBag - 2;
        potImg.SetImage(CraftingDataBase.GetPotionImage(m_potsList[num][0]));
    }

    private void UpdateFourthImage(PotSelectImg potImg)
    {
        if (m_potsList.Count == 1)
        {
            potImg.SetImage(CraftingDataBase.GetPotionImage(m_potsList[0][0]));
            return;
        }
        m_potsList = m_pots.GetPotiosBag();
        m_activeBag = m_pots.GetActivePot();
         
        int num = (m_activeBag + 2 > m_potsList.Count - 1) ? 0 + ((m_activeBag + 2) - m_potsList.Count) : m_activeBag + 2;
        potImg.SetImage(CraftingDataBase.GetPotionImage(m_potsList[num][0]));
    }

    private void CheckIfEmpty()
    {
        m_potsList = m_pots.GetPotiosBag();
        if (m_potsList.Count == 0)
        {
            m_potsImg[0].SetImage(null);
            m_potsImg[1].SetImage(null);
            m_potsImg[2].SetImage(null);
            m_potsImg[3].SetImage(null);
            m_potsImg[4].SetImage(null);
        }
    }
}
