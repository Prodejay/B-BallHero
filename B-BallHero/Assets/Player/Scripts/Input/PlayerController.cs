using System;
using System.ComponentModel;
using UnityEngine;

namespace BBallHero.Gameplay.Player.Input
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private InputReader _inputReader;
        [SerializeField]
        private Player _player;

        [SerializeField, Header("Player Modules")]
        private MovementModule _movement;
        [SerializeField]
        private ShootModule _throwBall;

        [SerializeField, ReadOnly(true)]
        private Vector2 _vector2Input;
        [SerializeField, ReadOnly(true)]
        private Vector2 _mouseVector2Input;

        private bool _canMove = true;
        private bool _isAiming = false;

        public event Action MovementStarted;
        public event Action MovementStopped;

        private void OnEnable()
        {
            _inputReader.CameraMovementPerformed += OnCameraMovementPerformed;
            _inputReader.MovementPerformed += OnMovementPerformed;
            _inputReader.MovementCancelled += OnMovementCancelled;
            _inputReader.SprintPerformed += OnSprintPerformed;
            _inputReader.SprintCancelled += OnSprintCancelled;
            _inputReader.ShootPerformed += OnShootPerformed;
            _inputReader.ShootCancelled += OnShootCancelled;
            _inputReader.CancelShotPerformed += OnCancelShotPerformed;
        }

        private void OnDisable()
        {
            _inputReader.CameraMovementPerformed -= OnCameraMovementPerformed;
            _inputReader.MovementPerformed -= OnMovementPerformed;
            _inputReader.MovementCancelled -= OnMovementCancelled;
            _inputReader.SprintPerformed -= OnSprintPerformed;
            _inputReader.SprintCancelled -= OnSprintCancelled;
            _inputReader.ShootPerformed -= OnShootPerformed;
            _inputReader.ShootCancelled -= OnShootCancelled;
            _inputReader.CancelShotPerformed -= OnCancelShotPerformed;
        }

        private void Awake()
        {

        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }

        private void Update()
        {
            if (_isAiming)
            {
                _movement.RotateWhileAiming(_vector2Input);
            }
        }

        private void FixedUpdate()
        {
            if (_isAiming == false)
            {
                _movement.HandleMovement(_vector2Input);
                _movement.HandleRotation(_vector2Input);

                if(_vector2Input == Vector2.zero)
                {
                    _movement.HandleBraking();
                }
            }
        }

        #region Input Event Functions
        private void OnCancelShotPerformed()
        {
            if (_player.hasBasketball == false)
                return;

            _throwBall.CancelThrow();
            _isAiming = false;
            _canMove = true;
            GameManager.instance.SetFreeLookCameraOn();
        }

        private void OnShootCancelled()
        {
            if (_player.hasBasketball == false)
                return;
            if (_isAiming == false)
                return;

            _canMove = true;
            _isAiming = false;
            _throwBall.ReleaseThrow();
            GameManager.instance.SetFreeLookCameraOn();
        }

        private void OnShootPerformed()
        {
            if (_player.hasBasketball == false)
                return;

            GameManager.instance.SetThirdPersonCameraOn();
            _movement.ForceRotateForThrow();
            _canMove = false;
            _isAiming = true;
            _throwBall.StartThrow();
        }

        private void OnSprintCancelled()
        {
            _movement.HandleSprint(false);
        }

        private void OnSprintPerformed()
        {
            _movement.HandleSprint(true);
        }

        private void OnMovementCancelled(Vector2 vector)
        {
            _vector2Input = Vector2.zero;
            MovementStopped?.Invoke();
        }

        private void OnMovementPerformed(Vector2 vector)
        {
            _vector2Input = vector;
            MovementStarted?.Invoke();
        }

        private void OnCameraMovementPerformed(Vector2 vector)
        {
            _mouseVector2Input = vector;
        }
        #endregion

        #region Action Handle

        #endregion
    }
}

