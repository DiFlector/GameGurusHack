using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private float _spread;
    public override bool TryToShoot()
    {
        if (!_isReloading)
        {
            if (_ammoIn > 0)
            {
                Debug.Log("Shot!");
                _particles1.Play();
                _particles2.Play();
                for (int i = 0; i < 10; i++)
                {
                    GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawn);
                    bullet.transform.parent = transform.root;
                    Vector3 dir = new Vector3(Random.Range(-_spread, _spread), Random.Range(-_spread, _spread), Random.Range(-_spread, _spread));
                    bullet.GetComponent<Rigidbody>().AddForce((_bulletSpawn.transform.forward + dir) * _bulletSpeed, ForceMode.Impulse);
                    StartCoroutine(BulletLifetime(bullet));
                }
                _ammoIn--;
                OnAmmoChanged.Invoke(_ammoIn, _allAmmo);
                return true;
            }
            else
                Debug.Log("NO AMMO!");
        }
        return false;

    }
}
