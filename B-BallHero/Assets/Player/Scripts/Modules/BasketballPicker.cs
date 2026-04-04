using System;
using UnityEngine;

namespace BBallHero.Gameplay.Player
{
    public class BasketballPicker : MonoBehaviour
    {
        [SerializeField]
        private Player _player;

        private Collider _triggerCollider;

        public event Action PickedUpBasketball;

        private void Awake()
        {
            _triggerCollider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((other.transform.tag == "Basketball") == false)
                return;

            if (_player.hasBasketball == true)
                return;

            PickedUpBasketball?.Invoke();
            Destroy(other.gameObject);
        }

        public void DisableCollider()
        {
            _triggerCollider.enabled = false;
        }

        public void EnableCollider()
        {
            _triggerCollider.enabled = true;
        }
    }
}

