using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

    [SerializeField] private GameObject _MainMenu;
    [SerializeField] private GameObject _Credits;
    
    
    [SerializeField] private GameObject _Tutorial;
    
    public AudioClip _select;
    public AudioSource _select_audioSource;

    bool _logged_in = false;

    // [SerializeField] private GameObject _targetToHide2;
    [SerializeField] private GameObject _CreditsMenu;

    // Use this for initialization
    void Start () {
        
    }
    
    //public void Login()
    //{
    //    Debug.Log("Turning it off again");
    //    _Login.SetActive(false);
    //}

    //// сука ыуат rush b
    public void Logout()
    {
        Application.Quit();
    }

    public void LoadTutorial()
    {
            _MainMenu.SetActive(false);
            _Tutorial.SetActive(true);
            _select_audioSource.PlayOneShot(_select);

    }

    public void LoadGame() //CREDITS
    {
        
        
            _MainMenu.SetActive(false);
            _Credits.SetActive(true);
            _Tutorial.SetActive(!true);
            _select_audioSource.PlayOneShot(_select);
       
    }

    public void LoadLobby()
    {
       
            SceneManager.LoadScene(1);
            _select_audioSource.PlayOneShot(_select);
        
        
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        _select_audioSource.PlayOneShot(_select);
    }

    public void CreditsMenu()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (_MainMenu.activeSelf)
            {
               
            }
            else
            {
                //_CreditsMenu.SetActive(!_CreditsMenu.activeSelf);
                _select_audioSource.PlayOneShot(_select);
                _MainMenu.SetActive(true);
                _Credits.SetActive(false);
                _Tutorial.SetActive(false);
            }
           // _targetToHide2.SetActive(false);
        }
    }
    
    void LoggingIn()
    {
        _logged_in = true;
        
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Return) && !_logged_in)
        {
            LoggingIn();
        }

        CreditsMenu();
    }
}
