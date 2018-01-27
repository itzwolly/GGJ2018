using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLLibrary
{
    public class ReadyUpMessage : Message
    {
        public string Name;
        public ReadyUpMessage(string pName)
        {
            Name = pName;
            MessageHelp = new ReadyUpMessageHelper();
        }
    }
}

