using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    private float timer;
    private float timerMax;
    private float shakingIntencity;

    private void Awake()
    {
        Instance = this;

        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (timer < timerMax)
        {
            timer += Time.deltaTime;
            float amplitude = Mathf.Lerp(shakingIntencity, 0f, timer / timerMax);
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amplitude;
        }
    }

    public void ShakeCamera(float intencity, float timer)
    {
        this.timerMax = timer;
        this.shakingIntencity = intencity;
        this.timer = 0;
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intencity;
    }
}
