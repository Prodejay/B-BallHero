using System;
using UnityEngine;

namespace BBallHero.Gameplay.Player.Input
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private InputReader _inputReader;

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

        private void OnLockOnPerformed()
        {
            throw new NotImplementedException();
        }

        private void OnShootCancelled()
        {
            throw new NotImplementedException();
        }

        private void OnShootPerformed()
        {
            throw new NotImplementedException();
        }

        private void OnSprintCancelled()
        {
            throw new NotImplementedException();
        }

        private void OnSprintPerformed()
        {
            throw new NotImplementedException();
        }

        private void OnMovementCancelled(Vector2 vector)
        {
            
        }

        private void OnMovementPerformed(Vector2 vector)
        {
            Debug.Log("Movement Performed");
        }

        private void OnCameraMovementPerformed(Vector2 vector)
        {
            throw new NotImplementedException();
        }
    }
}

