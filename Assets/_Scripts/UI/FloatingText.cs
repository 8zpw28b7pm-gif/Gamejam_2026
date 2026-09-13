using System;
using TMPro;
using UnityEngine;

namespace RF.UI
{
    public class FloatingText : MonoBehaviour
    {
        [SerializeField] TextMeshPro scoreText;

        [SerializeField] private Color goodColor;
        [SerializeField] private Color greatColor;
        [SerializeField] private Color negativeColor;

        [SerializeField] private float destroyTime;

        private void Start()
        {
            Destroy(gameObject, destroyTime);
        }
        public void Init(int score)
        {
            if (score < 0)
            {
                scoreText.text = $"{score.ToString()}";
                scoreText.color = negativeColor;
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (score > 0 && score <= 50)
            {
                scoreText.text = $"+{score.ToString()}";
                scoreText.color = goodColor;
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (score > 50)
            {
                scoreText.text = $"+{score.ToString()}";
                scoreText.color = greatColor;
                transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            }
        }
    }
}
