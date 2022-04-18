using System;
using DG.Tweening;
using Manager;
using UnityEngine;
using NaughtyAttributes;
using PathCreation;
using PathEnds;

namespace Train
{

    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class TrainMovementController : MonoBehaviour
    {
        [SerializeField] private Train train;
        [SerializeField] private PathEnd pathEnd;
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        
        //Properties
        public Train Train => train;
        
        private float distanceTravelled;
        [SerializeField, ReadOnly] private bool isEndTypeFork;
        private ForkController forkController;
        private Station station;

        #region UnityMethods
        void OnEnable() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
            
            //FallowingToLine method added to LifeCycle update
            LifeCycleManager.update += FallowingToLine;
        }

        void OnDisable()
        {
            LifeCycleManager.update -= FallowingToLine;
        }
            

        #endregion
        
        public void SpawnConstructor(PathEnd firstFork, PathCreator firstPath)
        {
            pathCreator = firstPath;
            pathEnd = firstFork;
            CheckEndType();
        }
        private void CheckEndType()
        {
            if (typeof(ForkController) == pathEnd.GetTypeOfObject())
            {
                forkController = pathEnd.GetComponentInParent<ForkController>();
                isEndTypeFork = true;
            }
            else
            {
                station = pathEnd.GetComponentInParent<Station>();
                isEndTypeFork = false;
            }
        }
        
        private void FallowingToLine()
        {
            if (pathCreator == null) return;
            
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);

            if (!(distanceTravelled >= pathCreator.path.length - 0.1f)) return;
            
            WhenEndOfThePath();
        }

        private void WhenEndOfThePath()
        {
            distanceTravelled = 0;
            pathCreator = null;
            Debug.Log("Reached end of path");
            
            if (isEndTypeFork)
            {
                var point = forkController.AvailablePathCreator.path.GetFirstPoint();
                var targetRotation = forkController.AvailablePathCreator.path.GetRotationAtDistance(0, EndOfPathInstruction.Stop);
                
                //tween time
                var tweenTime = Vector3.Distance(transform.position, point) / speed;
                transform.DOMove(point, tweenTime).
                    OnComplete(()=>
                    {
                        Debug.Log("On Complete");
                        pathCreator = forkController.AvailablePathCreator;
                        pathEnd = forkController.PathEnd;
                        Debug.Log("Before check end type");
                        CheckEndType();
                    });
                transform.DORotateQuaternion(targetRotation, tweenTime);
            }
            else
            {
                transform.DOMove(station.transform.position, speed).SetSpeedBased(true).
                    OnComplete(()=> station.OnTrainArrived(this));
            }
        }
        
        
        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
        
        //Show the last point and the first point of the path
        private void OnDrawGizmos() {
            if (pathCreator != null) {
                Gizmos.color = Color.black;
                Gizmos.DrawSphere(pathCreator.path.GetLastPoint(), 0.1f);
                Gizmos.color = Color.white;
                Gizmos.DrawSphere(pathCreator.path.GetFirstPoint(), 0.1f);
            }
        }
    }
}
