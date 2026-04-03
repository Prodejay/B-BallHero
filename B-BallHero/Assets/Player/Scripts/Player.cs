using System;
using UnityEngine;

namespace BBallHero.Gameplay.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private BasketballPicker _basketballPicker;
        [SerializeField]
        private ShootModule _shootModule;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private bool _hasBasketball;
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
        }

        private void OnDisable()
        {
            _basketballPicker.PickedUpBasketball -= OnPickedUpBasketball;
            _shootModule.ThrewBasketball -= OnThrewBasketball;
        }

        private void OnThrewBasketball()
        {
            _hasBasketball = false;
            _animator.SetTrigger("Shoot");
            _animator.SetBool("HasBall", false);
        }

        private void OnPickedUpBasketball()
        {
            _hasBasketball = true;
            _animator.SetBool("HasBall", true);
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_rb.linearVelocity.magnitude > 0.1f)
                _animator.SetBool("IsMoving", true);
            else
                _animator.SetBool("IsMoving", false);
        }
    }

}
