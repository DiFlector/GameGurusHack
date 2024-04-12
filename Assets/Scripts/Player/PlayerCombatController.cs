using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private InputActionAsset _actionAsset;
    private InputAction _fire;
    private InputAction _reload;
    private Player _player;
    [SerializeField] private Transform _camera;

    private void Awake()
    {
        _player = GetComponent<Player>();

        _fire = _actionAsset.FindAction("Fire");
        _reload = _actionAsset.FindAction("Reload");
        _fire.Enable();
        _reload.Enable();

        _fire.performed += Fire;
        _fire.canceled += Fire;
        _reload.performed += Reload;
    }

    private void OnEnable()
    {
        Player.OnShot.AddListener(ThrowShootCast);
    }

    private void ThrowShootCast()
    {
        Physics.Raycast(_camera.position, _camera.transform.forward, out RaycastHit hit, 10);
        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<IDamagable>() != null)
            {
                hit.collider.GetComponent<IDamagable>().ApplyDamage();
            }
        }
    }

    private void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _player.Shoot(true);
        }
        else if (context.canceled)
            _player.Shoot(false);
    }

    private void Reload(InputAction.CallbackContext context)
    {
        _player.Reload();
    }
}
