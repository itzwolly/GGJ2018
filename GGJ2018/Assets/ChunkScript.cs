using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChunkScript : MonoBehaviour
{
    [SerializeField] int _size;
    [SerializeField] Color _normalCol;
    [SerializeField] Color _stealCol;
    bool _type;

    float _procentage;
    ChunkHandler _chunkHandler;

    public void SetSize(int v)
    {
        _size = v;
    }

    public void SetProcentage(float pro)
    {
        _procentage = pro;
    }
    
	// Use this for initialization
	void Start () {
        _type = (Random.value > 0.5f);
        if(_type)
        {
            gameObject.GetComponent<Image>().color = _normalCol;
        }
        else
        {
            gameObject.GetComponent<Image>().color = _stealCol;
        }
        _chunkHandler = transform.parent.gameObject.GetComponent<ChunkHandler>();
	}

    private void OnDestroy()
    {
        _chunkHandler.SendChunkDestroy(_procentage, _type, _size);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
