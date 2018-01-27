using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLLibrary
{
    public class ConnectedToLobby : Message
    {
        public List<string> Names;
        public List<bool> Readys;
        public ConnectedToLobby(List<string> pNames, List<bool> pReadys)
        {
            Readys = pReadys;
            Names = pNames;
            MessageHelp = new ConnectedToLobbyHelper();
        }
    }
}

