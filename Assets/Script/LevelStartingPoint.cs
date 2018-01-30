using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartingPoint : MonoBehaviour {

    public Transform player_t;
    public Transform entryPoint;
    public Transform walkingPoint;
    public float speed;
    public float minDist;
    private Animator playerAnim;

    private bool finished = false;
    static private LevelStartingPoint instance = null;

    void Start () {
        finished = false;
        player_t.LookAt(walkingPoint);
        player_t.position = entryPoint.position;
        player_t.gameObject.layer = LayerMask.NameToLayer("IgnoreWalls");
        walkingPoint.position = new Vector3(walkingPoint.position.x, player_t.position.y, walkingPoint.position.z);
        playerAnim = player_t.gameObject.GetComponentInChildren<Animator>();

        playerAnim.SetBool("AutoRun", true);
    }
	
	void FixedUpdate () {
        if (finished)
            return;

        player_t.position = Vector3.MoveTowards(player_t.position, walkingPoint.position, speed * Time.fixedDeltaTime);

        if (Vector3.Distance(player_t.position, walkingPoint.position) < minDist)
        {
            player_t.gameObject.layer = LayerMask.NameToLayer("Player");
            playerAnim.SetBool("AutoRun", false);
            finished = true;
        }
	}

    public bool IsFinished()
    {
        return finished;
    }

    public static LevelStartingPoint GetInstance()
    {
        if (instance == null)
            instance = FindObjectOfType<LevelStartingPoint>();

        return instance;
    }
}
