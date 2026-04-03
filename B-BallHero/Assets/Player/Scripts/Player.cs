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
        }

        private void OnPickedUpBasketball()
        {
            _hasBasketball = true;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
