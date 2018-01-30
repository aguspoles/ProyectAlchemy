using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	[SerializeField]
	float m_Vida = 3;

    [SerializeField]
    GameObject spawn;
    [SerializeField]
    GameObject spawn2;

    GameObject player;

    public float currentHealth;

    bool isDead;


    void Awake ()
    {
		currentHealth = m_Vida;

        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update ()
    {
       
    }


    public void TakeDamage (float amount)
    {
        if(isDead)
            return;

        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    public void Death ()
    {
        isDead = true;

		Instantiate (gameObject, spawn.transform.position, spawn.transform.rotation);
		Instantiate (gameObject, spawn2.transform.position, spawn2.transform.rotation);
	

        player.GetComponent<Crafting>().PickUpIngredient(CraftingDataBase.IngredientList.Ash, 1);
        player.GetComponent<Crafting>().PickUpIngredient(CraftingDataBase.IngredientList.FireStone, 1);
        player.GetComponent<Crafting>().PickUpIngredient(CraftingDataBase.IngredientList.Snowflake, 1);
        player.GetComponent<Crafting>().PickUpIngredient(CraftingDataBase.IngredientList.IceStone, 1);

        Destroy (gameObject);
    }
}
