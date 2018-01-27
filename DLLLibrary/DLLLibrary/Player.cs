using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DLLLibrary
{
    public class Player
    {
        public GameObject Object;
        public string Name;
        public int Serial;

        public Player(GameObject obj,string name,int serial)
        {
            Object = obj;
            Name = name;
            Serial = serial;
        }
    }
}
