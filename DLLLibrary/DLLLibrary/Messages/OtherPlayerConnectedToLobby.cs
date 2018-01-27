using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLLibrary
{
    public class OtherPlayerConnectedToLobby : Message
    {
        public string Name;
        public bool Ready;
        public OtherPlayerConnectedToLobby(string pName,bool pReady)
        {
            Name = pName;
            Ready = pReady;
            MessageHelp = new OtherPlayerConnectedToLobbyHelper();
        }
    }
}

