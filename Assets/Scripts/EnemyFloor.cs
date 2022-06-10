using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private void OnEnable()
    {
        BattleManager.Instance.OnPlayerWinBattle += BattleManager_OnPlayerWinBattle;
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

    private void Awake()
    {
        powerLevelText = transform.Find("levelIndicator").Find("levelIndicatorText").GetComponent<TextMeshPro>();
    }

    void Start()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        powerLevelText.SetText(floorData.powerLevel.ToString());
    }

    public bool IsDefeated() => floorData.powerLevel <= 0;
}
