using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using System.Text;

namespace DLLLibrary
{
    public class ConnectedToLobbyHelper : MessageHelper
    {
        const int id = 6;
        public override int ID
        {
            get { return id; }
        }
        public override void Serialize(Message message, BinaryWriter writer)//write debug info to debug
        {
            ConnectedToLobby req = message as ConnectedToLobby;
            writer.Write(req.Names.Count);
            foreach(string name in req.Names)
            {
                writer.Write(name);
            }
            writer.Write(req.Readys.Count);
            foreach (bool ready in req.Readys)
            {
                writer.Write(ready);
            }
        }

        public override Message Deserialize(BinaryReader reader)
        {
            List<string> names = new List<string>();
            List<bool> readys = new List<bool>();
            int count = reader.ReadInt32();
            for(int i =0;i<count;i++)
            {
                names.Add(reader.ReadString());
            }
            count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                readys.Add(reader.ReadBoolean());
            }
            return new ConnectedToLobby(names, readys);
        }
    }
}

