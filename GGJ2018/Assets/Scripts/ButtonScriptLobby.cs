using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScriptLobby : MonoBehaviour {

    

    public void AbortLobby()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                SceneManager.LoadScene(0);
            }
            // _targetToHide2.SetActive(false);
        }

    }


    // Update is called once per frame
    void Update () {
   
        AbortLobby();
    }
}
