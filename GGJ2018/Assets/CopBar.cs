using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopBar : MonoBehaviour {
    [SerializeField] float _copIncrease;
    // Use this for initialization
    Image img;
    void Start () {
        img = GetComponent<Image>();
	}

    public void Decrease(float val)
    {
        img.fillAmount-=val;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        img.fillAmount += _copIncrease;
	}
}
