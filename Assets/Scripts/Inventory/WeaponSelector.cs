using System;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    public event Action<IInventoryWeapon> OnWeaponSelected;
    
    private PieMenuController _pieMenuController;
    private Inventory _inventory;
    private IInventoryWeapon _selected;

    private void Start()
    {
        _pieMenuController = GetComponent<PieMenuController>();
        _inventory = GetComponent<Inventory>();
        _pieMenuController.OnPieMenuItemClicked += OnPieMenuItemClicked;
        _inventory.OnWeaponAdded += OnWeaponAdded;
    }

    private void OnDestroy()
    {
        _pieMenuController.OnPieMenuItemClicked -= OnPieMenuItemClicked;
        _inventory.OnWeaponAdded -= OnWeaponAdded;
    }

    private void OnWeaponAdded(IInventoryWeapon weapon)
    {
        if (_selected == null)
        {
            Select(weapon.Type);
            OnWeaponSelected?.Invoke(weapon);
        }
        else
        {
            if (_selected.Type != weapon.Type)
            {
                weapon.Instance.SetActive(false);
            }
        }
    }
    
    private void OnPieMenuItemClicked(int menuItemId)
    {
        TrySelect((WeaponType)menuItemId);
    }

    private void TrySelect(WeaponType type)
    {
        if (_inventory.HasWeapon(type))
        {
            Select(type);
        }
    }

    private void Select(WeaponType type)
    {
        if (_selected != null)
        {
            _selected.Instance.SetActive(false);
        }
        _selected = _inventory.GetWeaponByType(type);
        _selected.Instance.SetActive(true);
        OnWeaponSelected?.Invoke(_selected);
    }
}
