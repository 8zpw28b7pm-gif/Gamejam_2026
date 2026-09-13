using RF.Core;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace RF.UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthText;

        [SerializeField] private Health playerHealth;
        [SerializeField] private Button invulnerableButton;

        private void Awake()
        {
            UIHandler.Instance.HealthUI = this;

            invulnerableButton.onClick.AddListener(() => SetInvulnerable());
        }

        private void SetInvulnerable()
        {
            if (playerHealth.isInvulnerable)
            {
                playerHealth.SetInvulnerable(false);
            }
            else
            {
                playerHealth.SetInvulnerable(true);
            }
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
