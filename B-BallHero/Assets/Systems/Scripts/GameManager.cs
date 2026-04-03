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

        public void AddScore(int score)
        {
            _score += score;
        }
    }
}

