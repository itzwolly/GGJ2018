using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace DLLLibrary
{
    public class MoveMessageRequestHelper :MessageHelper
    {
        const int id = 5;
        public override int ID
        {
            get { return id; }
        }
        public override void Serialize(Message message, BinaryWriter writer)//write debug info to debug
        {
            MoveMessageRequest req = message as MoveMessageRequest;
            writer.Write(req.Buttons.Count);
            foreach(MoveMessageRequest.Button i in req.Buttons)
            {
                writer.Write((int)i);
                //Debug.Log(i);
            }
            QuatornianHelper.Serialize(req.Quat,writer);
        }

        public override Message Deserialize(BinaryReader reader)
        {
            List<MoveMessageRequest.Button> buttons = new List<MoveMessageRequest.Button>();
            int length = reader.ReadInt32();
            for(int i =0;i<length;i++)
            {
                int t = reader.ReadInt32();
                //Debug.Log((MoveMessageRequest.Button)t);
                buttons.Add((MoveMessageRequest.Button)t);
            }
            Quaternion quat = QuatornianHelper.Deserialize(reader);
            return new MoveMessageRequest(buttons,quat);
        }
    }
}
