using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLLibrary
{
    public class ChunkCompletionInfo : Message
    {
        public float Procent;
        public bool Type;
        public int Size;
        public ChunkCompletionInfo(float procent, bool type, int size)
        {
            Procent = procent;
            Type = type;
            Size = size;
            MessageHelp = new ChunkCompletionInfoHelper();
        }
    }
}

