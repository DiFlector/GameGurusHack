using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputAsset;
    private InputAction _movement;
    private InputAction _jump;
    private InputAction _sprint;

    private CharacterController _controller;

    private Vector3 _moveDir;
    private Vector3 _velocity;
    private Vector2 _currInputVector;
    private Vector2 _smoothInputVector;
    private float _smoothMoveVector;

    private bool _isJumping;

    [SerializeField] private Transform _mainCam;

    [SerializeField] private float _gravity = 18f;
    private float _moveSpeed;
    [SerializeField] private float _walkSpeed = 6f;
    [SerializeField] private float _sprintSpeed = 10f;
    [SerializeField] private float _jumpForce = 10;
    [Range(0, 0.5f)]
    [SerializeField] private float _smoothVelocityValue = 0.12f;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _controller = GetComponent<CharacterController>();

        _moveSpeed = _walkSpeed;

        var inputActionMap = _inputAsset.FindActionMap("Player");
        _movement = inputActionMap.FindAction("Movement");
        _jump = inputActionMap.FindAction("Jump");
        _sprint = inputActionMap.FindAction("Sprint");

        _movement.Enable();
        _jump.Enable();
        _sprint.Enable();

        _jump.performed += OnJump;
        _jump.canceled += OnJump;
    }

    private void Update()
    {
        Vector2 direction = _movement.ReadValue<Vector2>();
        _currInputVector = Vector2.SmoothDamp(_currInputVector, direction, ref _smoothInputVector, _smoothVelocityValue);
        _moveDir = new Vector3(_currInputVector.x, 0, _currInputVector.y);

        if (_sprint.ReadValue<float>() != 0 && !_isJumping)
            _moveSpeed = Mathf.SmoothDamp(_moveSpeed, _sprintSpeed, ref _smoothMoveVector, _smoothVelocityValue);
        else
            _moveSpeed = Mathf.SmoothDamp(_moveSpeed, _walkSpeed, ref _smoothMoveVector, _smoothVelocityValue);

        if (_controller.isGrounded && !_isJumping)
            _velocity.y = -2f;
        else
            _velocity.y -= _gravity * Time.deltaTime;

        _velocity.x = 0;
        _velocity.z = 0;

        transform.DORotate(new Vector3(0, _mainCam.eulerAngles.y, 0), 0.1f);
        Vector3 tempVelocity = _moveDir.x * transform.right * _moveSpeed + _moveDir.z * transform.forward * _moveSpeed;
        _velocity += tempVelocity; 

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (!context.action.WasPerformedThisFrame() && _controller.isGrounded)
            StartCoroutine(Jump());
    }


    private IEnumerator Jump()
    {
        _isJumping = true;
        _velocity.y += _jumpForce;
        yield return new WaitForSeconds(0.2f);
        while (!_controller.isGrounded)
            yield return null;
        _isJumping = false;
    }
}
