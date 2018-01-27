using System.IO;
using UnityEngine;

namespace DLLLibrary
{
    public class MoveMessageResponseHelper:MessageHelper
    {
        const int id = 6;
        public override int ID
        {
            get { return id; }
        }
        public override void Serialize(Message message, BinaryWriter writer)//write debug info to debug
        {
            MoveMessageResponse mes = (message as MoveMessageResponse);

            QuatornianHelper.Serialize(mes.Rotation,writer);
            Vector3Helper.Serialize(mes.Velocity, writer);
        }

        public override Message Deserialize(BinaryReader reader)
        {
            Quaternion q = QuatornianHelper.Deserialize(reader);
            Vector3 vec = Vector3Helper.Deserialize(reader);
            return new MoveMessageResponse(vec,q);
        }
    }
}
