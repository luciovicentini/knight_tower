using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private TextMeshPro powerLevelText;
    private int powerLevel;

    private void OnEnable()
    {
        BattleManager.Instance.OnPlayerWinBattle += BattleManager_OnPlayerWinBattle;
    }

    private void OnDisable()
    {
        BattleManager.Instance.OnPlayerWinBattle -= BattleManager_OnPlayerWinBattle;
    }

    private void Awake()
    {
        powerLevel = 1;
        powerLevelText = transform.Find("levelIndicator").Find("levelIndicatorText").GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        powerLevelText.SetText(powerLevel.ToString());
    }

    private void BattleManager_OnPlayerWinBattle(object sender, BattleManager.OnPlayerWinBattleEventArgs e)
    {
        Debug.Log("Player win battle");
        powerLevel = e.newPlayerLevel;
        powerLevelText.SetText(powerLevel.ToString());
    }

    public int GetPowerLevel() => powerLevel;

    public void SetPowerLevel(int level) => powerLevel = level;
}
