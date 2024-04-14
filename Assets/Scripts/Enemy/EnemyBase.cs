using System;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamagable
    {
        public GameObject deathEffect;

        private int _health = 4;
        //private int _armor;
        private int _damage = 1;
        public float _maxAttackRange { get; private set; } = 2;
        public float _viewDistance { get; private set; } = 10;
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
            StartCoroutine(DeathEffectActivate());
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

        private IEnumerator DeathEffectActivate()
        {
            deathEffect.GetComponent<ParticleSystem>().Play();
            gameObject.transform.localScale = Vector3.zero;
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }
}
