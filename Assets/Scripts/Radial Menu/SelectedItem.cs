using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedItem : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _inventory.OnItemSelected += OnItemSelected;
    }
    
    private void OnDestroy()
    {
        _inventory.OnItemSelected -= OnItemSelected;
    }
    
    private void OnItemSelected(Sprite item)
    {
        _image.sprite = item;
        _image.SetNativeSize();
    }
}
