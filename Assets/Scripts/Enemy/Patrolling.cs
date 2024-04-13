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

        public Transform FindClosestWaypoint(Transform _currentGoal)
        {
            if (_currentGoal != Player.Instance.transform) return Player.Instance.transform;
            var min = Vector3.Distance(_waypoints[0].position, transform.position);
            Transform closestPoint = _waypoints[0];
            foreach (var point in _waypoints)
            {
                if (Vector3.Distance(point.position, transform.position) < min)
                {
                    min = Vector3.Distance(point.position, transform.position);
                    closestPoint = point;
                }
            }
            return closestPoint;
        }
        private void Update()
        {
            if(!_isPatrolling) return;
            ChangeWaypoint();
        }
    }
}