using System;
using Train;
using UnityEngine;

namespace PathEnds
{
    public abstract class PathEnd : MonoBehaviour
    {
        public virtual void OnLineEnd(TrainMovementController line)
        {
            
        }
        
        //This virtual method get type of object
        public abstract Type GetTypeOfObject();

    }
}