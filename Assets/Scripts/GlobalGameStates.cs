using UnityEngine;

namespace Scripts
{
    public class GlobalGameStates : MonoBehaviour
    {
        public static bool IsGameStopped = true;

        private void Start()
        {
            GlobalEventManager.OnGameStarted.AddListener(() => IsGameStopped = false);
            GlobalEventManager.OnGameStopped.AddListener(_ => IsGameStopped = true);
        }
    }
}