using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepPlayer : MonoBehaviour {

    AudioManager audioManager;

    void Start () {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void PlayFootstep()
    {
        audioManager.Play("FootStep");
    }
}
