using System.IO;
using UnityEngine;

namespace DLLLibrary
{
    class QuatornianHelper
    {
        public static void Serialize(Quaternion pMessage, BinaryWriter pWriter)
        {
            //Serialize the AddRequest into the stream
            pWriter.Write((double)pMessage.w);
            pWriter.Write((double)pMessage.x);
            pWriter.Write((double)pMessage.y);
            pWriter.Write((double)pMessage.z);
        }

        public static Quaternion Deserialize(BinaryReader pReader)
        {
            //return deserialized AddRequest from the stream
            float w = (float)pReader.ReadDouble();
            float x = (float)pReader.ReadDouble();
            float y = (float)pReader.ReadDouble();
            float z = (float)pReader.ReadDouble();
            return new Quaternion(x,y,z,w);
        }
    }
}
