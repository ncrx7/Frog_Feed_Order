using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    [SerializeField] PlayerInput _playerInput;
    InputAction _selectAction;
    InputAction _clickAction;

    //public event Action ClickEvent;

    public Vector2 MousePosition => _selectAction.ReadValue<Vector2>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _selectAction = _playerInput.actions["Select"];
        _clickAction = _playerInput.actions["Click"];

        _clickAction.performed += OnFired;
    }

    private void OnDisable()
    {
        _clickAction.performed -= OnFired;
    }

    private void OnFired(InputAction.CallbackContext obj)
    {
        GridBoardEventSystem.ClickEvent?.Invoke();
        //ClickEvent?.Invoke();
    }

/*     private void Update()
    {
        Debug.Log("mouse pos: " + MousePosition);
    } */
}
