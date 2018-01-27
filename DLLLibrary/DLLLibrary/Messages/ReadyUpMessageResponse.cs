using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLLibrary
{
    public class ReadyUpMessageResponse : Message
    {
        public string Name;
        public bool Val;
        public ReadyUpMessageResponse(string pName,bool val)
        {
            Name = pName;
            Val = val;
            MessageHelp = new ReadyUpMessageResponseHelper();
        }
    }
}

