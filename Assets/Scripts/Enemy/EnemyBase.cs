using UnityEngine;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        private int _health;
        //private int _armor;
        private int _damage;
        public  float _maxAttackRange {get; private set;}
        [HideInInspector] public float _speed {get; private set;}
        private AudioClip _response1;
        
        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health > 0) return;
            _health = 0;
            Death();
        }

        public void Attack()
        {
            //_player.TakeDamage(_damage);
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
