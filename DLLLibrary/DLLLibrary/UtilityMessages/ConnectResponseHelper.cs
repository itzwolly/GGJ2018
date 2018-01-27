using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using System.Text;

namespace DLLLibrary
{
    public class ConnectResponseHelper:MessageHelper
    {
        const int id = 3;
        public override int ID
        {
            get { return id; }
        }


        public override void Serialize(Message message, BinaryWriter writer)
        {
            writer.Write((message as ConnectResponse).Team);
            Vector3Helper.Serialize((message as ConnectResponse).Position, writer);
        }

        public override Message Deserialize(BinaryReader reader)
        {
            int team = reader.ReadInt32();
            Vector3 pos = Vector3Helper.Deserialize(reader);
            return new ConnectResponse(team,pos);
        }
    }
}
