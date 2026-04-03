using System;
using Unity.Jobs;
using UnityEngine;

namespace BBallHero.Gameplay.Player
{
    public class ShootModule : MonoBehaviour
    {
        [SerializeField]
        private GameObject _bballPrefab;
        [SerializeField]
        private Transform _shootStartPoint;
        [SerializeField]
        private Vector3 _throwDirection = new Vector3(0, 0, 0);
        [SerializeField]
        private float _throwForce;
        [SerializeField]
        private float _maxForce;
        [SerializeField]
        private float _chargeTime;
        private bool _isCharging = false;
        [SerializeField]
        private LineRenderer _trajectoryLine;

        public event Action ThrewBasketball;

        private void Start()
        {
            _trajectoryLine.enabled = false;
        }

        private void Update()
        {
            if (_isCharging)
            {
                ChargeThrow();
            }
        }

        public void StartThrow()
        {
            _isCharging = true;
            _chargeTime = 0;

            //Trajectory Line
            _trajectoryLine.enabled = true;
        }

        public void ChargeThrow()
        {
            _chargeTime += Time.deltaTime;

            //trajectory line velocity
            Vector3 ballVelocity = (transform.forward + _throwDirection).normalized * Mathf.Min(_chargeTime * _throwForce, _maxForce);
            ShowTrajectory(_shootStartPoint.position + transform.forward, ballVelocity);
        }

        private void ShowTrajectory(Vector3 origin, Vector3 velocity)
        {
            Vector3[] points = new Vector3[100];
            _trajectoryLine.positionCount = points.Length;
            for (int i = 0; i < points.Length; i++) 
            {
                float time = i * 0.1f;
                points[i] = origin + velocity * time + 0.5f * Physics.gravity * time * time;
            }

            _trajectoryLine.SetPositions(points);
        }

        public void ReleaseThrow()
        {
            ThrowBasketball(Mathf.Min(_chargeTime * _throwForce, _maxForce));
            _isCharging = false;
            _trajectoryLine.enabled = false;
            ThrewBasketball?.Invoke();
        }


        private void ThrowBasketball(float force)
        {
            Vector3 _spawnPosition = _shootStartPoint.position + transform.forward;
            GameObject ball = Instantiate(_bballPrefab, _spawnPosition, transform.rotation);
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            Vector3 finalThrowDirection = (transform.forward + _throwDirection).normalized;
            rb.AddForce(finalThrowDirection * force, ForceMode.VelocityChange);
        }
    }

}
