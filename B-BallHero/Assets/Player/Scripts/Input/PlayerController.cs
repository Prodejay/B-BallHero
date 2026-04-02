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
        private float _currentMoveSpeed;
        [SerializeField]
        private float _moveSpeed = 10f;
        [SerializeField]
        private float _sprintSpeed = 15f;
        [SerializeField, ReadOnly(true)]
        private Vector2 _Vector2Input;

        private Vector3 _moveDirection;

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

        private void Start()
        {
            _currentMoveSpeed = _moveSpeed;
        }

        private void Update()
        {
            HandleMovement();
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
            _currentMoveSpeed = _moveSpeed;
        }

        private void OnSprintPerformed()
        {
            _currentMoveSpeed = _sprintSpeed;
        }

        private void OnMovementCancelled(Vector2 vector)
        {
            _Vector2Input = Vector2.zero;
        }

        private void OnMovementPerformed(Vector2 vector)
        {
            _Vector2Input = vector;
        }

        private void OnCameraMovementPerformed(Vector2 vector)
        {
            
        }
        #endregion

        #region Action Handle
        private void HandleMovement()
        {

            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraRight = Camera.main.transform.right;

            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 moveDirection = (cameraForward * _Vector2Input.y) + (cameraRight * _Vector2Input.x);

            transform.Translate(moveDirection * _currentMoveSpeed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);
        }
        #endregion
    }
}

