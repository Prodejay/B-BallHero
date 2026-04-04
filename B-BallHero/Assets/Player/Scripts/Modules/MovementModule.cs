using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BBallHero.Gameplay.Player
{
    public class MovementModule : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField]
        private float _currentMoveSpeed;
        [SerializeField]
        private float _moveSpeed = 10f;
        [SerializeField]
        private float _sprintSpeed = 15f;
        [SerializeField]
        private float _maxSpeed = 20f;
        [SerializeField]
        private float _rotationDuration = 1f;
        [SerializeField]
        private float _aimRotationSpeed = 5f;
        [SerializeField]
        private float _brakeForce = 10f;

        float _turnSmoothVelocity;
        private Transform _mainCamera;
        bool isSprinting; 
        private Vector3 _moveDirection;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.freezeRotation = true;
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _currentMoveSpeed = _moveSpeed;
            _mainCamera = Camera.main.transform;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void HandleSprint(bool isSprinting)
        {
            if (isSprinting)
            {
                _currentMoveSpeed = _sprintSpeed;
            }
            else
            {
                _currentMoveSpeed = _moveSpeed;
            }
        }

        public void HandleMovement(Vector2 vector2Input)
        {
            _moveDirection = _mainCamera.forward * vector2Input.y + _mainCamera.right * vector2Input.x;
            if (_rb.linearVelocity.magnitude > _maxSpeed)
            {
                _rb.linearVelocity = _rb.linearVelocity.normalized * _maxSpeed;
            }
            else
            {
                _rb.AddForce(_moveDirection.normalized * _currentMoveSpeed, ForceMode.Force);
            }
        }

        public void HandleRotation(Vector2 vector2Input)
        {
            Vector3 inputDir = new Vector3(vector2Input.x, 0, vector2Input.y).normalized;

            if(inputDir.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + _mainCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _rotationDuration);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }

        public void RotateWhileAiming(Vector2 vector2Input)
        {
            if(vector2Input.x != 0)
            {
                float angle = vector2Input.x * _aimRotationSpeed * Time.deltaTime;
                Quaternion deltaRotation = Quaternion.Euler(0f, angle, 0f);
                transform.rotation *= deltaRotation;
            }
        }

        public void HandleBraking()
        {
            Vector3 reverseForce = -_rb.linearVelocity * _brakeForce;
            _rb.AddForce(reverseForce, ForceMode.Force);
        }

        public void ForceRotateForThrow()
        {
            Vector3 cameraEuler = _mainCamera.rotation.eulerAngles;
            Quaternion targetRotation = Quaternion.Euler(0, cameraEuler.y, 0);
            transform.rotation = targetRotation;
        }
    }
}

