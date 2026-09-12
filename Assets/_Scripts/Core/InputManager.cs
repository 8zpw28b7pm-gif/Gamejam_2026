using UnityEngine;

namespace RF.Core
{
    public class InputManager : MonoBehaviour
    {
        private PlayerControls playerControls;

        private void Awake()
        {
            playerControls = new PlayerControls();
        }

        private void Start()
        {
            Cursor.visible = false;
        }

        private void OnEnable()
        {
            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }

        public Vector2 GetMovementVectorNormalized()
        {
            return playerControls.Player.Move.ReadValue<Vector2>().normalized;
        }

    }
}
