using System;
using SimplePieMenu;
using UnityEngine;

public class PieMenuItemClickHandler : MonoBehaviour, IMenuItemClickHandler
{
    public event Action<PieMenuItem> OnPieMenuItemClicked;
    private PieMenuItem _pieMenuItem;
    
    private void Start()
    {
        _pieMenuItem = GetComponent<PieMenuItem>();
    }

    private void OnDestroy()
    {
        OnPieMenuItemClicked = null;
    }

    public void Handle()
    {
        OnPieMenuItemClicked?.Invoke(_pieMenuItem);
    }
}
