using UnityEngine;

namespace RF.Core
{
    public class InputManager : MonoBehaviour
    {
        private PlayerControls playerControls;

        private void Awake()
        {
            playerControls = new PlayerControls();

            playerControls.Enable();
        }

        public float GetMoveValue()
        {
            return playerControls.Player.Move.ReadValue<float>();
        }

        public float GetTiltValue()
        {
            return playerControls.Player.Tilt.ReadValue<float>();
        }
    }
}
