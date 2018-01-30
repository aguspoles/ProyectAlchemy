using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndingPoint : MonoBehaviour {

    public Transform player_t;
    public Transform walkingPoint;
    public float speed;
    public float minDist;
    public string nextLevel;
    private Animator playerAnim;

    private bool levelFinished = false;

    static private LevelEndingPoint instance = null;

    void Start () {

        walkingPoint.position = new Vector3(walkingPoint.position.x, player_t.position.y, walkingPoint.position.z);
        playerAnim = player_t.gameObject.GetComponentInChildren<Animator>();
    }
	
	void FixedUpdate () {
        if (!levelFinished)
            return;

        player_t.position = Vector3.MoveTowards(player_t.position, walkingPoint.position, speed * Time.fixedDeltaTime);
        if (Vector3.Distance(player_t.position, walkingPoint.position) < minDist)
        {
            player_t.gameObject.layer = LayerMask.NameToLayer("Player");
            playerAnim.SetBool("AutoRun", false);
            SceneManager.LoadScene(nextLevel);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerAnim.SetBool("AutoRun", true);
            levelFinished = true;
            player_t.LookAt(walkingPoint);
            player_t.gameObject.layer = LayerMask.NameToLayer("IgnoreWalls");
        }
    }

    public static LevelEndingPoint GetInstance()
    {
        if (instance == null)
            instance = FindObjectOfType<LevelEndingPoint>();

        return instance;
    }

    public bool isFinishing()
    {
        return levelFinished;
    }
}
