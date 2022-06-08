using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //public event 

    [SerializeField]
    private InputAction positionInput;

    [SerializeField]
    private InputAction pressInput;

    private Camera mainCamera;
    private Vector2 inputPosition;
    private IDrag iDragComponent;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        positionInput.Enable();
        pressInput.Enable();
        positionInput.performed += PositionInput_performed;
        pressInput.performed += PressInput_performed;
        pressInput.canceled += PressInput_canceled;
    }

    private void OnDisable()
    {
        positionInput.performed -= PositionInput_performed;
        pressInput.performed -= PressInput_performed;

        positionInput.Disable();
        pressInput.Disable();
    }

    private void PositionInput_performed(InputAction.CallbackContext context)
    {
        inputPosition = positionInput.ReadValue<Vector2>();
    }

    private void PressInput_performed(InputAction.CallbackContext obj)
    {
        Ray ray = mainCamera.ScreenPointToRay(inputPosition);

        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);

        if (hit2D.collider != null)
        {
            hit2D.collider.transform.TryGetComponent(out iDragComponent);
            iDragComponent?.OnDragStart(GetPointerWorldPosition());
            StartCoroutine(DragUpdate());
        }

    }

    private void PressInput_canceled(InputAction.CallbackContext obj)
    {
        iDragComponent?.OnDragEnd();
        iDragComponent = null;
    }

    private IEnumerator DragUpdate()
    {
        while (pressInput.ReadValue<float>() != 0)
        {
            iDragComponent?.OnDragging(GetPointerWorldPosition());
            yield return null;
        }
    }

    private Vector2 GetPointerWorldPosition() => mainCamera.ScreenToWorldPoint(inputPosition);

}
