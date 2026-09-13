using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RF.Core
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;

        public static int HighScore;

        public static List<int> HighscoreList = new();

        [SerializeField] private int score;

        [SerializeField] private AudioClip pointUpClip;
        [SerializeField] private AudioClip pointDownClip;
        [SerializeField] private float pointAudioVolume;

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
                    HighscoreList.Add(score);
                    HighscoreList.Sort();
                }
            }
        }

        public void AddScore(int amount)
        {
            if (GameManager.Instance.State != GameState.Running) return;

            score = Mathf.Max(score + amount, 0);

            if (pointDownClip != null && pointUpClip != null)
            {
                if (amount < 0)
                {
                    AudioSource.PlayClipAtPoint(pointDownClip, Camera.main.transform.position, pointAudioVolume);
                }
                else
                {
                    AudioSource.PlayClipAtPoint(pointUpClip, Camera.main.transform.position, pointAudioVolume);
                }
            }

            onScoreChanged?.Invoke();
        }

        public int GetScore()
        {
            return score;
        }
    }


}
