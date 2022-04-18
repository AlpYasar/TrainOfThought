using UnityEngine;

namespace Manager
{
    public class LifeCycleManager : MonoBehaviour
    {

        // Unity Life Cycle
        public delegate void UpdateCheck();
        public static event UpdateCheck update; // This event is for update. For optimization just use one update which is just in GameManager.If there is a need to use update add function to updateCheck 

        public delegate void FixedUpdateCheck();
        public static event FixedUpdateCheck fixedUpdate; // This event is for update. For optimization just use one update which is just in GameManager.If there is a need to use update add function to updateCheck 

        
        private void Update()
        {
            if(update != null) update.Invoke();  // just update when game started.In fact it just call mouse click but there will be different needs in future
        }

        private void FixedUpdate()
        {
            if(fixedUpdate != null)  fixedUpdate.Invoke();
        }

        
    }
}