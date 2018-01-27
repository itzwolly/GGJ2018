using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSounds : MonoBehaviour {

    public AudioClip _select;
    public AudioClip _hit;
    public AudioClip _miss;
    public AudioClip _lose_points;
    public AudioClip _win;
    public AudioClip _lose;
    public AudioClip _minigame_done;
    public AudioSource _select_audioSource;
    public AudioSource _points_audioSource;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Selected()
    {
        _select_audioSource.PlayOneShot(_select);
    }

    public void Hit()
    {
        _select_audioSource.PlayOneShot(_hit);
    }

    public void Miss()
    {
        _select_audioSource.PlayOneShot(_miss);
    }

    public void MinigameDone()
    {
        _points_audioSource.PlayOneShot(_minigame_done);
    }

    public void LosePoints()
    {
        _points_audioSource.PlayOneShot(_lose_points);
    }

    public void Win()
    {
        _points_audioSource.PlayOneShot(_win);
    }

    public void Lose()
    {
        _points_audioSource.PlayOneShot(_lose);
    }
}
