using System;
using Train;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class LineEnd : MonoBehaviour
    {
        public virtual void OnLineEnd(TrainMovementController line)
        {
            
        }
        
        //This virtual method get type of object
        public virtual Type GetTypeOfObject()
        {
            return typeof(LineEnd);
        }
    }
}