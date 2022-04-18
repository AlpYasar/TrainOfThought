using DG.Tweening;
using UnityEngine;
using NaughtyAttributes;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        [SerializeField] private ForkController forkController;
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        float distanceTravelled;
        
        private Vector3 startPosition;
        private Vector3 endPosition;

        void Start() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
                
                
                startPosition = pathCreator.path.GetFirstPoint();
                endPosition = pathCreator.path.GetLastPoint();
            }
        }

        void Update()
        {
            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                
                if (distanceTravelled >= pathCreator.path.length - 0.25f)
                {
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