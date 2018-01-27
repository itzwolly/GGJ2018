using System.IO;

namespace DLLLibrary
{
    public class StringMessageHelper:MessageHelper
    {
        private static int id = 2;

        public override int ID
        {
            get { return id; }
        }
        public override void Serialize(Message message,BinaryWriter writer)
        {
            writer.Write((message as StringMessage).Message);
        }

        public override Message Deserialize(BinaryReader reader)
        {
            string str = reader.ReadString();
            return new StringMessage(str);
        }
    }
}
