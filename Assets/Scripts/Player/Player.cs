using UnityEngine.Events;
using UnityEngine;
using System.Collections.Generic;

public class Player : Singleton<Player>
{
    public static UnityEvent<int> OnHealthChanged = new();
    public static UnityEvent OnShot = new();
    public int HPAmount { get; private set; } = 5;
    public Weapon _currentWeapon;
    [SerializeField] private List<Weapon> _weapons;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Shoot()
    {
        _currentWeapon.Shoot();
        OnShot.Invoke();
    }

    public void Reload()
    {
        _currentWeapon.TryToReload();
    }
    





    /// <summary>
    /// Adds value to HP
    /// </summary>
    public void ChangeHP(int points)
    {
        if (HPAmount + points <= 0)
        {
            Death();
            return;
        } 
        HPAmount += points;
        OnHealthChanged.Invoke(HPAmount);
    }

    private void Death()
    {
        Debug.LogAssertion("ВЫ оподливились =(");
    }
}
