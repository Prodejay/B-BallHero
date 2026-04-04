using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        [SerializeField]
        private TextMeshProUGUI _scoreText;

        public void AddScore(int score)
        {
            _score += score;
            _scoreText.SetText($"Score: {_score}");
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

        public void ReturnToTitleScreen()
        {
            SceneManager.LoadScene("Scene_Title");
        }

        public void LoadGameplayScene()
        {
            SceneManager.LoadScene("Scene_Gameplay");
        }
    }
}

