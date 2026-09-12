using System;
using RF.Core;
using TMPro;
using UnityEngine;

namespace RF.UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthText;

        [SerializeField] private Health playerHealth;

        private void Awake()
        {
            UIHandler.Instance.HealthUI = this;
        }

        private void OnEnable()
        {
            playerHealth.onHealthChanged += UpdateHealth;
        }

        private void OnDisable()
        {
            playerHealth.onHealthChanged -= UpdateHealth;
        }

        private void Start()
        {
            UpdateHealth();
        }
      
        private void UpdateHealth()
        {
            healthText.text = playerHealth.GetHealth().ToString();
        }
    }
}
