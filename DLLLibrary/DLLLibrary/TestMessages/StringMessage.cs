﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DLLLibrary
{
    public class StringMessage:Message
    {
        public string Message;
        public StringMessage (string str)
        {
            MessageHelp = new StringMessageHelper();
            Message = str;
        }
    }
}
