using UnityEngine;

namespace HoverController
{
    public class HoverObjectController : MonoBehaviour
    {
        //Singleton
        public static HoverObjectController Instance => _instance;
        private static HoverObjectController _instance;
        
        [SerializeField] private GameObject _hoverObject;
        public GameObject HoverObject => _hoverObject;
        
        void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
            
            _hoverObject.SetActive(false);
        }
        
        public void SetActiveAndSetPosition(Vector3 position)
        {
            _hoverObject.SetActive(true);
            _hoverObject.transform.position = position;
        }
        
        public void SetInactive()
        {
            _hoverObject.SetActive(false);
        }
        
    }
}