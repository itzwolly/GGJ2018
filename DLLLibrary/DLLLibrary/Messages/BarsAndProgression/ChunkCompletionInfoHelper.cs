using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using System.Text;

namespace DLLLibrary
{
    public class ChunkCompletionInfoHelper : MessageHelper
    {
        const int id = 10;
        public override int ID
        {
            get { return id; }
        }
        public override void Serialize(Message message, BinaryWriter writer)//write debug info to debug
        {
            ChunkCompletionInfo req = message as ChunkCompletionInfo;
            writer.Write(req.Type);
            writer.Write(req.Procent);
            writer.Write(req.Size);
        }

        public override Message Deserialize(BinaryReader reader)
        {
            bool type = reader.ReadBoolean();
            float procent = (float)reader.ReadDouble();
            int size = reader.ReadInt32();
            return new ChunkCompletionInfo(procent,type,size);
        }
    }
}

