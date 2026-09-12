using System;
using RF.Core;
using RF.GameLoop;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RF.Control
{
    public class PlayerController : MonoBehaviour
    {
        [Header("MOVEMENT")]
        [SerializeField] private float verticalMoveSpeed = 5f;
        [SerializeField] private float boundX = 3f;
        [SerializeField] private float minY = 0f;
        [SerializeField] private float maxY = 1f;

        [Header("BOUNCING")]
        [SerializeField] private float minBounce = 10f;
        [SerializeField] private float maxBounce = 12.5f;
        [SerializeField] private float currentBounce;
        [SerializeField] private float maxBounceAngle;

        [Header("STRETCHING")]
        [SerializeField] private float minSizeX;
        [SerializeField] private float maxSizeX;
        [SerializeField] private float minSizeY;
        [SerializeField] private float maxSizeY;

        private InputManager inputManager;
        private Health health;

        private Vector2 movementVector;
        private bool controlsDisabled = false;

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
            if (GameManager.Instance.State == GameState.WaitingToStart)
            {
                if (Keyboard.current.spaceKey.wasPressedThisFrame)
                {
                    GameManager.Instance.SetState(GameState.Running);
                }
            }

            if (ControlsDisabled()) return;

            Camera cam = Camera.main;
            Vector3 position = cam.ScreenToWorldPoint(
                Mouse.current.position.ReadValue()
            );

            position.x = Mathf.Clamp(position.x, -boundX, boundX);
            position.y = Mathf.Clamp(position.y, minY, maxY);
            position.z = transform.position.z;

            transform.position = position;

            CalculateBounce();
        }

        private bool ControlsDisabled()
        {
            return GameManager.Instance.State == GameState.Paused || GameManager.Instance.State == GameState.GameOver;
        }

        private void CalculateBounce()
        {
            currentBounce = Mathf.Lerp(minBounce, maxBounce, GetCurrentYPositionFraction());
        }

        private float GetCurrentYPositionFraction()
        {
            return 1f - Mathf.InverseLerp(minY, maxY, transform.position.y);
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.TryGetComponent<Item>(out Item item)) return;

            float differenceX = item.transform.position.x - transform.position.x;
            differenceX = Mathf.Clamp(differenceX, -maxBounceAngle, maxBounceAngle);

            Vector3 itemMoveDir = new Vector3(differenceX, 1, 0).normalized;

            item.ApplyForce(itemMoveDir * Mathf.Max(currentBounce, minBounce));

            float factor = 1f - Mathf.InverseLerp(minY, maxY, transform.position.y);
            float power = maxBounce * factor;

            onBounce?.Invoke();
        }

        private void HandleDeath()
        {
            GameManager.Instance.SetState(GameState.GameOver);
        }
    }
}
