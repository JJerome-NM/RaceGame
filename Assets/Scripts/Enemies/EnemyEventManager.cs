using UnityEngine;
using UnityEngine.Events;

namespace Enemies
{
    public class EnemyEventManager : MonoBehaviour
    {
        public static readonly UnityEvent<EnemyCarController> OnEnemyOutOfBounds = new();

        public static void EnemyOutOfBounds(EnemyCarController enemy)
        {
            OnEnemyOutOfBounds.Invoke(enemy);
        }
    }
}