using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour {

    [SerializeField]
    protected string ingredientName;
    [SerializeField]
    protected Texture image;
    protected CraftingDataBase.IngredientList ingCode;

    private Transform m_playerT;
    private Rigidbody rigidB;
    private float m_angle = 50;

    private float m_radius = 5;

    private float m_speed = 10f;
    private float m_accel = 1.5f;
    private bool m_goingToPlayer = false;


    AudioManager audioManager;

    void Start () {
        m_playerT = GameObject.FindGameObjectWithTag("Player").transform;
        audioManager = FindObjectOfType<AudioManager>();
    }
	
	void FixedUpdate () {
        if (m_goingToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_playerT.position, m_speed * Time.deltaTime);
            m_speed += m_accel;
        }	
	}

    public string GetName()
    {
        return ingredientName;
    }

    public Texture GetImage()
    {
        return image;
    }

    public void SetName(string _name)
    {
        ingredientName = _name;
    }

    static public int GetIngredientsCount()
    {
        return System.Enum.GetValues(typeof(CraftingDataBase.IngredientList)).Length;
    }

    public void Jump()
    {
        rigidB = GetComponent<Rigidbody>();

        Vector3 destPos = Random.insideUnitSphere * m_radius + transform.position;

        float gravity = Physics.gravity.magnitude;

        float angle = m_angle * Mathf.Deg2Rad;

        Vector3 planarDest = new Vector3(destPos.x, 0, destPos.z);
        Vector3 planarPos = new Vector3(transform.position.x, 1, transform.position.z);

        float distance = Vector3.Distance(planarDest, planarPos);

        float yOffset = transform.position.y - planarPos.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 vel = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarDest - planarPos) * (destPos.x > transform.position.x ? 1 : -1);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * vel;

        rigidB.velocity = finalVelocity;
    }

    public void GoToPlayer()
    {
        rigidB = GetComponent<Rigidbody>();
        rigidB.AddForce(new Vector3(0, 500, 0));
        m_playerT = GameObject.FindGameObjectWithTag("Player").transform;
        m_goingToPlayer = true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioManager.Play("PickUp");
            collision.gameObject.GetComponent<Crafting>().PickUpIngredient(ingCode, 1);
            gameObject.GetComponent<PoolObject>().Recycle();
        }
    }

    private void OnEnable()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
}
