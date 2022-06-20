using Cinemachine;
using UnityEngine;

public class CinemachineShake : MonoBehaviour {
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    private float shakingIntencity;

    private float timer;
    private float timerMax;

    private CinemachineVirtualCamera virtualCamera;
    public static CinemachineShake Instance { get; private set; }

    private void Awake() {
        Instance = this;

        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin =
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update() {
        if (timer < timerMax) {
            timer += Time.deltaTime;
            var amplitude = Mathf.Lerp(shakingIntencity, 0f, timer / timerMax);
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amplitude;
        }
    }

    public void ShakeCamera(float intencity, float timer) {
        timerMax = timer;
        shakingIntencity = intencity;
        this.timer = 0;
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intencity;
    }
}