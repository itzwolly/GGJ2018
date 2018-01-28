using System;
using System.IO;
using UnityEngine;

namespace DLLLibrary
{
    public class SerializeDeserialize
    {
        private static Message[] MessageTypes = {
            new ExitMessage(),
            new StringMessage(null),
            new ReadyUpMessage(null),
            new ReadyUpMessageResponse(null,false),
            new ConnectToServerMessage(null,false),
            new OtherPlayerConnectedToLobby(null,false),
            new ConnectedToLobby(null,null),
            new StartGameMessage(),
            new ProgressBarInfo(null,0,null),
            new ChunkCompletionInfo(0,false,0)
        };

        public static void Serialize(Message pMessage, BinaryWriter pWriter) /*where T: Message*/
        {
            
            pWriter.Write((pMessage).MessageHelp.ID);
            //Debug.Log((pMessage).MessageHelp.ID);
            (pMessage).MessageHelp.Serialize(pMessage, pWriter);

        }

        public static Message Deserialize(BinaryReader pReader)
        {
            int classId = pReader.ReadInt32();
            //Debug.Log(classId);

            foreach(Message msg in MessageTypes)
            {
                if(classId==msg.MessageHelp.ID)
                {
                    return msg.MessageHelp.Deserialize(pReader);
                }
            }

            
            
            throw new Exception("Cannot deserialize");
            
            
        }
    }
}