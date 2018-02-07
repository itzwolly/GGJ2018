using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HandleBars : MonoBehaviour {
    [SerializeField] Image _mybar;
    [SerializeField] Image _copbar;
    [SerializeField] int _dificytly;
    // Use this for initialization
    void Start () {
		
	}

    public void ChunkEnded(float _percentage, bool _type, int _size,int missed)
    {
        Debug.Log(_percentage+" {} "+_type+" {} "+_size);
        if(_type)
            _mybar.fillAmount += _percentage * _size / _dificytly;
        else
        {
            float val = _percentage*_size*(_copbar.fillAmount - _mybar.fillAmount);
            _mybar.fillAmount +=val/2;
            _copbar.fillAmount -=val/3;
            _copbar.fillAmount += (float)missed / 150 * _dificytly;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(_copbar.fillAmount==1)
        {
            SceneManager.LoadScene("Loss");
        }
        if(_mybar.fillAmount==1)
        {
            SceneManager.LoadScene("Win");
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Disconnected");
        }
	}
}
