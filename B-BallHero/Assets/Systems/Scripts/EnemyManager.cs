using BBallHero.Gameplay.Sound;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BBallHero.Gameplay.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private int _currentWave = 1;
        [SerializeField]
        private int _currentNumEnemiesInWave = 1;
        [SerializeField]
        private int _maxEnemies = 5;
        [SerializeField]
        private float _spawnDelay = 0.2f;
        [SerializeField]
        private float _textDuration = 2f;

        [SerializeField]
        private Transform[] _spawnAreas;
        [SerializeField]
        private EnemyAI _enemyPrefab;
        [SerializeField]
        private TextMeshProUGUI _waveStartText;
        [SerializeField]
        private TextMeshProUGUI _waveEndText;
        [SerializeField]
        private Transform _playerPosition;

        private int _enemiesKilled = 0;

        public event Action WaveStart;
        public event Action WaveEnd;

        private void Start()
        {
            StartCoroutine(SpawnWaveRoutine(_currentWave));
        }

        private void OnEnable()
        {
            WaveStart += OnWaveStart;
            WaveEnd += OnWaveEnd;
        }

        private void OnDisable()
        {
            WaveStart -= OnWaveStart;
            WaveEnd -= OnWaveEnd;
        }

        private void OnWaveEnd()
        {
            StartCoroutine(WaveEndUIRoutine());
        }

        private void OnWaveStart()
        {
            _currentWave++;
            StartCoroutine(SpawnWaveRoutine(_currentWave));
        }

        private IEnumerator SpawnWaveRoutine(int currentWave)
        {
            _enemiesKilled = 0;

            yield return WaveStartUIRoutine();

            //if current wave is a prime number, increase enemies in wave
            if(_currentNumEnemiesInWave < _maxEnemies)
            {
                if (currentWave % 2 == 0)
                {
                    _currentNumEnemiesInWave++;
                }
            }

            List<Transform> spawnLocations = new List<Transform>();
            for(int i = 0; i < _currentNumEnemiesInWave; i++)
            {
                //Get random spawn locations
                int randomIndex = UnityEngine.Random.Range(0, _spawnAreas.Length);

                if (spawnLocations.Contains(_spawnAreas[randomIndex]) == false)
                {
                    spawnLocations.Add(_spawnAreas[randomIndex]);
                }
            }

            //Spawn enemies in spawn location
            for(int i = 0; i < _currentNumEnemiesInWave; i++)
            {
                SpawnEnemy(spawnLocations[i]);
                yield return new WaitForSeconds(_spawnDelay);
            }
        }

        private void SpawnEnemy(Transform spawnLocation)
        {
            SoundManager.instance.PlaySoundEffect(SoundType.TELEPORT);
            GameObject enemy = Instantiate(_enemyPrefab.gameObject, spawnLocation.position, Quaternion.identity);
            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
            enemyAI.Killed += OnEnemyKilled;
            enemyAI.SetTarget(_playerPosition);
        }

        private void OnEnemyKilled(EnemyAI enemy)
        {
            _enemiesKilled++;
            //check if enemies killed equals to current number spawned
            if(_enemiesKilled >= _currentNumEnemiesInWave)
            {
                WaveEnd?.Invoke();                
            }
            //if so spawn next wave
            enemy.Killed -= OnEnemyKilled;
        }

        private IEnumerator WaveStartUIRoutine()
        {
            _waveStartText.text = $"WAVE {_currentWave}";
            _waveStartText.enabled = true;
            SoundManager.instance.PlaySoundEffect(SoundType.MLGHORN, 0.3f);
            yield return new WaitForSeconds(_textDuration);
            _waveStartText.enabled = false;
        }

        private IEnumerator WaveEndUIRoutine()
        {
            _waveEndText.enabled = true;
            yield return new WaitForSeconds(_textDuration);
            _waveEndText.enabled = false;
            WaveStart?.Invoke();
        }
    }
}

