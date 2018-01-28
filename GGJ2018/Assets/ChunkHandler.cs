using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkHandler : MonoBehaviour {
    [SerializeField] GameObject[] _chunkBrushes;
    [SerializeField] ChunkProcentageHandler _chunkProcentageHandler;
	// Use this for initialization

    public void SendChunkDestroy(float procent, bool type, int size)
    {
        _chunkProcentageHandler.SendBrokenChunk(procent, type, size);
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
