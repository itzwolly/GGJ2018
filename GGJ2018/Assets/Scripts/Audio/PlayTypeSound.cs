using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayTypeSound : MonoBehaviour {

    public AudioClip keypress;
    AudioSource audioSource;

    void Start()
    {    
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.anyKeyDown)
        {
            if (Input.GetMouseButtonDown(0)
            || Input.GetMouseButtonDown(1)
            || Input.GetMouseButtonDown(2))
                return; //Do Nothing
            audioSource.PlayOneShot(keypress);
        }
    }
}

