using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using System.Text;

namespace DLLLibrary
{
    public class StartGameMessageHelper : MessageHelper
    {
        const int id = 8;
        public override int ID
        {
            get { return id; }
        }
        public override void Serialize(Message message, BinaryWriter writer)//write debug info to debug
        {
        }

        public override Message Deserialize(BinaryReader reader)
        {
            return new StartGameMessage();
        }
    }
}

