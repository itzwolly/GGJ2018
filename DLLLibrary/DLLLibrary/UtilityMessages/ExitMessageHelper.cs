using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System;

namespace DLLLibrary
{
    public class ExitMessageHelper:MessageHelper
    {
        private static int id = 1;

        public override int ID
        {
            get { return id; }
        }

        public override void Serialize(Message message, BinaryWriter writer)
        {
        }

        public override Message Deserialize(BinaryReader reader)
        {
            return new ExitMessage();
        }
    }
}
