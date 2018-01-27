using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DLLLibrary
{
    public class MoveMessageResponse :Message
    {
        public Vector3 Velocity;
        public Quaternion Rotation;
        public MoveMessageResponse(Vector3 vel, Quaternion rot)
        {
            MessageHelp = new MoveMessageResponseHelper();
            Rotation = rot;
            Velocity = vel;
        }
    }
}
