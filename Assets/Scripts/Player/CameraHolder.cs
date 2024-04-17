using UnityEngine;

namespace Player
{
    public class CameraHolder : MonoBehaviour
    {
        [SerializeField] private Transform carPosition;
        
        private Vector3 _offset;
        
        private void Update()
        {
            transform.position = carPosition.transform.position + _offset;
        }
    }
}