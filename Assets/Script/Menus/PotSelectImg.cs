using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotSelectImg : MonoBehaviour {

    private RawImage image;

    private Transform pos1;
    private Transform pos2;
    private Transform pos3;
    private Transform pos4;
    private Transform pos5;

    private Transform next;
    private Transform prev;

    private Transform dest;

    [SerializeField]
    private int pos;

    [SerializeField]
    private bool isOutScreen;

    private float speed;
    private float normalSpeed = 800;
    private float fastSpeed = 9999;

    private bool needToMove;

    private void Awake()
    {
        image = GetComponent<RawImage>();

        pos1 = GameObject.Find("Pos1").transform;
        pos2 = GameObject.Find("Pos2").transform;
        pos3 = GameObject.Find("Pos3").transform;
        pos4 = GameObject.Find("Pos4").transform;
        pos5 = GameObject.Find("Pos5").transform;

        needToMove = false;

        SetDestinations();
    }

    private void Update()
    {
        if (!needToMove)
            return;

        transform.position = Vector3.MoveTowards(transform.position, dest.position, speed * Time.deltaTime);

        transform.localScale = Vector3.MoveTowards(transform.localScale, dest.localScale, speed * Time.deltaTime); 

        if (transform.position == dest.position)
            needToMove = false;
    }

    public void GoToNext()
    {
        SetDestinations();
        dest = next;
        needToMove = true;
        pos = (pos + 1 > 5) ? 1 : pos + 1;
        if (pos == 4)
            speed = fastSpeed;
        else
            speed = normalSpeed;
    }
    
    public void GoToPrev()
    {
        SetDestinations();
        dest = prev;
        needToMove = true;
        pos = (pos - 1 < 1) ? 5 : pos - 1;
        if (pos == 3)
            speed = fastSpeed;
        else
            speed = normalSpeed;
    } 

    private void SetDestinations()
    {
        switch (pos)
        {
            case 1:
                {
                    next = pos2;
                    prev = pos5;
                    break;
                }
            case 2:
                {
                    next = pos3;
                    prev = pos1;
                    break;
                }
            case 3:
                {
                    next = pos4;
                    prev = pos2;
                    break;
                }
            case 4:
                {
                    next = pos5;
                    prev = pos3;
                    break;
                }
            case 5:
                {
                    next = pos1;
                    prev = pos4;
                    break;
                }
            default:
                break;
        }
    }

    public void SetImage(Texture tex)
    {
        image.texture = tex;       
    }

    public int GetPos()
    {
        return pos;
    }
}
