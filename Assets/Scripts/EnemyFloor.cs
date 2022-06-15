using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFloor : BasicFloor
{
    public event EventHandler OnFloorDefeated;

    public static EnemyFloor Create(Transform parent, Vector3 position)
    {
        Transform floorTransform = Instantiate(GameAssets.Instance.pfEnemyFloor, Vector3.zero, Quaternion.identity, parent);
        floorTransform.localPosition = position;

        EnemyFloor enemyFloor = floorTransform.GetComponent<EnemyFloor>();
        return enemyFloor;
    }

    private void Awake()
    {
        levelIndicator = GetComponentInChildren<LevelIndicator>();
    }

    private void OnDisable()
    {
        BattleManager.Instance.OnPlayerWinBattle -= BattleManager_OnPlayerWinBattle;
    }

    private void BattleManager_OnPlayerWinBattle(object sender, BattleManager.OnPlayerWinBattleEventArgs e)
    {
        if (e.floorData.floorNumber != this.floorData.floorNumber) return;
        floorData.powerLevel = 0;
        UpdateText();
        GetComponent<BoxCollider2D>().enabled = false;
        if (IsDefeated())
        {
            OnFloorDefeated?.Invoke(this, EventArgs.Empty);
        }
    }

    void Start()
    {
        BattleManager.Instance.OnPlayerWinBattle += BattleManager_OnPlayerWinBattle;
        UpdateText();
    }

    private void UpdateText()
    {
        levelIndicator.SetPowerLevelText(floorData.powerLevel.ToString());
    }

    public bool IsDefeated() => floorData.powerLevel <= 0;
}
