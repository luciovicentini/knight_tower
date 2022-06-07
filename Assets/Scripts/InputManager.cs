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

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        positionInput.Enable();
        pressInput.Enable();
        positionInput.performed += Input_performed;
        pressInput.canceled += Input_canceled;
    }

    private void OnDisable()
    {
        positionInput.performed -= Input_performed;
        pressInput.canceled -= Input_canceled;
        positionInput.Disable();
        pressInput.Disable();
    }

    private void Input_performed(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(positionInput.ReadValue<Vector2>());

        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
        if (hit2D.collider != null)
        {
            StartCoroutine(DragUpdate(hit2D.collider.transform));
        }
    }

    private IEnumerator DragUpdate(Transform transform)
    {
        while (pressInput.ReadValue<float>() != 0)
        {
            transform.TryGetComponent<IDrag>(out var iDragComponent);
            iDragComponent?.OnDragStart(GetPointerWorldPosition());
            yield return null;
        }
    }

    private void Input_canceled(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(positionInput.ReadValue<Vector2>());

        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
        if (hit2D.collider != null)
        {
            transform.TryGetComponent<IDrag>(out var iDragComponent);
            iDragComponent?.OnDragEnd(GetPointerWorldPosition());
        }
    }

    private Vector2 GetPointerWorldPosition() => mainCamera.ScreenToWorldPoint(positionInput.ReadValue<Vector2>());

}
