using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class WeaponLabel : MonoBehaviour
{
    [SerializeField] private TMP_Text _ammoInGun;
    [SerializeField] private TMP_Text _ammoAtAll;
    [SerializeField] private Image _currentGun;
    [SerializeField] private UICircle _reloadCircle;

    private void OnEnable()
    {
        Weapon.OnAmmoChanged.AddListener(ChangeAmmo);
        Weapon.OnWeaponActive.AddListener(ChangeWeapon);
        Weapon.OnReload.AddListener(DisplayReload);
    }

    private void ChangeAmmo(int ammoIn, int ammoAll)
    {
        Debug.Log("AMMO CHANGED)))))))))");
        _ammoInGun.text = ammoIn.ToString();
        _ammoAtAll.text = ammoAll.ToString();
    }

    private void ChangeWeapon(WeaponData data)
    {
        _currentGun.sprite = data.Sprite;
    }

    private void DisplayReload(float duration)
    {
        StartCoroutine(DisplayReloadProgress(duration));
    }

    private IEnumerator DisplayReloadProgress(float duration)
    {
        float increment = 1 / duration;
        while (_reloadCircle.Progress < 1)
        {
            _reloadCircle.SetProgress(_reloadCircle.Progress + increment * Time.deltaTime);
            yield return null;
        }
        _reloadCircle.SetProgress(0);
    }
}
