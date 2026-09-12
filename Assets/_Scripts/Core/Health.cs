using System;
using UnityEngine;

namespace RF.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int health;
        [SerializeField] private int healthMax;

        private bool isDead;

        public event Action onHealthChanged;
        public event Action onDeath;

        private void OnEnable()
        {
            health = healthMax;
        }

        public int GetHealth()
        {
            return health;
        }

        public int GetMaxHealth()
        {
            return healthMax;
        }

        public void TakeDamage(int amount)
        {
            if (isDead) return;

            health = Mathf.Max(0, health - amount);

            onHealthChanged?.Invoke();

            Debug.Log("Take Damage");
            
            if (health == 0)
            {
                Die();
            }
        }

        public void Die()
        {
            if (isDead) return;

            isDead = true;

            onDeath?.Invoke();
        }
    }
}
