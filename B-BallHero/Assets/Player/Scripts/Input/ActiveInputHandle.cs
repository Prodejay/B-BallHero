using UnityEngine;

namespace BBallHero.Gameplay.Player.Input
{
    public class ActiveInputHandle : MonoBehaviour
    {
        [SerializeField]
        private InputReader _inputReader;

        private void OnEnable()
        {
            _inputReader.SetInputToGameplay();
        }

        private void OnDisable()
        {
            _inputReader.SetInputToUI();
        }
    }
}

