using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOptions : MonoBehaviour {
    
    private Button closeButton;
    private void Awake() {
        closeButton = transform.Find("optionWindow/closeBtn").GetComponent<Button>();
        closeButton.onClick.AddListener(Hide);
        
        Hide();
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
