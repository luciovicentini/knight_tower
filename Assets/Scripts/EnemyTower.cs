using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : MonoBehaviour {
    
    public static event EventHandler OnTowerDefeated;
    public static event EventHandler OnAllFloorsDefeated;
    public static event EventHandler OnCreateEnemyTower;
    
    
    public static EnemyTower Create(Vector3 position, List<FloorData> floorLevels, FloorData bossData) {
        GameManager.Instance.nextTowerLevel++;
        var enemyTowerTransform = Instantiate(GameAssets.Instance.pfEnemyTower, position, Quaternion.identity);

        var floors = new EnemyFloor[floorLevels.Count];

        var index = 0;
        foreach (var floorData in floorLevels) {
            var floorPosition = (floorData.floorNumber - 1) * new Vector3(0, 1f, 0);
            var floor = EnemyFloor.Create(enemyTowerTransform, floorPosition);
            floor.floorData = floorData;
            floors[index] = floor;
            index++;
        }

        var enemyTower = enemyTowerTransform.GetComponent<EnemyTower>();
        
        enemyTower.enemyRoof = enemyTowerTransform.Find("roof").GetComponent<EnemyRoof>();
        enemyTower.enemyRoof.transform.localPosition = floorLevels.Count * new Vector3(0f, 1f, 0f) - new Vector3(0f, .4f, 0f);
        
        SetUpEnemyTower(enemyTower, bossData, floors);
        OnCreateEnemyTower?.Invoke(enemyTower, EventArgs.Empty);
        return enemyTower;
    }
    
    private static void SetUpEnemyTower(EnemyTower enemyTower, FloorData bossData, EnemyFloor[] floors) {
        enemyTower.floors = floors;
        if (ShouldShowBoss()) {
            enemyTower.enemyBoss = EnemyBoss.Create(enemyTower.enemyRoof, bossData);
        }
    }
    
    private static bool ShouldShowBoss() {
        return (GameManager.Instance.nextTowerLevel-1) % 5 == 0;
    }
    
    private EnemyBoss enemyBoss;
    private EnemyFloor[] floors;
    private EnemyRoof enemyRoof;

    private void Start() {
        for (var i = 0; i < floors.Length; i++) {
            floors[i].OnFloorDefeated += EnemyFloor_OnFloorDefeated;
        }
        BattleManager.OnPlayerWinBossBattle += BattleManager_OnPlayerWinBossBattle;
    }

    private void OnDisable() {
        BattleManager.OnPlayerWinBossBattle -= BattleManager_OnPlayerWinBossBattle;
    }

    private void EnemyFloor_OnFloorDefeated(object sender, EventArgs e) {
        if (IsTowerDefeated()) {
            if (ShouldShowBoss()) {
                OnAllFloorsDefeated?.Invoke(this, EventArgs.Empty);
            }
            else {
                OnTowerDefeated?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private bool IsTowerDefeated() {
        foreach (var enemyFloor in floors)
            if (!enemyFloor.IsDefeated())
                return false;
        return true;
    }

    private void BattleManager_OnPlayerWinBossBattle(object sender, EventArgs e) {
        OnTowerDefeated?.Invoke(this, EventArgs.Empty);
    }
    
    public void InvokeEventTowerDefeated() {
        OnTowerDefeated?.Invoke(this, EventArgs.Empty);
    }
    public int GetFloorAmount() => floors.Length;

    public Vector3 GetRoofWorldPosition() => transform.position + enemyRoof.transform.localPosition;
}