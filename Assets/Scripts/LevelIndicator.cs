using TMPro;
using UnityEngine;

public class LevelIndicator : MonoBehaviour {
    private TextMeshPro powerLevelText;

    private void Awake() {
        powerLevelText = GetComponentInChildren<TextMeshPro>();
    }

    public void SetPowerLevelText(string powerLevel) {
        powerLevelText.SetText(powerLevel);
    }
}