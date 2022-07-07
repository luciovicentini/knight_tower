using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private float volume = .4f;

    private AudioSource audioSource;

    private void Awake() {
        volume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, .5f);

        audioSource = GetComponent<AudioSource>();
        UpdateAudioSourceVolume();
    }

    public void IncreaseVolume() {
        volume += .1f;
        volume = Mathf.Clamp01(volume);

        UpdateAudioSourceVolume();
        UpdatePlayerPrefs();
    }

    public void DecreaseVolume() {
        volume -= .1f;
        volume = Mathf.Clamp01(volume);
        
        UpdateAudioSourceVolume();
        UpdatePlayerPrefs();
    }

    private void UpdateAudioSourceVolume() {
        audioSource.volume = volume;
    }

    private void UpdatePlayerPrefs() {
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
    }

    public float GetVolume() => volume;
}
