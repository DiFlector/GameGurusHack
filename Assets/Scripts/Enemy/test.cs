using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy
{
    public class test : MonoBehaviour
    {
        private EnemyController _enemyController;

        private void Start()
        {
            _enemyController = FindObjectOfType<EnemyController>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _enemyController.OnStateChange?.Invoke();
            }
        }
    }
}