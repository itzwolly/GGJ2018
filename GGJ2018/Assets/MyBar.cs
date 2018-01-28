using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyBar : MonoBehaviour {

    // Use this for initialization
    Image img;
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        img.fillAmount = ClientController._myProgress;
    }
}
