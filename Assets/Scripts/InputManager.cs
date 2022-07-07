using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {

    [SerializeField] private InputAction positionInput;
    [SerializeField] private InputAction pressInput;

    private List<IDrag> draggableList;
    private List<IPressed> pressableList;
    
    private float draggingStartingTime = .25f;
    private bool wasDragging;
    
    private Vector2 inputPosition;

    private Camera mainCamera;

    private void Awake() {
        mainCamera = Camera.main;
        draggableList = new List<IDrag>();
        pressableList = new List<IPressed>();
    }

    private void LivesManager_OnPlayerDie(object sender, EventArgs e) {
        gameObject.SetActive(false);
    }

    private void OnEnable() {
        positionInput.Enable();
        pressInput.Enable();
        
        LivesManager.OnPlayerDie += LivesManager_OnPlayerDie;
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
        wasDragging = false;
        Ray ray = mainCamera.ScreenPointToRay(inputPosition);
    
        RaycastHit2D[] hits2DArray = Physics2D.GetRayIntersectionAll(ray);
        
        foreach (var hit2D in hits2DArray) {
            if (hit2D.collider != null) {
                IDrag iDragComponent = hit2D.collider.transform.GetComponent<IDrag>();
                if (iDragComponent != null) {
                    draggableList.Add(iDragComponent);
                    // iDragComponent.OnDragStart(GetPointerWorldPosition());
                }

                if (hit2D.collider.TryGetComponent(out IPressed pressed)) {
                    pressableList.Add(pressed);
                }
            }
        }

        StartCameraDragging();
        StartCoroutine(DragUpdate());
    }

    private void PressInput_canceled(InputAction.CallbackContext obj) {
        if (wasDragging) {
            foreach (var iDragComponent in draggableList) {
                iDragComponent?.OnDragEnd();
            }    
        } else {
            foreach (var pressComponent in pressableList) {
                pressComponent?.OnPressed();
            }
        }
        
        draggableList = new List<IDrag>();
        pressableList = new List<IPressed>();
    }

    private IEnumerator DragUpdate() {
        float draggingTime = 0f;
        while (pressInput.ReadValue<float>() != 0) {
            draggingTime += Time.deltaTime;

            if (!wasDragging) {
                if (draggingTime > draggingStartingTime) {
                    wasDragging = true;
                    foreach (var iDragComponent in draggableList) 
                        iDragComponent?.OnDragStart(GetPointerWorldPosition());
                }
            } else {
                foreach (var iDragComponent in draggableList) 
                    iDragComponent?.OnDragging(GetPointerWorldPosition());
            }
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
        draggableList.Add(iDragComponent);
        iDragComponent?.OnDragStart(GetPointerWorldPosition());
    }

    public void Activate() {
        gameObject.SetActive(true);
    }
}