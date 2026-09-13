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
        [SerializeField] private float lerpSpeed = 10f;
        [SerializeField, Min(0f)] private float maxMoveSpeed = 10f;
        [SerializeField] private float boundX = 3f;
        [SerializeField] private float minY = 0f;
        [SerializeField] private float maxY = 1f;

        [Header("BOUNCING")]
        [SerializeField] private float baseBounceSpeed = 10f;
        [SerializeField] private float minForce = 0f;
        [SerializeField] private float maxForce = 8f;
        [SerializeField] private float maxBounceAngle;

        [SerializeField] private Vector3 previousPosition;
        [SerializeField] private float currentVelocityY = 0f;
        [SerializeField] private float currentVelocityX = 0f;
        [SerializeField, Range(0, 1)] private float xSpeedFactor;

        [SerializeField] private Health health;

        public event Action onBounce;

        private void Awake()
        {
            if (health == null)
            {
                health = FindAnyObjectByType<Health>();
            }

            GameManager.Instance.Player = this.gameObject;
            GameManager.Instance.PlayerHealth = health;
        }

        private void OnEnable()
        {
            health.onDeath += HandleDeath;
            previousPosition = transform.position;
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
            Vector3 targetPosition = cam.ScreenToWorldPoint(
                Mouse.current.position.ReadValue()
            );

            targetPosition.x = Mathf.Clamp(targetPosition.x, -boundX, boundX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
            targetPosition.z = transform.position.z;

            Vector3 nextPosition = Vector3.Lerp(
                transform.position,
                targetPosition,
                lerpSpeed * Time.deltaTime
            );

            transform.position = Vector3.MoveTowards(
                transform.position,
                nextPosition,
                maxMoveSpeed * Time.deltaTime
            );

            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);

            CalculateVelocity();
        }

        private bool ControlsDisabled()
        {
            return GameManager.Instance.State == GameState.Paused || GameManager.Instance.State == GameState.GameOver;
        }

        private void CalculateVelocity()
        {
            currentVelocityY = (transform.position.y - previousPosition.y) / Time.deltaTime;
            currentVelocityX = (transform.position.x - previousPosition.x) / Time.deltaTime * xSpeedFactor;

            if (previousPosition != transform.position)
            {
                previousPosition = transform.position;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.TryGetComponent<Item>(out Item item)) return;

            float forceX = Mathf.Clamp(
                baseBounceSpeed * currentVelocityX,
                -maxForce,
                maxForce
            );

            float forceY = Mathf.Max(
                baseBounceSpeed,
                baseBounceSpeed * currentVelocityY
            );
            forceY = Mathf.Clamp(forceY, 1f, maxForce);

            if (item.ApplyForce(new Vector3(forceX, forceY, 0f)))
            {
                onBounce?.Invoke();
            }
        }

        private void HandleDeath()
        {
            GameManager.Instance.SetState(GameState.GameOver);
        }
    }
}
