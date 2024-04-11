using System.Collections.Generic;
using System.Linq;
using SimplePieMenu;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public delegate void ItemSelectedHandler(Sprite item);
    public event ItemSelectedHandler OnItemSelected;
    
    [SerializeField] private InventoryCFG _inventoryCFG;
    private List<PieMenuItem> _items;
    private PieMenu _pieMenu;
    private int _selectedItemID = -1;

    private void Awake()
    {
        _pieMenu = GetComponent<PieMenu>();
        _pieMenu.OnPieMenuFullyInitialized += Initialize;
    }

    private void OnDisable()
    {
        _pieMenu.OnPieMenuFullyInitialized -= Initialize;
    }

    private void Initialize()
    {
        _items = _pieMenu.GetMenuItems().Values.ToList();

        for (var i = 0; i < _items.Count; i++)
        {
            if (TrySpawnItem(i, out _items[i].Item))
            {
                _items[i].Item.SetActive(false);
            }
            else
            {
                Debug.LogWarning($"Item with id {i} not found in inventory configuration!\n" +
                                 "Button will be disabled.");
                _items[i].Disabled = true;
            }
        }

        if (_items.Count < _inventoryCFG.Items.Count)
        {
            Debug.LogWarning($"Configured {_inventoryCFG.Items.Count} items, but only {_items.Count} buttons found!\n" +
                             "Some items will not be available!");
        }

        TrySelectItem(0);
    }

    public void TrySelectItem(int id)
    {
        if (id < _items.Count && _items[id].GetComponent<Button>().interactable)
        {
            SelectItem(id);
        }
    }

    private void SelectItem(int id)
    {
        if (_selectedItemID != -1) _items[_selectedItemID].Item.SetActive(false);
        _selectedItemID = id;
        _items[id].Item.SetActive(true);
        OnItemSelected?.Invoke(_inventoryCFG.Items[id].Icon);
    }

    private bool TrySpawnItem(int id, out GameObject item)
    {
        if (id < _inventoryCFG.Items.Count && _inventoryCFG.Items[id].Item != null)
        {
            item = SpawnItem(id);
            return true;
        }

        item = null;
        return false;
    }

    private GameObject SpawnItem(int id)
    {
        return Instantiate(_inventoryCFG.Items[id].Item);
    }
}