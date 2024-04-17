using UnityEngine;

namespace Road
{
    public class RoadPartController : MonoBehaviour
    {
        private Vector3 _startPosition;

        private void Start()
        {
            _startPosition = transform.position;
        }

        public void Move(Vector3 endPosition, float distance)
        {
            var oldPosition = transform.position;

            if (endPosition.z > transform.position.z)
            {
                transform.position = _startPosition;
                return;
            }
            
            transform.position = new Vector3(oldPosition.x, oldPosition.y, oldPosition.z + distance);
        }
    }
}