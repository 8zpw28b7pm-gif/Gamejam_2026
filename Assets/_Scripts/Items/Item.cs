using RF.Core;
using RF.Items;
using UnityEngine;

namespace RF.GameLoop
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemSO itemSO;

        private Rigidbody2D rb;

        private bool hasTouchedTrampoline = false;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            GameManager.Instance.ItemTracker.RegisterItem(this);
        }

        public ItemSO GetItemSO()
        {
            return itemSO;
        }

        public bool ApplyForce(Vector2 direction)
        {
            if (IsRising()) return false;

            rb.linearVelocity = Vector2.zero;
            rb.AddForce(direction, ForceMode2D.Impulse);

            hasTouchedTrampoline = true;

            return true;
        }

        public bool IsRising()
        {
            return rb.linearVelocityY > 0.01f;
        }

        public bool IsFalling()
        {
            return rb.linearVelocityY < -0.025f;
        }

        public bool HasTouchedTrampoline()
        {
            return hasTouchedTrampoline;
        }

        public bool IsSettled()
        {
            return rb.linearVelocity.y < 0.01f && rb.linearVelocity.y > -0.01f;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                GameManager.Instance.PlayerHealth.TakeDamage(1);

                GameManager.Instance.AudioManager.PlayFallOffScreenSound();

                DestroySelf();
            }
        }

        public void DestroySelf()
        {
            GameManager.Instance.ItemTracker.DeregisterItem(this);
            Destroy(gameObject);
        }
    }
}
