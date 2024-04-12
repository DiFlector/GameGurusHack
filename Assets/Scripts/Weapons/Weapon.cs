using System.Collections;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    public static UnityEvent<int, int> OnAmmoChanged = new();
    public static UnityEvent<WeaponData> OnWeaponActive = new();
    public static UnityEvent<float> OnReload = new();

    public WeaponData WeaponData;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private float _bulletSpeed = 150;
    protected int _maxAmmoIn { get; private set; }
    protected bool _isReloading;
    protected int _ammoIn { get; private set; }
    protected int _allAmmo { get; private set; }

    protected virtual void Awake()
    {
        _maxAmmoIn = WeaponData.MaxAmmoIn;
        _ammoIn = WeaponData.MaxAmmoIn;
        _allAmmo = WeaponData.AllAmmo;
    }

    private void OnEnable()
    {
        OnWeaponActive.Invoke(WeaponData);
    }

    public virtual bool TryShoot()
    {
        if (!_isReloading)
        {
            //Shot mechanics
            if (_ammoIn > 0)
            {
                Debug.Log("Shot!");
                GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawn);
                Debug.Log(bullet);
                bullet.GetComponent<Rigidbody>().AddForce(_bulletSpawn.transform.forward * _bulletSpeed, ForceMode.Impulse);
                StartCoroutine(BulletLifetime(bullet));
                _ammoIn--;
                OnAmmoChanged.Invoke(_ammoIn, _allAmmo);
                return true;
            }
            else
                Debug.Log("NO AMMO!");
        }
        return false;
    }

    private IEnumerator BulletLifetime(GameObject bullet)
    {
        yield return new WaitForSeconds(30);
        Destroy(bullet);
    }

    public void UpdateWeaponInfo()
    {
        OnWeaponActive.Invoke(WeaponData);
    }

    public virtual bool TryToReload()
    {
        if (_ammoIn < _maxAmmoIn && _allAmmo > 0 && !_isReloading)
        {
            _isReloading = true;
            StartCoroutine(ReloadProcess(4));
            return true;
        }
        else
            return false;
    }

    protected IEnumerator ReloadProcess(int delay)
    {
        //Play some animation
        Debug.Log("RELOADING!");
        OnReload.Invoke(delay);
        yield return new WaitForSeconds(delay);
        if (_allAmmo >= (_maxAmmoIn - _ammoIn))
        {
            _allAmmo -= _maxAmmoIn - _ammoIn;
            _ammoIn = _maxAmmoIn;
        }
        else
        {
            _ammoIn += _allAmmo;
            _allAmmo = 0;
        }
        OnAmmoChanged.Invoke(_ammoIn, _allAmmo);
        _isReloading = false;
    }

}
