using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {

    [SerializeField] private InputAction positionInput;
    [SerializeField] private InputAction pressInput;

    private List<IDrag> iDragComponentList;
    private Vector2 inputPosition;

    private Camera mainCamera;

    private void Awake() {
        mainCamera = Camera.main;
        iDragComponentList = new List<IDrag>();
        LivesManager.OnPlayerDie += LivesManager_OnPlayerDie;
    }

    private void LivesManager_OnPlayerDie(object sender, EventArgs e) {
        gameObject.SetActive(false);
    }

    private void OnEnable() {
        positionInput.Enable();
        pressInput.Enable();
        
        positionInput.performed += PositionInput_performed;
        pressInput.performed += PressInput_performed;
        pressInput.canceled += PressInput_canceled;
    }

    private void OnDisable() {
        positionInput.performed -= PositionInput_performed;
        pressInput.performed -= PressInput_performed;
        
        positionInput.Disable();
        pressInput.Disable();
        
        LivesManager.OnPlayerDie -= LivesManager_OnPlayerDie;
    }

    private void PositionInput_performed(InputAction.CallbackContext context) {
        inputPosition = positionInput.ReadValue<Vector2>();
    }

    private void PressInput_performed(InputAction.CallbackContext obj) {
        Ray ray = mainCamera.ScreenPointToRay(inputPosition);

        RaycastHit2D[] hits2DArray = Physics2D.GetRayIntersectionAll(ray);

        foreach (var hit2D in hits2DArray) {
            if (hit2D.collider != null) {
                IDrag iDragComponent = hit2D.collider.transform.GetComponent<IDrag>();
                if (iDragComponent != null) {
                    iDragComponentList.Add(iDragComponent);
                    iDragComponent.OnDragStart(GetPointerWorldPosition());
                }

                if (hit2D.collider.TryGetComponent(out IPressed pressed)) {
                    pressed.OnPressed();   
                }
            }
        }

        StartCameraDragging();
        StartCoroutine(DragUpdate());
    }

    private void PressInput_canceled(InputAction.CallbackContext obj) {
        foreach (var iDragComponent in iDragComponentList) {
            iDragComponent?.OnDragEnd();
        }
        iDragComponentList = new List<IDrag>();
    }

    private IEnumerator DragUpdate() {
        while (pressInput.ReadValue<float>() != 0) {
            foreach (var iDragComponent in iDragComponentList) iDragComponent?.OnDragging(GetPointerWorldPosition());

            yield return null;
        }
    }

    private Vector2 GetPointerWorldPosition() {
        return mainCamera.ScreenToWorldPoint(inputPosition);
    }

    private void StartCameraDragging() {
        var cameraFollowTransform = GameObject.Find("/CameraHandler/CameraFollow")?.transform;
        if (cameraFollowTransform == null) return;
        var iDragComponent = cameraFollowTransform.GetComponent<IDrag>();
        iDragComponentList.Add(iDragComponent);
        iDragComponent?.OnDragStart(GetPointerWorldPosition());
    }

    public void Activate() {
        gameObject.SetActive(true);
    }
}