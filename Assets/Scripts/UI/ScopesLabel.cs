using UnityEngine;

public class ScopesLabel : MonoBehaviour
{
    [SerializeField] private GameObject _narrowScope;
    [SerializeField] private GameObject _wideScope;

    private void OnEnable()
    {
        Weapon.OnWeaponActive.AddListener(ChangeScope);
    }

    private void ChangeScope(WeaponData data)
    {
        if (data.WideScope)
        {
            _wideScope.SetActive(true);
            _narrowScope.SetActive(false);
        }
        else
        {
            _wideScope.SetActive(false);
            _narrowScope.SetActive(true);
        }
    }
}
