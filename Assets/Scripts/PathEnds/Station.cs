using System;
using UnityEngine;

namespace PathEnds
{
    public class Station : PathEnd
    {
        public override Type GetTypeOfObject()
        {
            return typeof(Station);
        }
    }
}