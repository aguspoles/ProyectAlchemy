using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionIndicator : MonoBehaviour {

    public string potionName;
    private int Code; 

	public void SetCode () {
        Code = CraftingDataBase.GetPotionCode(potionName);
	}
	
    public int GetCode()
    {
        return Code;
    }
}
