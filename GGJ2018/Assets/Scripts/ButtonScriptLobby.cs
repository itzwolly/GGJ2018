using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScriptLobby : MonoBehaviour {

    

    public void AbortLobby()
    {
        if (Input.GetKeyDown("escape"))
        {
            
                SceneManager.LoadScene(0);
            
        }

    }


    // Update is called once per frame
    void Update () {
   
        AbortLobby();
    }
}
