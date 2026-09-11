using UnityEngine;

namespace RF.GameLoop
{
    public class KitchenObject : MonoBehaviour
    {
        [SerializeField] private float bounceForce = 5;
        [SerializeField] private LayerMask shelfLayer;
        [SerializeField] private LayerMask wallLayer;

        private Rigidbody2D rb;
        private bool isFalling;

        private BoxCollider2D col;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<BoxCollider2D>();
        }

        private void OnEnable()
        {
            CupCounter.Instance.RegisterCup();
        }
        private void OnDisable()
        {
            CupCounter.Instance.DeregisterCup();
        }
        private void Update()
        {
            isFalling = rb.linearVelocityY < 0.01f;

            col.excludeLayers = LayerMask.NameToLayer("Shelf");
            if (isFalling)
            {
                col.excludeLayers = 6;
            }
            else
            {
                col.excludeLayers = -1;
            }
        }

        public void ApplyForce(Vector2 direction)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(direction * bounceForce, ForceMode2D.Impulse);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                DestroySelf();
            }
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
