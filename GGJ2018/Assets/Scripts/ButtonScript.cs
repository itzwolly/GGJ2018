using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

    [SerializeField] private GameObject _MainMenu;
    [SerializeField] private GameObject _Credits;
    [SerializeField] private GameObject _Lobby;
    [SerializeField] private GameObject _Login;

    public AudioClip _select;
    public AudioSource _select_audioSource;

    bool _logged_in = false;

    // [SerializeField] private GameObject _targetToHide2;
    [SerializeField] private GameObject _CreditsMenu;

    

    // Use this for initialization
    void Start () {
        _Login.SetActive(true);
    }
    
    public void Login()
    {
        _Login.SetActive(false);
    }

    public void LoadGame() //CREDITS
    {
        if (Input.GetKeyDown(KeyCode.Return) && !_logged_in)
        {
            _logged_in = true;
            Login();
        }
        else
        {
            _MainMenu.SetActive(false);
            _Credits.SetActive(true);
            _select_audioSource.PlayOneShot(_select);
        }
    }

    public void LoadLobby()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !_logged_in) 
        {
            _logged_in = true;
            Login();
        }
        else
        {
            SceneManager.LoadScene(1);
            _select_audioSource.PlayOneShot(_select);
        }
        
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
                return;
            }
            else
            {
                //_CreditsMenu.SetActive(!_CreditsMenu.activeSelf);
                _select_audioSource.PlayOneShot(_select);
                _MainMenu.SetActive(true);
                _Credits.SetActive(false);
            }
           // _targetToHide2.SetActive(false);
           
        }
            
    }

    public void AbortLobby()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (_Lobby.activeSelf)
            {
                SceneManager.LoadScene(0);
            }
            // _targetToHide2.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Return) && !_logged_in)
        {
            _logged_in = true;
            Login();
        }
        else
        {
            //return;
        }
        CreditsMenu();
        AbortLobby();
    }
}
