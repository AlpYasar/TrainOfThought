using DefaultNamespace;
using UnityAtoms;
using UnityEngine;

namespace Train
{
    public class Train : MonoBehaviour
    {
        [SerializeField] private TrainColors trainColors;
        [SerializeField, ReadOnly] private TrainMovementController trainMovement;
        public TrainColors TrainColors => trainColors;
    }
}