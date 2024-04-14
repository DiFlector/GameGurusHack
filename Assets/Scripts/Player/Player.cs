using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Singleton<Player>
{
    public static UnityEvent<int> OnHealthChanged = new();
    public static UnityEvent<int> OnArmorChanged = new();
    public static UnityEvent<bool> OnShot = new();
    private bool isSpraying;
    private bool canShoot = true;
    private bool isSwapping;

    public int HPAmount { get; private set; } = 5;
    public int ArmorAmount { get; private set; } = 0;
    [SerializeField] private float _armorRestoreDelay;
    public Weapon _currentWeapon;
    [SerializeField] private Pistol _pistol;
    [SerializeField] private Rifle _rifle;
    [SerializeField] private Shotgun _shotgun;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private MainView _mainView;

    protected override void Awake()
    {
        base.Awake();
        CheckArmorForRestore();
    }

    private void OnEnable()
    {
        CircleMenu.OnWeaponSwap.AddListener(SwapWeapon);
        CircleMenu.OnSelectorOpened.AddListener(ShootCheck);
    }

    private void OnDisable()
    {
        CircleMenu.OnWeaponSwap.RemoveListener(SwapWeapon);
        CircleMenu.OnSelectorOpened.RemoveListener(ShootCheck);
    }

    private void ShootCheck(bool state)
    {
        canShoot = !state;
    }

    public void Shoot(bool state)
    {
        if (canShoot)
        {
            if (_currentWeapon.WeaponData.FireType == FireType.Spray)
            {
                if (!isSpraying && state)
                {
                    isSpraying = true;
                    StartCoroutine("SprayProcess");
                }
                if (!state)
                {
                    isSpraying = false;
                    StopCoroutine("SprayProcess");
                }
            }
            else if (state)
            {
                if (_currentWeapon.TryToShoot())
                {
                    StartCoroutine(Recoil());
                    OnShot.Invoke(_currentWeapon.WeaponData.WideScope);
                }
            }
        }
    }

    private IEnumerator SprayProcess()
    {
        while (isSpraying)
        {
            if (_currentWeapon.TryToShoot())
            {
                OnShot.Invoke(_currentWeapon.WeaponData.WideScope);
                StartCoroutine(Recoil());
            }
            yield return new WaitForSeconds(_currentWeapon.WeaponData.FireRate);
        }
    }

    public void OpenSelection()
    {
        _mainView.ToggleCircleMenu();
    }

    private IEnumerator Recoil()
    {
        _camera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value -= 2;
        float duration = 1;
        float timeElapsed = 0;
        while (timeElapsed / duration < 1)
        {
            _camera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value += Mathf.Lerp(0, 0.1f, timeElapsed / duration);
            timeElapsed += Time.deltaTime * 10;
            yield return null;
        }
    }

    public void Reload()
    {
        _currentWeapon.TryToReload();
    }

    private void SwapWeapon(WeaponType type)
    {
        StartCoroutine(SwapWeaponProcess(type));
    }
    
    private IEnumerator SwapWeaponProcess(WeaponType type)
    {
        if (!isSwapping)
        {
            isSwapping = true;
            _currentWeapon.SwapDown();
            yield return new WaitForSeconds(0.4f);
            _currentWeapon.Toggle(false);
            switch (type)
            {
                case WeaponType.Pistol:
                    _currentWeapon = _pistol;
                    break;
                case WeaponType.Rifle:
                    _currentWeapon = _rifle;
                    break;
                case WeaponType.Shotgun:
                    _currentWeapon = _shotgun;
                    break;
            }
            _currentWeapon.Toggle(true);
            _currentWeapon.UpdateWeaponInfo();
            _currentWeapon.GetBulletInfo();
            _currentWeapon.SwapUp();
            yield return new WaitForSeconds(0.4f);
            _currentWeapon.Idle();
            isSwapping = false;
        }
    }

    public void ApplyDamage()
    {
        if (ArmorAmount > 0)
            ChangeArmor(ArmorAmount - 1);
        else
            ChangeHP(HPAmount - 1);
    }

    /// <summary>
    /// Adds value to HP
    /// </summary>
    private void ChangeHP(int points)
    {
        if (HPAmount + points <= 0)
        {
            HPAmount = 0;
            Death();
            return;
        }
        else if (HPAmount + points > 5)
            HPAmount = 5;
        else
            HPAmount += points;
        OnHealthChanged.Invoke(HPAmount);
    }

    /// <summary>
    /// Adds value to Armor
    /// </summary>
    private void ChangeArmor(int points)
    {
        if (ArmorAmount + points > 5)
            ArmorAmount = 5;
        else if (ArmorAmount + points < 0)
            return;
        else
            ArmorAmount += points;
        OnArmorChanged.Invoke(ArmorAmount);
        CheckArmorForRestore();
    }
    private void CheckArmorForRestore()
    {
        if (ArmorAmount < 5)
        {
            StartCoroutine(RestoreArmor());
            Debug.Log("REST");
        }
    }

    private IEnumerator RestoreArmor()
    {
        yield return new WaitForSeconds(_armorRestoreDelay);
        ChangeArmor(1);
    }

    private void Death()
    {
        Debug.LogAssertion("ВЫ оподливились =(");
    }
}
