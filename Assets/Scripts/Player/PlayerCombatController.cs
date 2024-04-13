using System.Linq;
using Unity.VisualScripting;
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


    private void ThrowShootCast(bool wideScope)
    {
        if (!wideScope)
        {
            Physics.Raycast(_camera.position, _camera.transform.forward, out RaycastHit hit, 100);
            if (hit.collider != null)
            {
                hit.collider.GetComponent<IDamagable>()?.ApplyDamage();
            }
        }
        else
        {
            RaycastHit[] hits = new RaycastHit[10];
            Physics.Raycast(_camera.position, new Vector3(_camera.transform.forward.x + 0.1f, _camera.transform.forward.y, _camera.transform.forward.z), out hits[0], 10);
            Physics.Raycast(_camera.position, new Vector3(_camera.transform.forward.x - 0.1f, _camera.transform.forward.y, _camera.transform.forward.z), out hits[1], 10);
            Physics.Raycast(_camera.position, new Vector3(_camera.transform.forward.x + 0.1f, _camera.transform.forward.y + 0.1f, _camera.transform.forward.z), out hits[2], 10);
            Physics.Raycast(_camera.position, new Vector3(_camera.transform.forward.x - 0.1f, _camera.transform.forward.y - 0.1f, _camera.transform.forward.z), out hits[3], 10);
            Physics.Raycast(_camera.position, new Vector3(_camera.transform.forward.x + 0.1f, _camera.transform.forward.y - 0.1f, _camera.transform.forward.z), out hits[4], 10);
            Physics.Raycast(_camera.position, new Vector3(_camera.transform.forward.x - 0.1f, _camera.transform.forward.y + 0.1f, _camera.transform.forward.z), out hits[5], 10);
            Physics.Raycast(_camera.position, new Vector3(_camera.transform.forward.x, _camera.transform.forward.y + 0.1f, _camera.transform.forward.z), out hits[6], 10);
            Physics.Raycast(_camera.position, new Vector3(_camera.transform.forward.x, _camera.transform.forward.y - 0.1f, _camera.transform.forward.z), out hits[7], 10);
            Physics.Raycast(_camera.position, _camera.transform.forward, out hits[8], 20);

            int hitted = 0;
            IDamagable enemy = null;
            foreach (var hit in hits)
            {
                if (hit.collider != null)
                {
                    if (hit.collider.GetComponent<IDamagable>() != null)
                    {
                        hitted++;
                        enemy = hit.collider.GetComponent<IDamagable>();
                    }
                }
            }
            enemy?.ApplyDamage();
            if (hitted > 4)
                enemy?.ApplyDamage();
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
