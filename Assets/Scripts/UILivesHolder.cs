using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILivesHolder : MonoBehaviour {
    
    private Transform live1;
    private Transform live2;
    private Transform live3;

    private void Awake() {
        live1 = transform.Find("live1");
        live2 = transform.Find("live2");
        live3 = transform.Find("live3");
    }
    
    void Start() {
        LivesManager.OnPlayerLoseLife += LivesManager_OnPlayerLoseLife;
    }

    private void OnDisable() {
        LivesManager.OnPlayerLoseLife -= LivesManager_OnPlayerLoseLife;
    }

    private void LivesManager_OnPlayerLoseLife(object sender, int remainingLives) {
        switch (remainingLives) {
            case 0:
                live1.gameObject.SetActive(false);
                break;
            case 1:
                live2.gameObject.SetActive(false);
                break;
            case 2:
                live3.gameObject.SetActive(false);
                break;
        }
    }

    public void ResetLives() {
        live1.gameObject.SetActive(true);
        live2.gameObject.SetActive(true);
        live3.gameObject.SetActive(true);
    }
}
