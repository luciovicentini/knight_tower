using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Floor : BasicFloor
{
    private void Awake()
    {
        floorData = new FloorData { floorNumber = 1, powerLevel = 1 };
        levelIndicator = GetComponentInChildren<LevelIndicator>();
    }

    private void OnEnable()
    {
        BattleManager.Instance.OnPlayerWinBattle += BattleManager_OnPlayerWinBattle;
    }

    private void OnDisable()
    {
        BattleManager.Instance.OnPlayerWinBattle -= BattleManager_OnPlayerWinBattle;
    }

    private void Start()
    {
        levelIndicator.SetPowerLevelText(floorData.powerLevel.ToString());
    }

    private void BattleManager_OnPlayerWinBattle(object sender, BattleManager.OnPlayerWinBattleEventArgs e)
    {
        floorData.powerLevel = e.newPlayerLevel;
        levelIndicator.SetPowerLevelText(floorData.powerLevel.ToString());
    }
}
