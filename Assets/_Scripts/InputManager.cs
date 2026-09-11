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

        private void Update()
        {
            Debug.Log(GetMoveValue());
            Debug.Log(GetTiltValue());
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
