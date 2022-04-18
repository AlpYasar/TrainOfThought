using System;
using DefaultNamespace;
using NaughtyAttributes;
using Train;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace PathEnds
{
    public class Station : PathEnd
    {
        [SerializeField] private TrainColors stationColor;
        [SerializeField, BoxGroup("Atom Events")] private BoolEvent trainArrivedAtStation;
        public override Type GetTypeOfObject()
        {
            return typeof(Station);
        }

        public void OnTrainArrived(TrainMovementController train)
        {
            //If the train's color matches the station's color this bool event raises true otherwise false
            trainArrivedAtStation.Raise(train.Train.TrainColors == stationColor);

            //then train's gameobject will SetActive(false) so it can be reused by pool
            train.gameObject.SetActive(false);
            
        }
    }
}