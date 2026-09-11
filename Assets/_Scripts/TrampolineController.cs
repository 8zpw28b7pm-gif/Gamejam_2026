using RF.Core;
using RF.GameLoop;
using UnityEngine;

namespace RF.Control
{
    public class TrampolineController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float tiltSpeed = 20f;

        [SerializeField] private float maxTilt;


        Vector2 movementVector;
        [SerializeField] float tilt;

        private InputManager inputManager;

        private void Awake()
        {
            inputManager = FindAnyObjectByType<InputManager>();
        }

        private void Update()
        {
            movementVector = new Vector2(inputManager.GetMoveValue(), 0);


            HandleMovement();
            HandleTilt();
        }

        private void HandleMovement()
        {
            transform.Translate(movementVector * moveSpeed * Time.deltaTime, Space.World);
        }

        private void HandleTilt()
        {
            float tiltValue = inputManager.GetTiltValue();

            transform.Rotate(0, 0, inputManager.GetTiltValue() * tiltSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.TryGetComponent<KitchenObject>(out KitchenObject kitchenObject)) return;

            Vector3 kitchenObjectMoveDir = new Vector3(-transform.rotation.z, 1, 0).normalized;
            
            kitchenObject.ApplyForce(kitchenObjectMoveDir);
        }
    }
}
