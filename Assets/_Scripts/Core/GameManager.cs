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

        public GameObject Player { get; set; }
        public Health PlayerHealth { get; set; }
        public ScoreManager ScoreManager { get; set; }

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

        public void SetState(GameState newState, bool forceReset = false)
        {
            if (newState == state && !forceReset) return;

            state = newState;
            
            onStateChanged?.Invoke();
        }
    }
}
