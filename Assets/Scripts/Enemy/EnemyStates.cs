using System;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyStates : MonoBehaviour
    {
        [SerializeField] private EnemyStatesEnum _currentState;
        [SerializeField] private Transform _moveTarget;
        private EnemyBase _enemyBase;
        private NavMeshAgent _agent;
        //private Player _player;
        private Transform _playerPosition;
        private EnemyStates _enemyStates;

        private void Start()
        {
            _currentState = EnemyStatesEnum.Idle;
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            Navigate();
        }

        private void GetPlayerPosition()
        {
            //_playerPosition = _player.transform.position;
        }

        private void Navigate()
        {
            if (!_moveTarget) return;
            _agent.destination = _moveTarget.position;
            
        }

        private void Awake()
        {
            OnStateChange.AddListener(States);
        }
        
        public UnityEvent OnStateChange;
        

        private void States()
        {
            print("it's me Mario!");
            switch (_currentState)
            {
                case EnemyStatesEnum.Idle:
                    _agent.speed = 0f;
                    _agent.isStopped = true;
                    break;
                case EnemyStatesEnum.Seekeing:
                    _agent.speed = 0f;
                    _agent.isStopped = true;
                    break;
                case EnemyStatesEnum.Following:
                    _agent.speed = 5f;
                    break;
                case EnemyStatesEnum.Attacking:
                    _agent.speed = 0f;
                    _agent.isStopped = true;
                    break;
                case EnemyStatesEnum.Crouching:
                    _agent.speed = 0f;
                    _agent.isStopped = true;
                    break;
            }
        }

        private void StateSwitcher()
        {
            if (_agent.remainingDistance < _enemyBase._maxAttackRange)
            {
                _currentState = EnemyStatesEnum.Attacking;
                OnStateChange?.Invoke();
            }
            else
            {
                _currentState = EnemyStatesEnum.Following;
                OnStateChange?.Invoke();
            }
        }
    }
}