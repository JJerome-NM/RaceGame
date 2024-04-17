using System.Collections;
using Scripts;
using UnityEngine;

namespace Road
{
    public class RoadPartsMovement : MonoBehaviour
    {
        [Header("Road")] 
        [SerializeField] private GameObject firstRoadPart;
        [SerializeField] private GameObject secondRoadPart;
        [SerializeField] private float firstRoadPartLength;
        [SerializeField] private float secondRoadPartLength;
        [SerializeField] private float roadSpeed = 10;
        
        [Header("Barriers")] 
        [SerializeField] private Transform endPosition;

        private Coroutine _coroutine;
        
        private void Start()
        {
            RoadEventManager.OnPlayerCarSpeedChange.AddListener(speed => roadSpeed = -speed);
            GlobalEventManager.OnGameStopped.AddListener(OnGameStopped);
            GlobalEventManager.OnGameStarted.AddListener(OnGameStarted);
        }

        private void OnGameStopped(GameEndState _)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }

        private void OnGameStarted()
        {
            _coroutine = StartCoroutine(MoveRoadParts());
        }
        
        private IEnumerator MoveRoadParts()
        {
            while (true)
            {
                if (endPosition.position.z > firstRoadPart.transform.position.z)
                {
                    Vector3 oldPos = secondRoadPart.transform.position;
                    firstRoadPart.transform.position = new Vector3(oldPos.x, oldPos.y, oldPos.z + secondRoadPartLength);
                } 
                else if (endPosition.position.z > secondRoadPart.transform.position.z)
                {
                    Vector3 oldPos = firstRoadPart.transform.position;
                    secondRoadPart.transform.position = new Vector3(oldPos.x, oldPos.y, oldPos.z + firstRoadPartLength);
                }
                else
                {
                    firstRoadPart.transform.position = MoveRoadPart(firstRoadPart.transform.position);
                    secondRoadPart.transform.position = MoveRoadPart(secondRoadPart.transform.position);
                }
                
                yield return null;
            }
        }
        
        private Vector3 MoveRoadPart(Vector3 position)
        {
            return new Vector3(position.x, position.y, position.z + Time.deltaTime * roadSpeed);
        }
    }
}