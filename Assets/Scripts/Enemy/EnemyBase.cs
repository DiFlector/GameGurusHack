using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamagable
    {
        private int _health = 4;
        //private int _armor;
        private int _damage = 1;
        public float _maxAttackRange { get; private set; } = 10;
        public float _viewDistance { get; private set; } = 20;
        [HideInInspector] public float Speed {get; private set;}
        private AudioClip _response1;
        [SerializeField] private float _attackTimer;
        
        private void Update()
        {
            _attackTimer += Time.deltaTime;
        }

        public void Attack()
        {
            if (_attackTimer < 2) return;
            Player.Instance.ApplyDamage();
            _attackTimer = 0;
            print("Attack");
        }
        
        public void SetSpeed(float speed)
        {
            Speed = speed;
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

        public void ApplyDamage()
        {
            _health--;
            if (_health > 0) return;
            _health = 0;
            Death();
        }
    }
}
