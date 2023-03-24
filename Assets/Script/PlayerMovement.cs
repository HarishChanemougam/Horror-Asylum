using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController _characterController;
    [SerializeField] Camera _playerCamera;
    [SerializeField] InputActionReference _moveInput;
    [SerializeField] InputActionReference _runInput;
    [SerializeField] InputActionReference _jumpInput;
    [SerializeField] InputActionReference _useInput;
    [SerializeField] InputActionReference _exitInput;
    [SerializeField] Animator _animator;
    [SerializeField] Vector3 _jumpForce;
    [SerializeField] float _speed;
    [SerializeField] float _runSpeed;
    [SerializeField] float _rotationSpeed;
    bool _isMoving;
    bool _isRunning;
    bool _isJumping;
    Vector3 _playerMovement;
    Vector3 _calculatedDirection;
    void Start()
    {
        _moveInput.action.started += StartMove;
        _moveInput.action.performed += StartMove;
        _moveInput.action.canceled += EndMove;

        _runInput.action.started += StartRun;
        _runInput.action.canceled += EndRun;

        _jumpInput.action.started += StartJump;
        _jumpInput.action.canceled += EndJump;

        _useInput.action.started += StartUse;
        _useInput.action.canceled += EndUse;

        _useInput.action.started += StartExit;
        _useInput.action.canceled += EndExit;
    }
    private void OnDestroy()
    {
        _moveInput.action.started -= StartMove;
        _moveInput.action.performed -= StartMove;
        _moveInput.action.canceled -= EndMove;

        _runInput.action.started -= StartRun;
        _runInput.action.canceled -= EndRun;

        _jumpInput.action.started -= StartJump;
        _jumpInput.action.canceled -= EndJump;

        _useInput.action.started += StartUse;
        _useInput.action.canceled += EndUse;

        _useInput.action.started += StartExit;
        _useInput.action.canceled += EndExit;
    }
  
    void Update()
    {
        Vector3 CalculatedDirection = _playerMovement * Time.deltaTime * _speed;
        if (_isRunning)
        {
            CalculatedDirection *= _runSpeed;
        }
        _characterController.Move(CalculatedDirection);

        CalculatedDirection = _playerCamera.transform.TransformDirection(CalculatedDirection);
        CalculatedDirection.y = 0;

        float YAxis = _playerCamera.transform.localEulerAngles.y;
        float currentYAxis = _characterController.transform.localEulerAngles.y;
        float Value = Mathf.LerpAngle(currentYAxis, YAxis, Time.deltaTime * _rotationSpeed);
        _characterController.transform.rotation = Quaternion.Euler(0, Value, 0);
      
    }
    private void StartMove(InputAction.CallbackContext obj)
    {
        Debug.Log("MOVE");
        _playerMovement = obj.ReadValue<Vector3>();
        _isMoving = true;
        _animator.SetBool("Standard Walk", true);
    }

    private void EndMove(InputAction.CallbackContext obj)
    {
        Debug.Log("don't move");
        _playerMovement = Vector3.zero;
        _isMoving = false;
        _animator.SetBool("Standard Walk", false);
    }

    private void StartRun(InputAction.CallbackContext obj)
    {
        Debug.Log("RUN");
        _isRunning = true;
        _animator.SetBool("Running", true);     
    }
    private void EndRun(InputAction.CallbackContext obj)
    {
        Debug.Log("StopRUN");
        _isRunning = false;
        _animator.SetBool("Running", false);
       
    }

    private void StartJump(InputAction.CallbackContext obj)
    {
        Debug.Log("JUMP");
        _isJumping = true;
    }

    private void EndJump(InputAction.CallbackContext obj)
    {
        Debug.Log("GROUND");
        _isJumping = false;
    }

    private void StartUse(InputAction.CallbackContext obj)
    {
        Debug.Log("USE");
    }

    private void EndUse(InputAction.CallbackContext obj)
    {
        Debug.Log("Don't");
    }

    private void StartExit(InputAction.CallbackContext obj)
    {
        Debug.Log("Exit");
    }

    private void EndExit(InputAction.CallbackContext obj)
    {
        Debug.Log("EXit");
    }
}
