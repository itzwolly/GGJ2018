using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace DLLLibrary
{
    public class ConnectResponse:Message
    {
        public int Team;
        public Vector3 Position = Vector3.zero;

        public ConnectResponse(int team, Vector3 pos)
        {
            MessageHelp = new ConnectResponseHelper();
            Position = pos;
            Team = team;
        }
    }
}
