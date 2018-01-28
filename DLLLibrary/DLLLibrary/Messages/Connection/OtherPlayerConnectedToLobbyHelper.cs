using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using System.Text;

namespace DLLLibrary
{
    public class OtherPlayerConnectedToLobbyHelper : MessageHelper
    {
        const int id = 7;
        public override int ID
        {
            get { return id; }
        }
        public override void Serialize(Message message, BinaryWriter writer)//write debug info to debug
        {
            OtherPlayerConnectedToLobby req = message as OtherPlayerConnectedToLobby;
            writer.Write(req.Name);
            writer.Write(req.Ready);
        }

        public override Message Deserialize(BinaryReader reader)
        {
            string name = reader.ReadString();
            bool ready = reader.ReadBoolean();
            return new OtherPlayerConnectedToLobby(name, ready);
        }
    }
}

