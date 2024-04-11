using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    public static UnityEvent<int, int> OnAmmoChanged = new();
    public static UnityEvent<WeaponData> OnWeaponActive = new();

    [SerializeField] private WeaponData _weaponData;
    protected int _maxAmmoIn { get; private set; }
    protected bool _isReloading;
    protected int _ammoIn { get; private set; }
    protected int _allAmmo { get; private set; }

    protected virtual void Awake()
    {
        _maxAmmoIn = _weaponData.MaxAmmoIn;
        _ammoIn = _weaponData.MaxAmmoIn;
        _allAmmo = _weaponData.AllAmmo;
    }

    private void OnEnable()
    {
        OnWeaponActive.Invoke(_weaponData);
    }

    public virtual void Shoot()
    {
        if (!_isReloading)
        {
            //Shot mechanics
            if (_ammoIn > 0)
            {
                Debug.Log("Shot!");
                _ammoIn--;
                OnAmmoChanged.Invoke(_ammoIn, _allAmmo);
            }
            else
                Debug.Log("NO AMMO!");
        }
    }

    public void UpdateWeaponInfo()
    {
        OnWeaponActive.Invoke(_weaponData);
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
