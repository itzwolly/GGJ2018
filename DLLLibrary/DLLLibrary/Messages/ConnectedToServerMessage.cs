using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLLibrary
{
    public class ConnectToServerMessage : Message
    {
        public string Name;
        public bool NewGame;
        public ConnectToServerMessage(string pName,bool pNewGame)
        {
            Name = pName;
            NewGame = pNewGame;
            MessageHelp = new ConnectToServerMessageHelper();
        }
    }
}

