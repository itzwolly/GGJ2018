using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLLibrary
{
    public class GameObjectInfo
    {
        public Vector3 Pos;
        public Quaternion Rot;
        public Vector3 Scale;

        public GameObjectInfo(Vector3 pos,Quaternion rot,Vector3 scale)
        {
            Pos = pos;
            Rot = rot;
            Scale = scale;
        }

        public static GameObjectInfo TransferIntoInfo(GameObject obj)
        {
            return new GameObjectInfo(obj.transform.position,obj.transform.rotation,obj.transform.lossyScale);
        }
    }
}
