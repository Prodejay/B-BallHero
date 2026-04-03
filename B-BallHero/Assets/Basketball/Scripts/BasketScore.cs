using UnityEngine;

namespace BBallHero.Gameplay
{
    public class BasketScore : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag != "Basketball")
                return;

            Vector3 myCenter = GetComponent<Collider>().bounds.center;
            Vector3 otherCenter = other.bounds.center;
            Vector3 direction = (otherCenter - myCenter).normalized;
            float dot = Vector3.Dot(direction, Vector3.up);

            if (dot > 0.5f)
            {
                GameManager.instance.AddScore(1);
            }
        }
    }
}

