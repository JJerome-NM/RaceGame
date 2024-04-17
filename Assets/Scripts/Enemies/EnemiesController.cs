using System.Collections;
using System.Collections.Generic;
using Scripts;
using UnityEngine;

namespace Enemies
{
    public class EnemiesController : MonoBehaviour
    {
        [SerializeField] private Transform rightPosition;
        [SerializeField] private Transform leftPosition;
        
        [SerializeField] private List<EnemyCarController> carsPool;
        [SerializeField] private int maxCarsOnTheRoad = 5;
        
        
        private readonly List<EnemyCarController> _carsAtRoad = new();
        private Coroutine _coroutine;
        
        private void Start()
        {
            foreach (EnemyCarController controller in carsPool)
            {
                controller.RemoveFromRoad();
            }   
            
            GlobalEventManager.OnGameStarted.AddListener(OnGameStarted);            
            GlobalEventManager.OnGameStopped.AddListener(OnGameStopped);      
            EnemyEventManager.OnEnemyOutOfBounds.AddListener(enemy =>
            {
                enemy.RemoveFromRoad();
                _carsAtRoad.Remove(enemy);
                carsPool.Add(enemy);
            });
        }

        private void OnGameStarted()
        {
            RemoveAllFromRoad();
            
            _coroutine ??= StartCoroutine(SpawnEnemyCars());
        }

        
        private void OnGameStopped(GameEndState state)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            RemoveAllFromRoad();
        }

        private void RemoveAllFromRoad()
        {
            foreach (EnemyCarController carController in _carsAtRoad)
            {
                carController.RemoveFromRoad();
            }
            
            carsPool.AddRange(_carsAtRoad);
            _carsAtRoad.Clear();
        }
        
        private IEnumerator SpawnEnemyCars()
        {
            while (!GlobalGameStates.IsGameStopped)
            {
                if (_carsAtRoad.Count <= maxCarsOnTheRoad && carsPool.Count != 0 && Random.Range(0, 4) > 1)
                {
                    var enemy = carsPool[Random.Range(0, carsPool.Count)];
                    
                    if (Random.Range(0, 2) == 1)
                    {
                        enemy.SpawnOnTheRoad(rightPosition, Random.Range(5, 15));
                    }
                    else
                    {
                        enemy.SpawnOnTheRoad(leftPosition, Random.Range(-5, -10));
                    }
                    
                    _carsAtRoad.Add(enemy);
                    carsPool.Remove(enemy);
                }
                yield return new WaitForSeconds(1);
            }
            _coroutine = null;
        }
    }
}