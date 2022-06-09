using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Floor : BasicFloor
{
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
        floorData = new FloorData { floorNumber = 1, powerLevel = 1 };
        powerLevelText = transform.Find("levelIndicator").Find("levelIndicatorText").GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        powerLevelText.SetText(floorData.powerLevel.ToString());
    }

    private void BattleManager_OnPlayerWinBattle(object sender, BattleManager.OnPlayerWinBattleEventArgs e)
    {
        Debug.Log("Player win battle");
        floorData.powerLevel = e.newPlayerLevel;
        powerLevelText.SetText(floorData.powerLevel.ToString());
    }
}
