using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DLLLibrary;

public class ChunkProcentageHandler : MonoBehaviour {

    // Use this for initialization
    public void SendBrokenChunk(float procent,bool type, int size)
    {
        ClientController.SendChunkInfo(procent, type, size);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
