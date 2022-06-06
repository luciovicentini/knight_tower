using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private InputAction input;

    private void Awake()
    {
        Debug.Log(input);
        input.Enable();

        input.performed += Input_performed;
        input.canceled += Input_canceled;
    }

    private void Input_canceled(InputAction.CallbackContext context)
    {
        Debug.Log(input.ReadValue<Vector2>());
    }

    private void Input_performed(InputAction.CallbackContext context)
    {
        Debug.Log(input.ReadValue<Vector2>());
    }
}
