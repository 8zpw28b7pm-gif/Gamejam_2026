using System;
using RF.Core;
using RF.GameLoop;
using UnityEngine;

namespace RF.Control
{
    public class PlayerController : MonoBehaviour
    {
        [Header("MOVEMENT")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float boundX = 2.5f;

        [Header("BOUNCING")]
        [SerializeField] private float maxBounceAngle;

        private InputManager inputManager;
        private Health health;

        Vector2 movementVector;

        public event Action onBounce;

        private void Awake()
        {
            inputManager = FindAnyObjectByType<InputManager>();
            health = GetComponent<Health>();

            GameManager.Instance.Player = this.gameObject;
            GameManager.Instance.PlayerHealth = health;
        }

        private void OnEnable()
        {
            health.onDeath += HandleDeath;
        }

        private void OnDisable()
        {
            health.onDeath -= HandleDeath;
        }

        private void Update()
        {
            movementVector = new Vector2(inputManager.GetMoveValue(), 0);

            HandleMovement();
        }

        private void HandleMovement()
        {
            if (transform.position.x < -boundX && movementVector.x < 0) return;
            if (transform.position.x > boundX && movementVector.x > 0) return;

            transform.Translate(movementVector * moveSpeed * Time.deltaTime, Space.World);
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.TryGetComponent<Item>(out Item kitchenObject)) return;

            float differenceX = kitchenObject.transform.position.x - transform.position.x;
            differenceX = Mathf.Clamp(differenceX, -maxBounceAngle, maxBounceAngle);

            Vector3 kitchenObjectMoveDir = new Vector3(differenceX, 1, 0).normalized;

            kitchenObject.ApplyForce(kitchenObjectMoveDir);

            onBounce?.Invoke();
        }

        private void HandleDeath()
        {
            GameManager.Instance.SetState(GameState.GameOver);
        }
    }
}
