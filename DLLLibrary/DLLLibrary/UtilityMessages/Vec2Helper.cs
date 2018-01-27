using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

class Vec2Helper
{
    public static void Serialize(Vec2 pMessage, BinaryWriter pWriter)
    {
        //Serialize the AddRequest into the stream
        pWriter.Write((double)pMessage.X);
        pWriter.Write((double)pMessage.Y);
    }

    public static Vec2 Deserialize(BinaryReader pReader)
    {
        //return deserialized AddRequest from the stream
        float x = (float)pReader.ReadDouble();
        float y = (float)pReader.ReadDouble();
        return new Vec2(x, y);
    }
}

