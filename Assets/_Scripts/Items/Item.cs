using RF.Core;
using RF.Items;
using UnityEngine;

namespace RF.GameLoop
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemSO itemSO;

        [SerializeField] private float bounceForce = 5;
        [SerializeField] private Collider2D contactCollider;

        int itemMask = -1;

        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            itemMask = LayerMask.GetMask("Item");
            contactCollider.enabled = false;
        }

        private void Update()
        {
            if (IsRising())
            {
                contactCollider.excludeLayers |= itemMask;
            }
            else
            {
                contactCollider.excludeLayers &= ~itemMask;
            }
        }

        public ItemSO GetItemSO()
        {
            return itemSO;
        }

        public void ApplyForce(Vector2 direction)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(direction * bounceForce, ForceMode2D.Impulse);

            contactCollider.enabled = true;
        }

        public bool IsRising()
        {
            return rb.linearVelocityY > 0.01f;
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

        private void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
