using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLLibrary
{
    class PlaneHelper
    {
        public static void Serialize(Vector3 pMessage, BinaryWriter pWriter)
        {
            //Serialize the AddRequest into the stream
            pWriter.Write((double)pMessage.x);
            pWriter.Write((double)pMessage.y);
            pWriter.Write((double)pMessage.z);
        }

        public static Vector3 Deserialize(BinaryReader pReader)
        {
            //return deserialized AddRequest from the stream
            float x = (float)pReader.ReadDouble();
            float y = (float)pReader.ReadDouble();
            float z = (float)pReader.ReadDouble();
            return new Vector3(x, y, z);
        }
    }
}
