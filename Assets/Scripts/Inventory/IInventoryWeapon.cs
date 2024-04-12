using UnityEngine;
using UnityEngine.Events;

public interface IInventoryWeapon
{
    public UnityEvent<int, IInventoryWeapon> OnAmmoChanged { get; }
    public string Name { get; }
    public GameObject Instance { get; }
    public int Ammo { get; set; }
    public int MaxAmmo { get; }
    public WeaponType Type { get; }
}

