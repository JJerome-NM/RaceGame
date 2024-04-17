using UnityEngine;
using UnityEngine.Events;

namespace Road
{
    public class RoadEventManager : MonoBehaviour
    {
        public static readonly UnityEvent<float> OnPlayerCarSpeedChange = new();

        public static void ChangePlayerSpeed(float newSpeed)
        {
            OnPlayerCarSpeedChange.Invoke(newSpeed);
        }
    }
}