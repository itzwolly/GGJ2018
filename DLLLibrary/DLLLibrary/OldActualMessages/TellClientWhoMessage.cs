using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLLibrary
{
    class TellClientWhoMessage : Message
    {
        public int PosOfIt;
        public TellClientWhoMessage(int pos)
        {
            MessageHelp = new TellClientWhoMessageHelper();
            PosOfIt = pos;
        }
    }
}
