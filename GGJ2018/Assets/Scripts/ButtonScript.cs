using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

    [SerializeField] private GameObject _MainMenu;
    [SerializeField] private GameObject _Credits;


   // [SerializeField] private GameObject _targetToHide2;
    [SerializeField] private GameObject _CreditsMenu;

    // Use this for initialization
    void Start () {
		
	}
    
    public void LoadGame()
    {
        _MainMenu.SetActive(false);
        _Credits.SetActive(true);
    }

    public void CreditsMenu()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (_MainMenu.activeSelf)
            {
                return;
            }
            else
            {
                _CreditsMenu.SetActive(!_CreditsMenu.activeSelf);
            }
           // _targetToHide2.SetActive(false);
           
        }
            
    }

    // Update is called once per frame
    void Update () {
        CreditsMenu();

    }
}
