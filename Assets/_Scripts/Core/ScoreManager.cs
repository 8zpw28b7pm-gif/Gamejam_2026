using System;
using UnityEngine;

namespace RF.Core
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;

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

        public void AddScore(float distanceToSillhouette)
        {
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
            score--;
            onScoreChanged?.Invoke();
        }

        public int GetScore()
        {
            return score;
        }
    }


}
