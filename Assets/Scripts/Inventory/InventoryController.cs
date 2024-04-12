using SimplePieMenu;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private PieMenu _pieMenu;
    private Inventory _inventory;
    
    class InventoryWeapon : IInventoryWeapon
    {
        public UnityEvent<int, IInventoryWeapon> OnAmmoChanged { get; } = new();
        public WeaponType Type { get; set; }
        public string Name { get; set; }
        public GameObject Instance { get; set; }
        public int Ammo { get; set; }
        public int MaxAmmo { get; set; }
        
        public override string ToString()
        {
            return $"Weapon: {Name}, Ammo: {Ammo}, Type: {Type}";
        }
    }

    private void Start()
    {
        _inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (PieMenuShared.Instance == null)
            {
                _pieMenu.gameObject.SetActive(true);
            }
            else
            {
                PieMenuShared.References.PieMenuToggler.SetActive(_pieMenu, !_pieMenu.PieMenuInfo.IsActive);
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            InventoryWeapon weapon = new InventoryWeapon();
            weapon.Type = (WeaponType)Random.Range(0, 5);
            weapon.Name = weapon.Type.ToString();
            weapon.Instance = new GameObject() {name = weapon.Name};
            weapon.Ammo = Random.Range(10, 101);
            weapon.MaxAmmo = 20;
            Debug.Log(weapon);
            _inventory.AddWeapon(weapon);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            IInventoryWeapon weapon = _inventory.GetWeaponByType(WeaponType.Riffle);
            weapon.Ammo -= 10;
            weapon.OnAmmoChanged?.Invoke((int)weapon.Type, weapon);
            
            Debug.Log($"Ammo: {_inventory.GetWeaponByType(WeaponType.Riffle).Ammo}");
        }
    }
}
