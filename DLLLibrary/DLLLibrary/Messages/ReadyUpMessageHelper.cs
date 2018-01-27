using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using System.Text;

namespace DLLLibrary
{
    public class ReadyUpMessageHelper : MessageHelper
    {
        const int id = 3;
        public override int ID
        {
            get { return id; }
        }
        public override void Serialize(Message message, BinaryWriter writer)//write debug info to debug
        {
            ReadyUpMessage req = message as ReadyUpMessage;
            writer.Write(req.Name);
        }

        public override Message Deserialize(BinaryReader reader)
        {
            string name = reader.ReadString();
            return new ReadyUpMessage(name);
        }
    }
}

