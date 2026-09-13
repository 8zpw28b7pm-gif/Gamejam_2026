using System;
using RF.Core;
using UnityEngine;

namespace RF.Core
{
    [DefaultExecutionOrder(-9999)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private GameState state;

        public GameState State => state;

        public AudioManager AudioManager { get; set; }
        public GameObject Player { get; set; }
        public Health PlayerHealth { get; set; }
        public ScoreManager ScoreManager { get; set; }
        public ItemTracker ItemTracker { get; set; }
        public ShelfSpawner ShelfSpawner { get; set; }

        private float timeSinceGameStart = 0;

        public event Action onStateChanged;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning("Multiple GameManager Instances!");
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            SetState(GameState.WaitingToStart, true);
        }

        private void Update()
        {
            if (state == GameState.Running)
            {
                timeSinceGameStart += Time.deltaTime;
            }
        }

        public float GetTimeSinceGameStart()
        {
            return timeSinceGameStart;
        }

        public void SetState(GameState newState, bool forceReset = false)
        {
            if (newState == state && !forceReset) return;

            state = newState;

            onStateChanged?.Invoke();



            switch (newState)
            {
                case GameState.WaitingToStart:
                    Time.timeScale = 0f;
                    break;
                case GameState.Running:
                    timeSinceGameStart = 0f;
                    Time.timeScale = 1f;
                    break;
                case GameState.Paused:
                    Time.timeScale = 0f;
                    break;
                case GameState.GameOver:
                    Time.timeScale = 0f;
                    break;
            }
        }
    }
}
