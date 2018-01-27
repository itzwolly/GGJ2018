using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using System.Text;

namespace DLLLibrary
{
    public class ReadyUpMessageResponseHelper : MessageHelper
    {
        const int id = 4;
        public override int ID
        {
            get { return id; }
        }
        public override void Serialize(Message message, BinaryWriter writer)//write debug info to debug
        {
            ReadyUpMessageResponse req = message as ReadyUpMessageResponse;
            writer.Write(req.Name);
            writer.Write(req.Val);
        }

        public override Message Deserialize(BinaryReader reader)
        {
            string name = reader.ReadString();
            bool val = reader.ReadBoolean();
            return new ReadyUpMessageResponse(name,val);
        }
    }
}

