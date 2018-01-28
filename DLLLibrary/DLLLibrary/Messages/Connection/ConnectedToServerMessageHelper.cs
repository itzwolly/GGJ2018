using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using System.Text;

namespace DLLLibrary
{
    public class ConnectToServerMessageHelper : MessageHelper
    {
        const int id = 5;
        public override int ID
        {
            get { return id; }
        }
        public override void Serialize(Message message, BinaryWriter writer)//write debug info to debug
        {
            ConnectToServerMessage req = message as ConnectToServerMessage;
            writer.Write(req.Name);
            writer.Write(req.NewGame);
        }

        public override Message Deserialize(BinaryReader reader)
        {
            string name = reader.ReadString();
            bool newgame = reader.ReadBoolean();
            return new ConnectToServerMessage(name,newgame);
        }
    }
}

