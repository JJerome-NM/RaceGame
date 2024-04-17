using System.Collections;
using Road;
using Scripts;
using UnityEngine;

namespace Enemies
{
    public class EnemyCarController : MonoBehaviour
    {
        [SerializeField] private Transform startBound;
        
        private float _speed = 30;
        private float _currentSpeed;
        private Coroutine _coroutine;
        private bool _isOnRoad = false; 
        
        private void Start()
        {
            GlobalEventManager.OnGameStarted.AddListener(OnGameStarted);
            GlobalEventManager.OnGameStopped.AddListener(OnGameStopped);
            RoadEventManager.OnPlayerCarSpeedChange.AddListener(speed => _currentSpeed = _speed - speed);
        }

        private void OnGameStopped(GameEndState state)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }
        
        private void OnGameStarted()
        {
            if (_isOnRoad)
            {
                _coroutine ??= StartCoroutine(MoveMe());
            }
        }
        
        private IEnumerator MoveMe()
        {
            while (_isOnRoad)
            {
                if (transform.position.z < startBound.position.z)
                {
                    EnemyEventManager.EnemyOutOfBounds(this);
                }
                
                transform.position += Vector3.forward * (Time.deltaTime * _currentSpeed);
                yield return null;
            }
        }

        public void SpawnOnTheRoad(Transform newTransform, float newSpeed)
        {
            Vector3 newPos = newTransform.position;
            
            transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);
            transform.rotation = newTransform.rotation;
            gameObject.SetActive(true);

            _speed = newSpeed;

            _isOnRoad = true;

            _coroutine ??= StartCoroutine(MoveMe());
        }

        public void RemoveFromRoad()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            _isOnRoad = false;
            
            gameObject.SetActive(false);
        }
    }
}