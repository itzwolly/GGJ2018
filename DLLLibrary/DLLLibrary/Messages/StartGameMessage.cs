using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLLibrary
{
    public class StartGameMessage : Message
    {
        public StartGameMessage()
        {
            MessageHelp = new StartGameMessageHelper();
        }
    }
}

