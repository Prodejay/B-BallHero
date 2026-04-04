using BBallHero.Gameplay.Player;
using BBallHero.Gameplay.Sound;
using System;
using UnityEngine;

namespace BBallHero.Gameplay.Player
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        public event Action ShootAnimationEnd;
        public event Action BasketballReleased;
        public void BasketballThrown()
        {
            BasketballReleased?.Invoke();           
            SoundManager.instance.PlaySoundEffect(SoundType.THROWWOOSH);
        }

        public void BasketBounce()
        {
            SoundManager.instance.PlaySoundEffect(SoundType.DRIBBLE);
        }

        public void StepTaken()
        {
            SoundManager.instance.PlaySoundEffect(SoundType.ARMORCLANK, 0.3f);
        }

        public void ShootEnd()
        {
            ShootAnimationEnd?.Invoke();
        }
    }
}

