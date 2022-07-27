using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowerManager : MonoBehaviour {
    
    private EnemyTower currentTower;
    private Floor playerFloor;

    private Transform playerTower;
    
    
    private void Awake() {
        playerTower = GameObject.Find("player").transform;
        playerFloor = playerTower.GetComponentInChildren<Floor>();
    }

    private void Start() {
        EnemyTower.OnTowerDefeated += EnemyTower_OnTowerDefeated;
        
        CreateNextTower();
        // currentTower = CreateNewTower(++currentFloorAmount, new FloorData(powerLevel: 4500000, levelSymbol: "CC"));
    }

    private void OnDisable() {
        EnemyTower.OnTowerDefeated -= EnemyTower_OnTowerDefeated;
    }

    private EnemyTower CreateNewTower() {
        FloorData playerFloorData = FloorDataUtil.UpdatePlayerLevel(playerFloor.floorData);
        playerFloor.SetPlayerLevel(playerFloorData);
        List<FloorData> floorDataList = FloorDataUtil.GetFloorPowerLevels(GameManager.Instance.nextTowerLevel, playerFloorData);
        FloorData bossData = FloorDataUtil.GetBossLevel(floorDataList, playerFloorData);
        EnemyTower enemyTower = EnemyTower.Create(floorDataList, bossData);
        
        return enemyTower;
    }

    private void EnemyTower_OnTowerDefeated(object sender, EventArgs e) {
        CreateNextTower();
    }

    private void CreateNextTower() {
        if (currentTower != null) Destroy(currentTower.gameObject);
        currentTower = CreateNewTower();
    }

    public void ResetTowerManager() {
        CreateNextTower();
    }
}