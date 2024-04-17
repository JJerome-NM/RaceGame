using Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UiController : MonoBehaviour
    {
        [SerializeField] private GameObject mainPane;
        [SerializeField] private TextMeshProUGUI lostText;
        [SerializeField] private Button startButton;
        [SerializeField] private Button stopButton;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            GlobalEventManager.OnGameStopped.AddListener(OnGameStopped);
            GlobalEventManager.OnGameStarted.AddListener(OnGameStarted);

            startButton.onClick.AddListener(GlobalEventManager.StartGame);
            stopButton.onClick.AddListener(() => GlobalEventManager.StopGame(GameEndState.PlayerStoppedGame));
            
            GlobalEventManager.StopGame(GameEndState.PlayerStoppedGame);
        }

        private void OnGameStarted()
        {
            mainPane.SetActive(false);
            stopButton.gameObject.SetActive(true);
        }
        
        private void OnGameStopped(GameEndState state)
        {
            mainPane.SetActive(true);
            stopButton.gameObject.SetActive(false);
            
            switch (state)
            {
                case GameEndState.PlayerCrashed:
                {
                    lostText.SetText("You crashed");
                    break;
                }
                case GameEndState.PlayerStoppedGame:
                {
                    lostText.SetText("The game is stopped");
                    break;
                }
            }
        }
    }
}