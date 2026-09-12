using RF.Core;
using RF.Items;
using UnityEngine;

namespace RF.GameLoop
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemSO itemSO;

        [SerializeField] private float bounceForce = 5;
        // [SerializeField] private Collider2D contactCollider;

        private Rigidbody2D rb;

        private bool hasTouchedTrampoline = false;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public ItemSO GetItemSO()
        {
            return itemSO;
        }

        public void ApplyForce(Vector2 direction)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(direction, ForceMode2D.Impulse);

            hasTouchedTrampoline = true;
        }

        public bool IsRising()
        {
            return rb.linearVelocityY > 0.01f;
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
                DestroySelf();
            }
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
