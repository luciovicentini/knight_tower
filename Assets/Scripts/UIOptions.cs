using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIOptions : MonoBehaviour {

    [SerializeField] private SoundManager soundManager;
    [SerializeField] private MusicManager musicManager;
    
    private Button closeButton;
    private Button soundIncreaseBtn;
    private TextMeshProUGUI soundVolumeText;
    private Button soundDecreaseBtn;
    
    private TextMeshProUGUI musicVolumeText;
    
    private void Awake() {
        closeButton = transform.Find("optionWindow/closeBtn").GetComponent<Button>();
        
        soundVolumeText = transform.Find("optionWindow/soundSection/soundVolumeText").GetComponent<TextMeshProUGUI>();
        musicVolumeText = transform.Find("optionWindow/musicSection/musicVolumeText").GetComponent<TextMeshProUGUI>();
        
        closeButton.onClick.AddListener(Hide);
        
        SetUpButtons();
    }

    private void SetUpButtons() {
        transform.Find("optionWindow/soundSection/soundIncreaseBtn").GetComponent<Button>().onClick
            .AddListener(() => Debug.Log("SoundIncreaseBtn Clicked!"));
        transform.Find("optionWindow/soundSection/soundDecreaseBtn").GetComponent<Button>().onClick
            .AddListener(() => Debug.Log("SoundDecreaseBtn Clicked!"));

        transform.Find("optionWindow/musicSection/musicIncreaseBtn").GetComponent<Button>()
            .onClick.AddListener(() => {
                musicManager.IncreaseVolume();
                UpdateText();
            });
        transform.Find("optionWindow/musicSection/musicDecreaseBtn").GetComponent<Button>()
            .onClick.AddListener(() => {
                musicManager.DecreaseVolume();
                UpdateText();
            });
        transform.Find("optionWindow/restartButton").GetComponent<Button>()
            .onClick.AddListener(() => {
                GameManager.Instance.ResetGame();
                Hide();
            });
    }

    private void Start() {
        UpdateText();
        Hide();
    }

    private void UpdateText() {
        soundVolumeText.SetText("10");
        musicVolumeText.SetText(Mathf.RoundToInt(musicManager.GetVolume() * 10).ToString());
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
