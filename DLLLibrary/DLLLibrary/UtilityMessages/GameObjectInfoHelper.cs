using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLLibrary
{
    class GameObjectInfoHelper
    {
        public static void Serialize(GameObjectInfo pMessage, BinaryWriter pWriter)
        {
            //Serialize the AddRequest into the stream
            Vector3Helper.Serialize(pMessage.Pos, pWriter);
            QuatornianHelper.Serialize(pMessage.Rot, pWriter);
            Vector3Helper.Serialize(pMessage.Scale, pWriter);
        }

        public static GameObjectInfo Deserialize(BinaryReader pReader)
        {
            //return deserialized AddRequest from the stream
            Vector3 pos = Vector3Helper.Deserialize(pReader);
            Quaternion rot = QuatornianHelper.Deserialize(pReader);
            Vector3 scale = Vector3Helper.Deserialize(pReader);
            return new GameObjectInfo(pos,rot,scale);
        }
    }
}
