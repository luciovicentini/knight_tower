using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Floor : BasicFloor
{

    private void Awake()
    {
        SetStartFloorData();
        levelIndicator = GetComponentInChildren<LevelIndicator>();
    }

    private void SetStartFloorData()
    {
        floorData = new FloorData { floorNumber = 1, powerLevel = 1 };
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
        UpdateLevelIndicatorString();
    }

    private void BattleManager_OnPlayerWinBattle(object sender, BattleManager.OnPlayerWinBattleEventArgs e)
    {
        floorData.powerLevel = e.newPlayerLevel;
        UpdateLevelIndicatorString();
    }

    private void UpdateLevelIndicatorString()
    {
        levelIndicator.SetPowerLevelText(floorData.powerLevel.ToString());
    }

    public void ResetPlayerFloor()
    {
        SetStartFloorData();
        UpdateLevelIndicatorString();
    }
}
