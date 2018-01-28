using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLLibrary
{
    public class ProgressBarInfo : Message
    {
        public float[] Bars;
        public int Size;
        public ProgressBarInfo(float[] bars,int size)
        {
            Bars = bars;
            Size = size;
            MessageHelp = new ProgressBarInfoHelper();
        }
    }
}

