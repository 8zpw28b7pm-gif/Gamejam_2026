using System;
using RF.Core;
using TMPro;
using UnityEngine;

namespace RF.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private ScoreManager scoreManager;

        private void OnEnable()
        {
            scoreManager.onScoreChanged += UpdateScoreText;
        }

        private void OnDisable()
        {
            scoreManager.onScoreChanged -= UpdateScoreText;
        }

        private void UpdateScoreText()
        {
            scoreText.text = scoreManager.GetScore().ToString();
        }


    }
}
