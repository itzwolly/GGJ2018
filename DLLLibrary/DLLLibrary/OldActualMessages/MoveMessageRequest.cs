using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;

namespace DLLLibrary
{
    public class MoveMessageRequest :Message
    {
        public List<Button> Buttons;
        public Quaternion Quat;
        public enum Button
        {
            none = 0,
            jump = 1,
            forward = 2,
            back = 3,
            left = 4,
            right = 5,
            crouch = 6,
            sprint = 7,
            shoot = 8,
            reload = 9,
            aim = 10,
            swap = 11,
            power = 12
        }

        public MoveMessageRequest(List<Button> buttons, Quaternion quat)
        {
            Quat = quat;
            Buttons = buttons;
            MessageHelp = new MoveMessageRequestHelper();
        }
    }
}
