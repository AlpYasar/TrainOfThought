using DG.Tweening;
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
        [SerializeField] private PathEnd pathEnd;
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        
        private float distanceTravelled;
        [SerializeField, ReadOnly] private bool isEndTypeFork;
        private ForkController forkController;
        

        private Vector3 endPosition;

        void Start() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
                
                
                pathCreator.path.GetFirstPoint();
                endPosition = pathCreator.path.GetLastPoint();
            }
            
            //if Type of end is fork
            if (typeof(ForkController) == pathEnd.GetTypeOfObject())
            {
                forkController = GetComponentInParent<ForkController>();
                isEndTypeFork = true;
            }
            
            
        }

        
        private void FallowingToLine()
        {
            if (pathCreator == null) return;
            
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);

            if (!(distanceTravelled >= pathCreator.path.length - 0.1f)) return;
            
            distanceTravelled = 0;
            pathCreator = null;
            Debug.Log("Reached end of path");
            
            if (forkController.AvailablePathCreator)
            {
                transform.DOMove(forkController.AvailablePathCreator.path.GetFirstPoint(), speed).SetSpeedBased(true).
                    OnComplete(()=> pathCreator = forkController.AvailablePathCreator);
            }
            else
            {
                transform.DOMove(forkController.AvailablePathCreator.path.GetFirstPoint(), speed).SetSpeedBased(true).
                    OnComplete(()=> pathCreator = forkController.AvailablePathCreator);
            }
        }
        

        [Button]
        private void CubeMaker()
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = transform.position;
            cube.transform.rotation = transform.rotation;
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
