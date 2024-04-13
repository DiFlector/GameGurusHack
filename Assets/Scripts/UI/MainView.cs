using UnityEngine;

public class MainView : View
{
    [SerializeField] private HPBar _hpBar;
    [SerializeField] private WeaponLabel _weaponLabel;
    public override void Init()
    {
        _hpBar.SetHPAmount(Player.Instance.HPAmount);
        _hpBar.SetArmorAmount(Player.Instance.ArmorAmount);
        Player.Instance._currentWeapon.UpdateWeaponInfo();
        Player.Instance._currentWeapon.GetBulletInfo();
    }
}
