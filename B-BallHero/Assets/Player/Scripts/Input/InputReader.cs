using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BBallHero.Gameplay.Player.Input
{
    [CreateAssetMenu(fileName = "Input Reader")]
    public class InputReader : ScriptableObject, IA_PlayerInput.IGameplayActions, IA_PlayerInput.IUIActions
    {
        private IA_PlayerInput _playerInput;

        private void OnEnable()
        {
            if(_playerInput == null)
            {
                _playerInput = new IA_PlayerInput();

                _playerInput.Gameplay.SetCallbacks(this);
                _playerInput.UI.SetCallbacks(this);
            }
        }

        public void SetInputToGameplay()
        {
            _playerInput.Gameplay.Enable();
            _playerInput.UI.Disable();

        }

        public void SetInputToUI()
        {
            _playerInput.Gameplay.Disable();
            _playerInput.UI.Enable();
        }

        public event Action<Vector2> CameraMovementPerformed;
        public event Action<Vector2> MovementPerformed;
        public event Action<Vector2> MovementCancelled;
        public event Action SprintPerformed;
        public event Action SprintCancelled;
        public event Action ShootPerformed;
        public event Action ShootCancelled;
        public event Action CancelShotPerformed;
        public event Action PausePerformed;


        public void OnCameraMovement(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                CameraMovementPerformed?.Invoke(context.ReadValue<Vector2>());
            }
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                MovementPerformed?.Invoke(context.ReadValue<Vector2>());
            }

            if (context.canceled)
            {
                MovementCancelled?.Invoke(context.ReadValue<Vector2>());
            }
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed) 
            {
                SprintPerformed?.Invoke();
            }

            if (context.canceled)
            {
                SprintCancelled?.Invoke();
            }
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                ShootPerformed?.Invoke();
            }

            if (context.canceled)
            {
                ShootCancelled?.Invoke();
            }
        }

        public void OnCancelShot(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                CancelShotPerformed?.Invoke();
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                PausePerformed?.Invoke();
            }
        }
    }
}

