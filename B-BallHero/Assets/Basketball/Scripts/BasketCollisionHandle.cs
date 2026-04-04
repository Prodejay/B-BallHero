using Unity.Cinemachine;
using UnityEngine;

namespace BBallHero.Gameplay
{
    public class BasketCollisionHandle : MonoBehaviour
    {
        [SerializeField]
        private CinemachineImpulseSource _impulse;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag != "Basketball")
                return;

            _impulse.GenerateImpulse();
        }
    }
}

