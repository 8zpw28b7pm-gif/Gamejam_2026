using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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

        }

        private void OnEnable()
        {
            playerControls.Player.Escape.performed += OnEscape;
            playerControls.Enable();

            GameManager.Instance.onStateChanged += GameManager_OnStateChanged;
        }

        private void OnEscape(InputAction.CallbackContext context)
        {
            GameState state = GameManager.Instance.State;
            
            if (state == GameState.Running || state == GameState.WaitingToStart)
            {
                GameManager.Instance.SetState(GameState.Paused);
            }
            else if (state == GameState.Paused)
            {
                GameManager.Instance.SetState(GameState.Running);
            }
        }

        private void OnDisable()
        {
            playerControls.Disable();

            GameManager.Instance.onStateChanged -= GameManager_OnStateChanged;
        }

        private void GameManager_OnStateChanged()
        {
            if (GameManager.Instance.State == GameState.Running)
            {
                Cursor.visible = false;
            }
            else
            {
                Cursor.visible = true;
            }
        }

        public Vector2 GetMovementVectorNormalized()
        {
            return playerControls.Player.Move.ReadValue<Vector2>().normalized;
        }

    }
}
