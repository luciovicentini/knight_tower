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
    private List<IDrag> iDragComponentList;

    private void Awake()
    {
        mainCamera = Camera.main;
        iDragComponentList = new List<IDrag>();
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

        RaycastHit2D[] hits2DArray = Physics2D.GetRayIntersectionAll(ray);

        foreach (RaycastHit2D hit2D in hits2DArray)
        {
            if (hit2D.collider != null)
            {
                IDrag iDragComponent = hit2D.collider.transform.GetComponent<IDrag>();
                Debug.Log($"[InputManager](PressInput_Performed) idragComponent = {iDragComponent}");
                if (iDragComponent != null)
                {
                    iDragComponentList.Add(iDragComponent);
                    iDragComponent.OnDragStart(GetPointerWorldPosition());
                    for (int i = 0; i < iDragComponentList.Count; i++)
                    {
                        Debug.Log($"[InputManager](PressInput_Performed) idragComponent(List) = {iDragComponentList[i]}");
                    }
                }
            }
        }
        StartCameraDragging();

        StartCoroutine(DragUpdate());
    }

    private void PressInput_canceled(InputAction.CallbackContext obj)
    {
        for (int i = 0; i < iDragComponentList.Count; i++)
        {
            Debug.Log($"[InputManager](PressInput_canceled) idragComponent(List) = {iDragComponentList[i]}");
        }
        foreach (IDrag iDragComponent in iDragComponentList)
        {
            iDragComponent?.OnDragEnd();
        }
        iDragComponentList = new List<IDrag>();
    }

    private IEnumerator DragUpdate()
    {
        while (pressInput.ReadValue<float>() != 0)
        {
            foreach (IDrag iDragComponent in iDragComponentList)
            {
                Debug.Log($"[InputManager](DragUpdate) idragComponent(List) = {iDragComponent}");
                iDragComponent?.OnDragging(GetPointerWorldPosition());
            }

            yield return null;
        }
    }

    private Vector2 GetPointerWorldPosition() => mainCamera.ScreenToWorldPoint(inputPosition);

    private void StartCameraDragging()
    {
        Transform cameraFollowTransform = GameObject.Find("/MainObjects/CameraFollow")?.transform;

        if (cameraFollowTransform == null) return;
        IDrag iDragComponent = cameraFollowTransform.GetComponent<IDrag>();
        iDragComponentList.Add(iDragComponent);
        iDragComponent?.OnDragStart(GetPointerWorldPosition());
    }
}
