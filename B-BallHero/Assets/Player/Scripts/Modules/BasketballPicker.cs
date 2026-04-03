using System;
using UnityEngine;

namespace BBallHero.Gameplay.Player
{
    public class BasketballPicker : MonoBehaviour
    {
        [SerializeField]
        private Player _player;

        public event Action PickedUpBasketball;


        private void OnTriggerEnter(Collider other)
        {
            if ((other.transform.tag == "Basketball") == false)
                return;

            if (_player.hasBasketball == true)
                return;

            PickedUpBasketball?.Invoke();
            other.gameObject.SetActive(false); //replace with return to pool later
        }
    }
}

