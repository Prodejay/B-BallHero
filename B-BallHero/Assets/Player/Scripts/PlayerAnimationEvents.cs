using BBallHero.Gameplay.Player;
using BBallHero.Gameplay.Sound;
using System;
using UnityEngine;

namespace BBallHero.Gameplay.Player
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        public event Action BasketballReleased;
        public void BasketballThrown()
        {
            BasketballReleased?.Invoke();
        }

        public void BasketBounce()
        {
            SoundManager.instance.PlaySoundEffect(SoundType.DRIBBLE);
        }
    }
}

