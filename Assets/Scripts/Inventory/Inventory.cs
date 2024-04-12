using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action<IInventoryWeapon> OnWeaponAdded;
    private Dictionary<WeaponType, IInventoryWeapon> _weapons = new();
    
    public void AddWeapon(IInventoryWeapon weapon)
    {
        WeaponType type = weapon.Type;
        
        if (_weapons.ContainsKey(type))
        {
            _weapons[type].Ammo += weapon.Ammo;
            Destroy(weapon.Instance);
        }
        else
        {
            _weapons.Add(type, weapon);
        }
        OnWeaponAdded?.Invoke(_weapons[type]);
    }
    
    public bool HasWeapon(WeaponType type)
    {
        return _weapons.ContainsKey(type);
    }

    public IInventoryWeapon GetWeaponByType(WeaponType type)
    {
        return _weapons[type];
    }
}