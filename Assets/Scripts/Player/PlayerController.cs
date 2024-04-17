using Scripts;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private Vector3 _startPosition;
        private Quaternion _startRotation;
        
        private void Start()
        {
            _startPosition = transform.position;
            _startRotation = transform.rotation;

            GlobalEventManager.OnGameStarted.AddListener(OnGameStart);
        }

        private void OnGameStart()
        {
            transform.position = _startPosition;
            transform.rotation = _startRotation;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("EnemyCar"))
            {
                GlobalEventManager.StopGame(GameEndState.PlayerCrashed);
            }
        }
    }
}