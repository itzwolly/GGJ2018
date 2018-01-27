using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DLLLibrary
{
    public abstract class MessageHelper
    {
        public abstract int ID
        {
            get ;
        }

        public abstract void Serialize(Message message, BinaryWriter writer);

        public abstract Message Deserialize(BinaryReader reader);

    }
}
