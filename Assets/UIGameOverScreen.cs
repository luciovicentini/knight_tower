using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOverScreen : MonoBehaviour {
    
    private Button restartButton;

    private void Awake() {
        Debug.Log("UIGameOverScreen Awake");
        Hide();
        restartButton = transform.Find("restartButton").GetComponent<Button>();
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        LivesManager.OnPlayerDie += LivesManager_OnPlayerDie;

    }

    private void OnRestartButtonClicked() {
        Hide();
        GameManager.Instance.ResetGame();
    }

    private void OnDestroy() {
        LivesManager.OnPlayerDie -= LivesManager_OnPlayerDie;
    }

    private void LivesManager_OnPlayerDie(object sender, EventArgs e) {
        Debug.Log("Inside Event Observer LiveManager_OnPlayerDie");
        Show();
    }

    private void Show() {
        Debug.Log("ShowGameOverScreen()");
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
