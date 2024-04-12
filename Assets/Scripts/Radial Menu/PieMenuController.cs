using System;
using System.Collections.Generic;
using SimplePieMenu;
using UnityEngine;

public class PieMenuController : MonoBehaviour
{
    public event Action<int> OnPieMenuItemClicked;
    
    [SerializeField] private PieMenu _pieMenu;
    private Dictionary<int, PieMenuItem> _pieMenuItems;
    private Inventory _inventory;
    private MenuItemDisabler _disabler;

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
        _pieMenu.OnPieMenuFullyInitialized += OnPieMenuFullyInitialized;
        _inventory.OnWeaponAdded += OnWeaponAdded;
    }

    private void OnDestroy()
    {
        _pieMenu.OnPieMenuFullyInitialized -= OnPieMenuFullyInitialized;
        _inventory.OnWeaponAdded -= OnWeaponAdded;
    }
    
    private void OnPieMenuFullyInitialized()
    {
        _disabler = PieMenuShared.References.MenuItemsManager.MenuItemDisabler;
        _pieMenuItems = _pieMenu.GetMenuItems();

        foreach ((int id, PieMenuItem item) in _pieMenuItems)
        {
            GetOrCreateClickHandler<PieMenuItemClickHandler>(item).OnPieMenuItemClicked += OnPieMenuItemSelected;
            bool hasWeapon = _inventory.HasWeapon((WeaponType)id);
            ToggleDisableMenuItem(id, !hasWeapon);
            if (hasWeapon)
            {
                _inventory.GetWeaponByType((WeaponType)id).OnAmmoChanged.AddListener(UpdateWeaponDetails);
                UpdatePieMenuItemInfo(id, _inventory.GetWeaponByType((WeaponType)id));
            }
        }
    }
    
    private void ToggleDisableMenuItem(int menuItemId, bool disabled)
    {
        _disabler.ToggleDisable(_pieMenu, menuItemId, disabled);
    }

    private void UpdatePieMenuItemInfo(int menuItemId, IInventoryWeapon weapon)
    {
        _pieMenuItems[menuItemId].SetHeader(weapon.Name);
        UpdateWeaponDetails(menuItemId, weapon);
    }

    private void UpdateWeaponDetails(int menuItemId, IInventoryWeapon weapon)
    {
        _pieMenuItems[menuItemId].SetDetails($"Ammo: {weapon.Ammo} / {weapon.MaxAmmo}");
    }
    
    private void OnPieMenuItemSelected(PieMenuItem item)
    {
        OnPieMenuItemClicked?.Invoke(item.Id);
    }
    
    private void OnWeaponAdded(IInventoryWeapon weapon)
    {
        if (PieMenuShared.Instance == null) return;
        ToggleDisableMenuItem((int)weapon.Type, false);
        UpdatePieMenuItemInfo((int)weapon.Type, weapon);
    }

    private T GetOrCreateClickHandler<T>(PieMenuItem menuItem) where T : Component, IMenuItemClickHandler
    {
        if (!menuItem.TryGetComponent(out T component))
        {
            component = menuItem.gameObject.AddComponent<T>();
        }

        return component;
    }
}
