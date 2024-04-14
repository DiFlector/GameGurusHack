using UnityEngine;


public enum AnimType
{
    Idle,
    Reload,
    WeaponDown,
    WeaponUp
}

public class WeaponAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _weaponAnimator;
    private string _currentState;

    private const string WEAPON_DOWN = "Down";
    private const string WEAPON_UP = "Up";
    private const string WEAPON_RELOAD = "Reload";
    private const string WEAPON_IDLE = "Idle";

    public void Animate(AnimType type)
    {
        switch (type)
        {
            case AnimType.Idle:
                ChangeState(WEAPON_IDLE);
                break;
            case AnimType.Reload:
                ChangeState(WEAPON_RELOAD);
                break;
            case AnimType.WeaponDown:
                ChangeState(WEAPON_DOWN);
                break;
            case AnimType.WeaponUp:
                ChangeState(WEAPON_UP);
                break;
        }
    }
    private void ChangeState(string state)
    {
        if (_currentState == state) return;
        _weaponAnimator.Play(state);
        _weaponAnimator.CrossFadeInFixedTime(state, 0.2f);
        _currentState = state;
    }
}
