using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum WeaponType
{
    Pistol,
    Rifle,
    Shotgun
}

public class CircleMenu : MonoBehaviour
{
    public static UnityEvent<WeaponType> OnWeaponSwap = new();
    public static UnityEvent<bool> OnSelectorOpened = new();
    public bool isEnabled;

    [SerializeField] private Button _pistolBtn;
    [SerializeField] private Button _rifleBtn;
    [SerializeField] private Button _shotgunBtn;

    private void OnEnable()
    {
        _pistolBtn.onClick.AddListener(() => OnWeaponSwap.Invoke(WeaponType.Pistol));
        _rifleBtn.onClick.AddListener(() => OnWeaponSwap.Invoke(WeaponType.Rifle));
        _shotgunBtn.onClick.AddListener(() => OnWeaponSwap.Invoke(WeaponType.Shotgun));
    }

    private void OnDisable()
    {
        _pistolBtn.onClick.RemoveListener(() => OnWeaponSwap.Invoke(WeaponType.Pistol));
        _rifleBtn.onClick.RemoveListener(() => OnWeaponSwap.Invoke(WeaponType.Rifle));
        _shotgunBtn.onClick.RemoveListener(() => OnWeaponSwap.Invoke(WeaponType.Shotgun));
    }

    public void Toggle()
    {
        if (!isEnabled)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            isEnabled = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            OnSelectorOpened.Invoke(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            isEnabled = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            OnSelectorOpened.Invoke(false);
        }
    }
}
