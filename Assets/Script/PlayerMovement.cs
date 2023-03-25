using NaughtyAttributes;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;
using System;
using TMPro;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerMovement : MonoBehaviour
{
    public LayerMask _movementMask;
    public Interactable _Focus;
    [SerializeField] PlayerMotor _motor;
    [SerializeField] CharacterController _characterController;
    [SerializeField] Camera _playerCamera;
    [SerializeField] InputActionReference _moveInput;
    [SerializeField] InputActionReference _runInput;
    [SerializeField] InputActionReference _crawlInput;
    [SerializeField] public float _speed;
    [SerializeField] public float _runSpeed;
    [SerializeField] public float _rotationSpeed;
    [SerializeField] public float _stoppingDistance;

    public bool updateRotation;
    bool _isMoving;
    bool _isRunning;
    bool _isCrawling;
    Vector3 _playerMovement;
    Vector3 _calculatedDirection;
    void Start()
    {
        _playerCamera = Camera.main;
        _motor = GetComponent<PlayerMotor>();

        _moveInput.action.started += StartMove;
        _moveInput.action.performed += StartMove;
        _moveInput.action.canceled += EndMove;

        _runInput.action.started += StartRun;
        _runInput.action.canceled += EndRun;

        _crawlInput.action.started += StartCrawl;
        _crawlInput.action.canceled += EndCrawl;
    }
    private void OnDestroy()
    {
        _moveInput.action.started -= StartMove;
        _moveInput.action.performed -= StartMove;
        _moveInput.action.canceled -= EndMove;

        _runInput.action.started -= StartRun;
        _runInput.action.canceled -= EndRun;

        _crawlInput.action.started -= StartCrawl;
        _crawlInput.action.canceled -= EndCrawl;
    }
  
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100, _movementMask))
            {
                Debug.Log("We Hit" + hit.collider.name + "" + hit.point);
                //Move oru player to what we hit
                RemoveFocus();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                 if(interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

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

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != _Focus)
        {
            if(newFocus != null)
            {
                _Focus.OnDefocused();
            }
            _Focus = newFocus;
            _motor.FollowTarget(newFocus);
        }
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if(_Focus != null)
        {
            _Focus.OnDefocused();
        }
        _Focus = null;
        _motor.StopFollowingTarget();
    }
    private void StartMove(InputAction.CallbackContext obj)
    {
        _playerMovement = obj.ReadValue<Vector3>();
        _isMoving = true;
    }

    private void EndMove(InputAction.CallbackContext obj)
    {
        _playerMovement = Vector3.zero;
        _isMoving = false;
    }

    private void StartRun(InputAction.CallbackContext obj)
    {
        _isRunning = true;
    }
    private void EndRun(InputAction.CallbackContext obj)
    {
        _isRunning = false;
    }

    private void StartCrawl(InputAction.CallbackContext obj)
    {
        Debug.Log("Crawl");
        _isCrawling = true;
    }

    private void EndCrawl(InputAction.CallbackContext obj)
    {
        Debug.Log("StandUp");
        _isCrawling = false;
    }

    internal void SetDestination(Vector3 point)
    {
        throw new NotImplementedException();
    }
}
