using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DLLLibrary
{
    class TellClientWhoMessageHelper:MessageHelper
    {
        const int id = 8;
        public override int ID
        {
            get { return id; }
        }

        public override void Serialize(Message message, BinaryWriter writer)
        {
            TellClientWhoMessage msg = message as TellClientWhoMessage;
            writer.Write(msg.PosOfIt);
        }

        public override Message Deserialize(BinaryReader reader)
        {
            int pos = reader.ReadInt32();
            return new TellClientWhoMessage(pos);
        }
    }
}
