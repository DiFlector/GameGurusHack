using TMPro;
using UnityEngine;

public class MainView : View
{
    [SerializeField] private HPBar _hpBar;
    [SerializeField] private WeaponLabel _weaponLabel;
    [SerializeField] private TMP_Text _ammoAmount;
    public override void Init()
    {
        _hpBar.SetHPAmount(Player.Instance.HPAmount);
        Player.Instance._currentWeapon.UpdateWeaponInfo();
    }
}
