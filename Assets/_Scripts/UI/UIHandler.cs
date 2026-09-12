using System;
using RF.Core;
using UnityEngine;

namespace RF.UI
{
    [DefaultExecutionOrder(-8888)]
    public class UIHandler : MonoBehaviour
    {
        public static UIHandler Instance;

        public ScoreUI ScoreUI { get; set; }
        public HealthUI HealthUI { get; set; }

        [SerializeField] private StartPromptUI startPromptUI;
        [SerializeField] private GameOverMenuUI gameOverMenuUI;
        [SerializeField] private InGameMenuUI inGameMenuUI;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning("Multiple UIHandler Instances!");
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void OnEnable()
        {
            GameManager.Instance.onStateChanged += GameManager_OnStateChanged;
        }
        private void OnDisable()
        {
            GameManager.Instance.onStateChanged -= GameManager_OnStateChanged;
        }

        private void GameManager_OnStateChanged()
        {
            HideAllMenus();

            switch (GameManager.Instance.State)
            {
                case GameState.WaitingToStart:
                    startPromptUI.gameObject.SetActive(true);
                    break;
                case GameState.Running:
                    HideAllMenus();
                    break;
                case GameState.Paused:
                    inGameMenuUI.gameObject.SetActive(true);
                    break;
                case GameState.GameOver:
                    gameOverMenuUI.gameObject.SetActive(true);
                    break;
            }
        }

        public void HideAllMenus()
        {
            startPromptUI.gameObject.SetActive(false);
            inGameMenuUI.gameObject.SetActive(false);
            gameOverMenuUI.gameObject.SetActive(false);
        }   
    }
}