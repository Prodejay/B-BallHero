using UnityEngine;

namespace BBallHero.Gameplay
{
    public class BasketScore : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.transform.tag == "Basketball")
            {
                //call game manager to add score
            }
        }
    }
}

