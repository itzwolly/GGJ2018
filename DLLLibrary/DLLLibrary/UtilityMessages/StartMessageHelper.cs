using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

namespace DLLLibrary
{
    public class StartMessageHelper:MessageHelper
    {
        const int id = 4;
        public override int ID
        {
            get { return id; }
        }
        public override void Serialize(Message message, BinaryWriter writer)//write debug info to debug
        {
            StartGameMessage msg = message as StartGameMessage;
            int count = (msg).Responses.Count;
            writer.Write(count);
            for(int i=0;i<count;i++)
            {
                Debug.Log("Writing Response");
                writer.Write(msg.Responses[i].Team);
                Vector3Helper.Serialize(msg.Responses[i].Position, writer);
            }
            count = msg.Planes.Count;
            writer.Write(count);
            for (int i = 0; i < count; i++)
            {
                Debug.Log("Writing Plane");
                GameObjectInfoHelper.Serialize(msg.Planes[i], writer);
            }
            count = msg.Cubes.Count;
            writer.Write(count);
            for (int i = 0; i < count; i++)
            {
                Debug.Log("Writing Cube");
                GameObjectInfoHelper.Serialize(msg.Cubes[i], writer);
            }
            Debug.Log("done writing");
        }

        public override Message Deserialize(BinaryReader reader)
        {
            Debug.Log("Reading Count...");
            int count = reader.ReadInt32();
            List<ConnectResponse> responses = new List<ConnectResponse>();
            for(int i=0;i<count;i++)
            {
                Debug.Log("Reading Response");
                int team = reader.ReadInt32();
                Vector3 pos = Vector3Helper.Deserialize(reader);
                
                responses.Add(new ConnectResponse(team,pos));
            }

            List<GameObjectInfo> planes = new List<GameObjectInfo>();
            count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                Debug.Log("Reading Planes");
                GameObjectInfo plane = GameObjectInfoHelper.Deserialize(reader);

                planes.Add(plane);
            }
            List<GameObjectInfo> cubes = new List<GameObjectInfo>();
            count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                Debug.Log("Reading Cubes");
                GameObjectInfo cube = GameObjectInfoHelper.Deserialize(reader);

                cubes.Add(cube);
            }

            return new StartGameMessage(responses,cubes,planes);
        }
    }
}
