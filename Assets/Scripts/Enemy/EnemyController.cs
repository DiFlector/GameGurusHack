using System;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyStatesEnum _currentState;
        [SerializeField] private Transform _moveTarget;
        private Patrolling _patrolling;
        private EnemyBase _enemyBase;
        private NavMeshAgent _agent;
        private Transform _playerPosition;
        private EnemyController _enemyController;

        private void Start()
        {
            _patrolling = GetComponent<Patrolling>();
            _enemyBase = GetComponent<EnemyBase>();
            _agent = GetComponent<NavMeshAgent>();
            _currentState = EnemyStatesEnum.Patrolling;
            TryGetPlayerPosition();
        }

        private void Update()
        {
            if (_currentState == EnemyStatesEnum.Attacking)
            {
                _enemyBase.Attack();
            }
            Navigate();
            StateSwitcher();
        }

        private bool TryGetPlayerPosition()
        {
            if(!Player.Instance) return false;
            if (ToPlayerDistance() < _enemyBase._viewDistance)
            {
                _playerPosition = Player.Instance.transform;
                return true;
            }

            return false;
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
            switch (_currentState)
            {
                case EnemyStatesEnum.Idle:
                    _agent.speed = 0f;
                    _agent.isStopped = true;
                    _patrolling._isPatrolling = false;
                    break;
                case EnemyStatesEnum.Patrolling:
                    _agent.speed = 3f;
                    _agent.isStopped = false;
                    _patrolling._isPatrolling = true;
                    break;
                case EnemyStatesEnum.Following:
                    _agent.speed = 5f;
                    _agent.isStopped = false;
                    _patrolling._isPatrolling = false;
                    _moveTarget = _playerPosition;
                    break;
                case EnemyStatesEnum.Attacking:
                    _agent.speed = 0f;
                    _agent.isStopped = true;
                    _patrolling._isPatrolling = false;
                    _moveTarget = _playerPosition;
                    break;
            }
        }

        private void StateSwitcher()
        {
            //if(!_moveTarget) return;
            if (_agent.remainingDistance <= _enemyBase._maxAttackRange && TryGetPlayerPosition() && _currentState != EnemyStatesEnum.Attacking)
            {
                _currentState = EnemyStatesEnum.Attacking;
                OnStateChange?.Invoke();
            }
            else if(_agent.remainingDistance > _enemyBase._maxAttackRange && TryGetPlayerPosition() && _currentState != EnemyStatesEnum.Following)
            {
                _currentState = EnemyStatesEnum.Following;
                OnStateChange?.Invoke();
            }
            else if(!TryGetPlayerPosition() && _currentState != EnemyStatesEnum.Patrolling)
            {
                _currentState = EnemyStatesEnum.Patrolling;
                OnStateChange?.Invoke();
            }
        }

        private float ToPlayerDistance()
        {
            return Vector3.Distance(transform.position, Player.Instance.transform.position);
        }
    }
}