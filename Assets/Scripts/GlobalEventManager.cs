using UnityEngine;
using UnityEngine.Events;

namespace Scripts
{
    public class GlobalEventManager : MonoBehaviour
    {
        public static readonly UnityEvent<GameEndState> OnGameStopped = new();
        public static readonly UnityEvent OnGameStarted = new();
        
        public static void StopGame(GameEndState state)
        {
            OnGameStopped.Invoke(state);
        }

        public static void StartGame()
        {
            OnGameStarted.Invoke();
        }
    }
}