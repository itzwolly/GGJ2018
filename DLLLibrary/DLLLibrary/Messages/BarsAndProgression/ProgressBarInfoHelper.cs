using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using System.Text;

namespace DLLLibrary
{
    public class ProgressBarInfoHelper : MessageHelper
    {
        const int id = 19;
        public override int ID
        {
            get { return id; }
        }
        public override void Serialize(Message message, BinaryWriter writer)//write debug info to debug
        {
            ProgressBarInfo req = message as ProgressBarInfo;
            writer.Write(req.Size);
            for(int i=0;i<req.Size;i++)
            {
                writer.Write(req.Bars[i]);
            }
            writer.Write(req.NextPlayer);
        }

        public override Message Deserialize(BinaryReader reader)
        {
            int size = reader.ReadInt32();
            float[] bars = new float[size];
            for (int i = 0; i < size; i++)
            {
                bars[i] = (float)reader.ReadDouble();
            }
            string next = reader.ReadString();
            return new ProgressBarInfo(bars,size,next);
        }
    }
}

