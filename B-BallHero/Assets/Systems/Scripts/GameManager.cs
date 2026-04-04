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
        [SerializeField]
        private GameObject _pausePanel;
        [SerializeField]
        private GameObject _gameOverPanel;

        private bool _isGamePaused = false;
        public bool isGamePaused => _isGamePaused;

        private void Start()
        {
            UnPauseGame();
        }

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

        public void PauseGame()
        {
            _isGamePaused = true;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _pausePanel.SetActive(true);
        }

        public void UnPauseGame()
        {
            _isGamePaused = false;
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _pausePanel.SetActive(false);
        }

        public void GameOver()
        {
            _isGamePaused = true;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _gameOverPanel.SetActive(true);
        }
    }
}

