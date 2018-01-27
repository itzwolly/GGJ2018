using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLLibrary
{
    public class StartGameMessage:Message
    {
        public List<ConnectResponse> Responses;
        public List<GameObjectInfo> Planes;
        public List<GameObjectInfo> Cubes;
        public StartGameMessage(List<ConnectResponse> responses, List<GameObjectInfo> cubes, List<GameObjectInfo> planes)
        {
            MessageHelp = new StartMessageHelper();
            Cubes = cubes;
            Planes = planes;
            Responses = responses;
        }
    }
}
