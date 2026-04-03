using UnityEngine;

namespace BBallHero.Gameplay.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private bool _hasBasketball;
        public bool hasBasketball => _hasBasketball;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_hasBasketball == false)
            {
                _hasBasketball = true;
            }
        }
    }

}
