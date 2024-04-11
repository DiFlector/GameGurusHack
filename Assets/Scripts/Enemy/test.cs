using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy
{
    public class test : MonoBehaviour
    {
        private EnemyStates _enemyStates;

        private void Start()
        {
            _enemyStates = FindObjectOfType<EnemyStates>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _enemyStates.OnStateChange?.Invoke();
            }
        }
    }
}