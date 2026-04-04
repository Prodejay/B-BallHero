using BBallHero.Gameplay.Player;
using System;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public event Action BasketballReleased;
    public void BasketballThrown()
    {
        BasketballReleased?.Invoke();
    }
}
