using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace DLLLibrary
{
    public class InformClientMessageHelper:MessageHelper
    {
        const int id = 7;
        public override int ID
        {
            get { return id; }
        }
        public override void Serialize(Message message, BinaryWriter writer)//write debug info to debug
        {
            InformClientMessage req = message as InformClientMessage;
            writer.Write(req.Positions.Count);
            foreach (Vector3 i in req.Positions)
            {
                Vector3Helper.Serialize(i, writer);
            }
            writer.Write(req.Velocities.Count);
            foreach (Vector3 i in req.Velocities)
            {
                Vector3Helper.Serialize(i, writer);
            }
        }

        public override Message Deserialize(BinaryReader reader)
        {
            List<Vector3> pos = new List<Vector3>();
            List<Vector3> vels = new List<Vector3>();
            int length = reader.ReadInt32();
            for (int i = 0; i < length; i++)
            {
                Vector3 t = Vector3Helper.Deserialize(reader);
                pos.Add(t);
            }
            length = reader.ReadInt32();
            for (int i = 0; i < length; i++)
            {
                Vector3 t = Vector3Helper.Deserialize(reader);
                vels.Add(t);
            }

            return new InformClientMessage(pos, vels);
        }
    }
}
