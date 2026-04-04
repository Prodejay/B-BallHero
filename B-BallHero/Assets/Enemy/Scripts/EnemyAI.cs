using BBallHero.Gameplay.Sound;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace BBallHero.Gameplay.Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private NavMeshAgent _agent;
        [SerializeField]
        private Transform _target;
        [SerializeField]
        private BasketScore _basketScore;
        [SerializeField]
        private ParticleSystem _explosionVFX;
        [SerializeField]
        private ParticleSystem _spawnInVFX;

        [SerializeField]
        private float _deathDelay = 2f;

        public event Action<EnemyAI> Killed;

        private void OnEnable()
        {
            _basketScore.Scored += OnScore;

            _spawnInVFX.Play();
        }

        private void OnDisable()
        {
            _basketScore.Scored -= OnScore;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        [ContextMenu("Kill Me")]
        private void OnScore()
        {
            _explosionVFX.Play();
            _animator.SetTrigger("Death");
            _agent.isStopped = true;
            SoundManager.instance.PlaySoundEffect(SoundType.EXPLOSION, 0.5f);
            StartCoroutine(DeathRoutine());
        }

        private IEnumerator DeathRoutine()
        {
            yield return new WaitForSeconds(_deathDelay);
            Killed?.Invoke(this);
            Destroy(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            _agent.SetDestination(_target.position);
        }
    }
}

