using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyBar : MonoBehaviour {

    // Use this for initialization
    float _ammount;
    static Image img;
    void Start()
    {
        img = GetComponent<Image>();
    }

    public static void IncreaseFill(float val)
    {
        img.fillAmount += val;
    }
}
