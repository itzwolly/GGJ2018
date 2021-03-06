﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChunkScript : MonoBehaviour
{
    [SerializeField] int _size=1;
    [SerializeField] Color _normalCol;
    [SerializeField] Color _stealCol;
    bool _type;

    public bool PassedOver;

    float _percentage;
    ChunkHandler _chunkHandler;

    public void SetSize(int v)
    {
        _size = v;
    }
    public int GetSize()
    {
        return _size;
    }

    public void GrowSize()
    {
        if(_size<3)
            _size ++;
    }

    public void SetProcentage(float pro)
    {
        _percentage = pro;
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
        _size = 1;
        _chunkHandler = transform.parent.gameObject.GetComponent<ChunkHandler>();
	}

    private void OnDestroy()
    {
        //_chunkHandler.SendChunkDestroy(_percentage, _type, _size);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
