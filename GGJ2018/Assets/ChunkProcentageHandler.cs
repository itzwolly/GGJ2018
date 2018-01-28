using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DLLLibrary;

public class ChunkProcentageHandler : MonoBehaviour {

    // Use this for initialization
    public void SendBrokenChunk(float procent,bool type, int size)
    {
        SerializeDeserialize.Serialize(new ChunkCompletionInfo(procent,type,size), ClientController.GetWriter());
        Debug.Log("procent = " + procent);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
