using System;
using Unity.VisualScripting;
using UnityEngine;

namespace RF.Core
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;

        public static int HighScore;

        public enum Scores
        {
            BAD = -1,
            NONE = 0,
            GOOD = 1,
            GREAT = 2,
            PERFECT = 5
        }

        [SerializeField] private int score;

        public event Action onScoreChanged;

        private void Awake()
        {
            Instance = this;

            GameManager.Instance.ScoreManager = this;
        }

        private void OnEnable()
        {
            GameManager.Instance.onStateChanged += GameManager_OnStateChanged;
        }

        private void GameManager_OnStateChanged()
        {
            if (GameManager.Instance.State == GameState.GameOver)
            {
                if (score > HighScore)
                {
                    HighScore = score;
                }
            }
        }

        public void AddScore(float distanceToSillhouette)
        {
            if (GameManager.Instance.State != GameState.Running) return;

            Scores scoreForItem = Scores.NONE;

            if (distanceToSillhouette > 0.5f)
            {
                scoreForItem = Scores.GOOD;
            }
            else if (distanceToSillhouette < 0.5f && distanceToSillhouette > 0.2f)
            {
                scoreForItem = Scores.GREAT;
            }
            else if (distanceToSillhouette < 0.1f)
            {
                scoreForItem = Scores.PERFECT;
            }

            score += (int)scoreForItem;

            onScoreChanged?.Invoke();
        }

        public void RemoveScore()
        {
            if (GameManager.Instance.State != GameState.Running) return;
            
            score--;
            onScoreChanged?.Invoke();
        }

        public int GetScore()
        {
            return score;
        }
    }


}
