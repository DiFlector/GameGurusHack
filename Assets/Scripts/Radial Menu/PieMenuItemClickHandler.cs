using System;
using System.Collections;
using System.Collections.Generic;
using SimplePieMenu;
using UnityEngine;

public class PieMenuItemClickHandler : MonoBehaviour, IMenuItemClickHandler
{
    private PieMenuItem _pieMenuItem;
    private PieMenu _pieMenu;
    private int _id;
    private Inventory _inventory;
    
    private void Start()
    {
        _pieMenuItem = GetComponent<PieMenuItem>();
        _pieMenu = _pieMenuItem.PieMenu;
        _id = _pieMenuItem.Id;
        _inventory = _pieMenu.GetComponent<Inventory>();
    }

    public void Handle()
    {
        Debug.Log($"Pie menu item ({_id}) clicked!");
        _inventory.TrySelectItem(_id);
    }
}
