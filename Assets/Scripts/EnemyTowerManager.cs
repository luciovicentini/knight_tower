using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowerManager : MonoBehaviour {
    
    private EnemyTower currentTower;
    private Vector3 enemyTowerPosition;
    private Floor playerFloor;

    private Transform playerTower;
    
    
    private void Awake() {
        playerTower = GameObject.Find("player").transform;
        playerFloor = playerTower.GetComponentInChildren<Floor>();
    }

    private void Start() {
        var screenWorldPosition = UtilsClass.GetScreenWorldPosition();
        enemyTowerPosition =
            new Vector3(screenWorldPosition.x - playerTower.position.x * -1, playerTower.position.y, 0f);
        EnemyTower.OnTowerDefeated += EnemyTower_OnTowerDefeated;
        
        CreateNextTower();
        // currentTower = CreateNewTower(++currentFloorAmount, new FloorData(powerLevel: 4500000, levelSymbol: "CC"));
    }

    private void OnDisable() {
        EnemyTower.OnTowerDefeated -= EnemyTower_OnTowerDefeated;
    }

    private EnemyTower CreateNewTower(FloorData playerFloorData) {
        List<FloorData> floorDataList = FloorDataUtil.GetFloorPowerLevels(GameManager.Instance.nextTowerLevel, playerFloorData);
        FloorData bossData = FloorDataUtil.GetBossLevel(floorDataList, playerFloorData);
        EnemyTower enemyTower = EnemyTower.Create(enemyTowerPosition, floorDataList, bossData);
        
        return enemyTower;
    }

    private void EnemyTower_OnTowerDefeated(object sender, EventArgs e) {
        CreateNextTower();
    }

    private void CreateNextTower() {
        if (currentTower != null) Destroy(currentTower.gameObject);
        // TODO el update del player floor data deberia estar en su clase, no acá.
        var playerFloorFloorData = playerFloor.floorData;
        FloorDataUtil.UpdatePlayerLevel(ref playerFloorFloorData);
        playerFloor.SetPlayerLevel(playerFloorFloorData);
        currentTower = CreateNewTower(playerFloorFloorData);
    }

    public void ResetTowerManager() {
        CreateNextTower();
    }
}