using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : MonoBehaviour
{
    public event EventHandler OnTowerDefeated;
    public static event EventHandler OnAllFloorsDefeated;
    public static event EventHandler OnCreateEnemyTower;

    private EnemyFloor[] floors;
    private EnemyBoss enemyBoss;

    public static EnemyTower Create(Vector3 position, List<FloorData> floorLevels, int bossLevel)
    {
        Transform enemyTowerTransform = Instantiate(GameAssets.Instance.pfEnemyTower, position, Quaternion.identity);

        EnemyFloor[] floors = new EnemyFloor[floorLevels.Count];

        int index = 0;
        foreach (FloorData floorData in floorLevels)
        {
            Vector3 floorPosition = (floorData.floorNumber - 1) * new Vector3(0, 1f, 0);
            EnemyFloor floor = EnemyFloor.Create(enemyTowerTransform, floorPosition);
            floor.floorData = floorData;
            floors[index] = floor;
            index++;
        }

        Transform roofTransform = enemyTowerTransform.Find("roof");
        roofTransform.localPosition = (floorLevels.Count * new Vector3(0f, 1f, 0f)) - new Vector3(0f, .4f, 0f);
        EnemyTower enemyTower = enemyTowerTransform.GetComponent<EnemyTower>();
        SetUpEnemyTower(enemyTower, bossLevel, floors, roofTransform);

        return enemyTower;
    }

    internal void InvokeEventTowerDefeated()
    {
        OnTowerDefeated?.Invoke(this, EventArgs.Empty);
    }

    private static void SetUpEnemyTower(EnemyTower enemyTower,
                                        int bossLevel,
                                        EnemyFloor[] floors,
                                        Transform roofTransform)
    {
        enemyTower.floors = floors;
        enemyTower.enemyBoss = roofTransform.Find("pfEnemyBoss").GetComponent<EnemyBoss>();
        enemyTower.enemyBoss.SetLevel(bossLevel);
        OnCreateEnemyTower?.Invoke(enemyTower, EventArgs.Empty);
    }

    private void Start()
    {
        for (int i = 0; i < floors.Length; i++)
        {
            floors[i].OnFloorDefeated += EnemyFloor_OnFloorDefeated;
        }
        BattleManager.OnPlayerWinBossBattle += BattleManager_OnPlayerWinBossBattle;
    }

    private void OnDisable()
    {
        BattleManager.OnPlayerWinBossBattle -= BattleManager_OnPlayerWinBossBattle;
    }

    private void EnemyFloor_OnFloorDefeated(object sender, EventArgs e)
    {
        if (IsTowerDefeated())
        {
            OnAllFloorsDefeated?.Invoke(this, EventArgs.Empty);
        }
    }

    private bool IsTowerDefeated()
    {
        foreach (var enemyFloor in floors)
        {
            if (!enemyFloor.IsDefeated())
            {
                return false;
            }
        }
        return true;
    }

    private void BattleManager_OnPlayerWinBossBattle(object sender, EventArgs e)
    {
        OnTowerDefeated?.Invoke(this, EventArgs.Empty);
    }

    public int GetFloorAmount() => floors.Length;
}
