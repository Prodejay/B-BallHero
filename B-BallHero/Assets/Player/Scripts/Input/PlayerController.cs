using System;
using System.ComponentModel;
using UnityEngine;

namespace BBallHero.Gameplay.Player.Input
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private InputReader _inputReader;

        [SerializeField, Header("Player Modules")]
        private MovementModule _movement;

        [SerializeField, ReadOnly(true)]
        private Vector2 _vector2Input;
        [SerializeField, ReadOnly(true)]
        private Vector2 _mouseVector2Input;

        private void OnEnable()
        {
            _inputReader.CameraMovementPerformed += OnCameraMovementPerformed;
            _inputReader.MovementPerformed += OnMovementPerformed;
            _inputReader.MovementCancelled += OnMovementCancelled;
            _inputReader.SprintPerformed += OnSprintPerformed;
            _inputReader.SprintCancelled += OnSprintCancelled;
            _inputReader.ShootPerformed += OnShootPerformed;
            _inputReader.ShootCancelled += OnShootCancelled;
            _inputReader.LockOnPerformed += OnLockOnPerformed;
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
            _inputReader.LockOnPerformed -= OnLockOnPerformed;
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
            
        }

        private void FixedUpdate()
        {
            _movement.HandleMovement(_vector2Input);
            _movement.HandleRotation(_vector2Input);
        }

        #region Input Event Functions
        private void OnLockOnPerformed()
        {
            
        }

        private void OnShootCancelled()
        {
            
        }

        private void OnShootPerformed()
        {
            
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
        }

        private void OnMovementPerformed(Vector2 vector)
        {
            _vector2Input = vector;
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

