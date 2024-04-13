using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Patrolling : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Transform[] _waypoints;
        private float _distanceToWaypoint = 1f;
        private int _currentWaypointIndex = 0;
        public bool _isPatrolling = true;

        private void Start()
        {
            _waypoints = GameObject.Find("Waypoints").GetComponentsInChildren<Transform>();
            _agent = GetComponent<NavMeshAgent>();
            _agent.SetDestination(_waypoints[_currentWaypointIndex].position);
        }

        private void ChangeWaypoint()
        {
            if(_agent.remainingDistance <= _distanceToWaypoint)
            {
                _currentWaypointIndex++;
                if (_currentWaypointIndex >= _waypoints.Length)
                {
                    _currentWaypointIndex = 0;
                }
                _agent.SetDestination(_waypoints[_currentWaypointIndex].position);
            }
        }
        private void Update()
        {
            if(!_isPatrolling) return;
            ChangeWaypoint();
        }
    }
}