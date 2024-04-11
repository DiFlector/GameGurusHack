using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/InventoryCFG")]
public class InventoryCFG : ScriptableObject
{
    [Serializable]
    public struct InventoryItem
    {
        public GameObject Item;
        public Sprite Icon;
    }
    
    public List<InventoryItem> Items = new List<InventoryItem>();
}
