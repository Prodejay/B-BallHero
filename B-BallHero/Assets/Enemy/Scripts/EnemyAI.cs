using System;
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
            _animator.SetTrigger("Death");
        }

        // Update is called once per frame
        void Update()
        {
            _agent.SetDestination(_target.position);
        }
    }
}

