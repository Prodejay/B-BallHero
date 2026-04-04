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
        private float _deathDelay = 2f;

        private void OnEnable()
        {
            _basketScore.Scored += OnScore;
        }

        private void OnDisable()
        {
            _basketScore.Scored -= OnScore;
        }

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
            Destroy(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            _agent.SetDestination(_target.position);
        }
    }
}

