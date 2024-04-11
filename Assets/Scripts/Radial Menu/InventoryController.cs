using SimplePieMenu;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private PieMenu _pieMenu;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            PieMenuShared.References.PieMenuToggler.SetActive(_pieMenu, !_pieMenu.PieMenuInfo.IsActive);
        }
    }
}
