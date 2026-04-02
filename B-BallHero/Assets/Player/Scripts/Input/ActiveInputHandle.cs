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
            //Temporarily placed here to deal with stack overflow warning error
            _inputReader.SetInputToUI();
        }
    }
}

