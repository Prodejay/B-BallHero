using UnityEngine;

namespace BBallHero.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        [SerializeField]
        private int _score;
        public int score => _score;

        [SerializeField]
        private GameObject _freelookCamera;
        [SerializeField]
        private GameObject _thirdPersonCamera;

        public void AddScore(int score)
        {
            _score += score;
        }

        public void SetFreeLookCameraOn()
        {
            _freelookCamera.SetActive(true);
            _thirdPersonCamera.SetActive(false);
        }

        public void SetThirdPersonCameraOn() 
        {
            _thirdPersonCamera.SetActive(true);
            _freelookCamera.SetActive(false);
        }
    }
}

