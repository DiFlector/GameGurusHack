using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WeaponAnimationController))]
public abstract class Weapon : MonoBehaviour
{
    public static UnityEvent<int, int> OnAmmoChanged = new();
    public static UnityEvent<WeaponData> OnWeaponActive = new();
    public static UnityEvent<float> OnReload = new();

    public WeaponData WeaponData;
    [SerializeField] protected GameObject _bulletPrefab;
    [SerializeField] protected Transform _bulletSpawn;
    [SerializeField] protected float _bulletSpeed = 150;
    [SerializeField] protected float _bulletLifetime = 30;
    [SerializeField] protected ParticleSystem _particles1;
    [SerializeField] protected ParticleSystem _particles2;
    protected WeaponAnimationController _weaponAnimator;


    protected int _maxAmmoIn { get; set; }
    protected bool _isReloading;
    protected int _ammoIn { get; set; }
    protected int _allAmmo { get; set; }

    protected virtual void Awake()
    {
        _maxAmmoIn = WeaponData.MaxAmmoIn;
        _ammoIn = WeaponData.MaxAmmoIn;
        _allAmmo = WeaponData.AllAmmo;
        _weaponAnimator = GetComponent<WeaponAnimationController>();
    }

    private void OnEnable()
    {
        OnWeaponActive.Invoke(WeaponData);
    }

    public virtual bool TryToShoot()
    {
        if (!_isReloading)
        {
            if (_ammoIn > 0)
            {
                Debug.Log("Shot!");
                GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawn);
                Debug.Log(bullet);
                _particles1.Play();
                _particles2.Play();
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

    protected IEnumerator BulletLifetime(GameObject bullet)
    {
        yield return new WaitForSeconds(30);
        Destroy(bullet);
    }

    public void UpdateWeaponInfo()
    {
        OnWeaponActive.Invoke(WeaponData);
        Debug.Log("updated");
    }

    public void GetBulletInfo()
    {
        OnAmmoChanged.Invoke(_ammoIn, _allAmmo);
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
        _weaponAnimator.Animate(AnimType.Reload);
        OnReload.Invoke(delay);
        yield return new WaitForSeconds(delay);
        _weaponAnimator.Animate(AnimType.Idle);
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
