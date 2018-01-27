using System;
using System.Collections.Generic;
using UnityEngine;

namespace DLLLibrary
{
    public class InformClientMessage:Message
    {
        public List<Vector3> Positions;
        public List<Vector3> Velocities;
        public InformClientMessage(List<Vector3>pos, List<Vector3>vels)
        {
            Positions = pos;
            Velocities = vels;
            MessageHelp = new InformClientMessageHelper();
        }
    }
}
