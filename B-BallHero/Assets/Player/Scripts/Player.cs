using BBallHero.Gameplay.Player.Input;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace BBallHero.Gameplay.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private BasketballPicker _basketballPicker;
        [SerializeField]
        private PlayerController _playerController;
        [SerializeField]
        private ShootModule _shootModule;
        [SerializeField]
        private PlayerAnimationEvents _playerAnimationEvents;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private bool _hasBasketball;
        [SerializeField]
        private GameObject _animatedBall;

        public bool hasBasketball
        {
            get
            {
                return _hasBasketball;
            }
            set
            {
                _hasBasketball = value;
            }
        }

        private Rigidbody _rb;

        private void OnEnable()
        {
            _basketballPicker.PickedUpBasketball += OnPickedUpBasketball;
            _shootModule.ThrewBasketball += OnThrewBasketball;
            _playerAnimationEvents.ShootAnimationEnd += OnShootAnimationEnd;
            _playerAnimationEvents.BasketballReleased += OnBasketballReleased;
            _playerController.MovementStarted += OnMovementStarted;
            _playerController.MovementStopped += OnMovementStopped;
        }

        private void OnDisable()
        {
            _basketballPicker.PickedUpBasketball -= OnPickedUpBasketball;
            _shootModule.ThrewBasketball -= OnThrewBasketball;
            _playerAnimationEvents.ShootAnimationEnd -= OnShootAnimationEnd;
            _playerAnimationEvents.BasketballReleased += OnBasketballReleased;
            _playerController.MovementStarted -= OnMovementStarted;
            _playerController.MovementStopped -= OnMovementStopped;
        }

        private void OnBasketballReleased()
        {
            _animatedBall.SetActive(false);
            _shootModule.ReleaseBall();
        }

        private void OnMovementStopped()
        {
            
        }

        private void OnMovementStarted()
        {
            
        }

        private void OnShootAnimationEnd()
        {

        }

        private void OnThrewBasketball()
        {
            _animator.SetBool("IsShooting", true);
        }

        private void OnPickedUpBasketball()
        {
            _hasBasketball = true;
            _animatedBall.SetActive(true);
            _animator.SetBool("HasBall", true);
            _basketballPicker.DisableCollider();
        }

        public void ShootExitBehaviour()
        {
            _hasBasketball = false;
            _basketballPicker.EnableCollider();
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _animatedBall.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (_rb.linearVelocity.magnitude > 0.1f)
            {
                _animator.SetBool("IsMoving", true);
            }
            else
            {
                _animator.SetBool("IsMoving", false);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.tag == "Enemy")
            {
                GameManager.instance.GameOver();
            }
        }
    }

}
