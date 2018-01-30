using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPotionDetecter : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {		
		if (other.gameObject.tag == "Potion")
        {
			other.GetComponent<Potions>().Effect(gameObject);
        }
    }
}
