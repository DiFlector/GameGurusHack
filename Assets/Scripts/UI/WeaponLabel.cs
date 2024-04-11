using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponLabel : MonoBehaviour
{
    [SerializeField] private TMP_Text _ammoInGun;
    [SerializeField] private TMP_Text _ammoAtAll;
    [SerializeField] private Image _currentGun;

    private void OnEnable()
    {
        Weapon.OnAmmoChanged.AddListener(ChangeAmmo);
        Weapon.OnWeaponActive.AddListener(ChangeWeapon);
    }

    private void ChangeAmmo(int ammoIn, int ammoAll)
    {
        _ammoInGun.text = ammoIn.ToString();
        _ammoAtAll.text = ammoAll.ToString();
    }

    private void ChangeWeapon(WeaponData data)
    {
        _currentGun.sprite = data.Sprite;
    }
}
