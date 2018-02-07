using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class SoundsPopobar : MonoBehaviour {

    public GameObject CopBar;

    public AudioSource _select_audioSource;
    public AudioClip _Popo_1;
    public AudioClip _Popo_2;
    public AudioClip _Popo_3;
    public AudioClip _Popo_4;
    public AudioClip _Popo_5;
    public AudioClip _Popo_6;
    public AudioClip _Popo_7;
    public AudioClip _Popo_8;
    public AudioClip _Popo_9;
    public AudioClip _Popo_10;
    bool played1 = false;
    bool played2 = false;
    bool played3 = false;
    bool played4 = false;
    bool played5 = false;
    bool played6 = false;
    bool played7 = false;
    bool played8 = false;
    bool played9 = false;
    // Use this for initialization
    void Start () {
		
	}
    void Update()
    {
        if (!_select_audioSource.isPlaying)
        {
            if (CopBar.GetComponent<Image>().fillAmount - 0.9 >= 0.05 && !played9)
            {
                //Reduce fill amount over 30 seconds
                _select_audioSource.PlayOneShot(_Popo_10);
                played9 = true;
            }
            else if (CopBar.GetComponent<Image>().fillAmount - 0.8>= 0.05 && !played8)
            {
                //Reduce fill amount over 30 seconds
                _select_audioSource.PlayOneShot(_Popo_9);
                played8 = true;
            }
            else if (CopBar.GetComponent<Image>().fillAmount - 0.7 >= 0.05 && !played7)
            {
                //Reduce fill amount over 30 seconds
                _select_audioSource.PlayOneShot(_Popo_8);
                played7 = true;
            }
            else if (CopBar.GetComponent<Image>().fillAmount - 0.6 >= 0.05 && !played6)
            {
                //Reduce fill amount over 30 seconds
                _select_audioSource.PlayOneShot(_Popo_7);
                played6 = true;
            }
            else if (CopBar.GetComponent<Image>().fillAmount - 0.5 >= 0.05 && !played5)
            {
                //Reduce fill amount over 30 seconds
                _select_audioSource.PlayOneShot(_Popo_5);
                played5 = true;
            }
            else if (CopBar.GetComponent<Image>().fillAmount - 0.4 >= 0.05 && !played4)
            {
                //Reduce fill amount over 30 seconds
                _select_audioSource.PlayOneShot(_Popo_4);
                played4 = true;
            }
          
            else if (CopBar.GetComponent<Image>().fillAmount - 0.3 >= 0.05 && !played3)
            {
                //Reduce fill amount over 30 seconds
                _select_audioSource.PlayOneShot(_Popo_3);
                played3 = true;
            }
            else if (CopBar.GetComponent<Image>().fillAmount - 0.2 >= 0.05 && !played2)
            {
                //Reduce fill amount over 30 seconds
                _select_audioSource.PlayOneShot(_Popo_2);
                played2 = true;
            }
            else if (CopBar.GetComponent<Image>().fillAmount - 0.1 >= 0.05 && !played1)
            {
                //Reduce fill amount over 30 seconds
                _select_audioSource.PlayOneShot(_Popo_1);
                played1 = true;
            }

            //if (CopBar.GetComponent<Image>().fillAmount == 0.3)
            //{
            //    //Reduce fill amount over 30 seconds
            //    _select_audioSource.PlayOneShot(_Popo_3);
            //}
            //if (CopBar.GetComponent<Image>().fillAmount == 0.4)
            //{
            //    //Reduce fill amount over 30 seconds
            //    _select_audioSource.PlayOneShot(_Popo_4);
            //}
            //if (CopBar.GetComponent<Image>().fillAmount == 0.5)
            //{
            //    //Reduce fill amount over 30 seconds
            //    _select_audioSource.PlayOneShot(_Popo_5);
            //}
            //if (CopBar.GetComponent<Image>().fillAmount == 0.6)
            //{
            //    //Reduce fill amount over 30 seconds
            //    _select_audioSource.PlayOneShot(_Popo_6);
            //}
            //if (CopBar.GetComponent<Image>().fillAmount == 0.7)
            //{
            //    //Reduce fill amount over 30 seconds
            //    _select_audioSource.PlayOneShot(_Popo_7);
            //}
            //if (CopBar.GetComponent<Image>().fillAmount == 0.8)
            //{
            //    //Reduce fill amount over 30 seconds
            //    _select_audioSource.PlayOneShot(_Popo_8);
            //}
            //if (CopBar.GetComponent<Image>().fillAmount == 0.9)
            //{
            //    //Reduce fill amount over 30 seconds
            //    _select_audioSource.PlayOneShot(_Popo_9);
            //}
            //if (CopBar.GetComponent<Image>().fillAmount == 0.10)
            //{
            //    //Reduce fill amount over 30 seconds
            //    _select_audioSource.PlayOneShot(_Popo_10);
            //}
        }
    }

}