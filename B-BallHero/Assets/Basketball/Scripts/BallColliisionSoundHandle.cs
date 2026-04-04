using UnityEngine;

namespace BBallHero.Gameplay.Sound
{
    public class BallColliisionSoundHandle : MonoBehaviour
    {
        [SerializeField]
        private SoundType soundType;
        [SerializeField]
        private float volume = 1f;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag != "Basketball")
                return;

            SoundManager.instance.PlaySoundEffect(soundType, volume);
        }
    }
}

