using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPickUpBox : MonoBehaviour {
    private Ingredient ingredient;

    private void Start()
    {
        ingredient = GetComponentInParent<Ingredient>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponentInParent<Ingredient>().GoToPlayer();
        }
    } 
}
