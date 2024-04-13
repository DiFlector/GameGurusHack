using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        private int _health;
        //private int _armor;
        private int _damage = 1;
        public float _maxAttackRange { get; private set; } = 10;
        public float _viewDistance { get; private set; } = 20;
        [HideInInspector] public float _speed {get; private set;}
        private AudioClip _response1;
        [SerializeField] private float _attackTimer;
        
        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health > 0) return;
            _health = 0;
            Death();
        }

        private void Update()
        {
            _attackTimer += Time.deltaTime;
        }

        public void Attack()
        {
            if (_attackTimer < 2) return;
            Player.Instance.ChangeHP(_damage);
            _attackTimer = 0;
            print("Attack");
        }
        
        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        private void Death()
        {
            Destroy(gameObject);
        }

        public void SetResponse()
        {
            //gameObject.GetComponent(AudioClip) = _response1;
        }

        public void PlayResponse()
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
            
    }
}
