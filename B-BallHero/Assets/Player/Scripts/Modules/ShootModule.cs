using BBallHero.Gameplay.Sound;
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

        private float _chargeTime = 0;
        private float _currentForce;
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
            _currentForce = Mathf.Min(_chargeTime * _throwForce, _maxForce);
            //trajectory line velocity
            Vector3 ballVelocity = (transform.forward + _throwDirection).normalized * _currentForce;
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
            //Invoke event to play animation
            _isCharging = false;
            _trajectoryLine.enabled = false;
            if(_currentForce >= _maxForce)
            {
                SoundManager.instance.PlaySoundEffect(SoundType.KOBE, 0.3f);
            }
            ThrewBasketball?.Invoke();
        }

        public void CancelThrow()
        {
            _isCharging = false;
            _trajectoryLine.enabled = false;
        }

        //called on time for when animation event for throw ball runs
        public void ReleaseBall()
        {
            ThrowBasketball(Mathf.Min(_chargeTime * _throwForce, _maxForce));
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
