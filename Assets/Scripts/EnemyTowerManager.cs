using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTowerManager : MonoBehaviour
{

    [SerializeField]
    private int startingFloorAmount = 4;

    private Transform playerTower;
    private Vector3 enemyTowerPosition;
    private int currentFloorAmount;
    private EnemyTower currentTower;

    private void Awake()
    {
        playerTower = GameObject.Find("player").transform;
        currentFloorAmount = startingFloorAmount;
    }

    private void Start()
    {
        Vector3 screenWorldPosition = UtilsClass.GetScreenWorldPosition();
        enemyTowerPosition = new Vector3(screenWorldPosition.x - (playerTower.position.x * -1), playerTower.position.y, 0f);
        currentTower = CreateNewTower();
    }

    private EnemyTower CreateNewTower(int playerLevel = 1)
    {
        List<FloorData> floorDataList = FloorData.GetFloorPowerLevels(currentFloorAmount, playerLevel);
        int bossLevel = FloorData.GetBossLevel(floorDataList, playerLevel);
        EnemyTower enemyTower = EnemyTower.Create(enemyTowerPosition, floorDataList, bossLevel);
        enemyTower.OnTowerDefeated += EnemyTower_OnTowerDefeated;
        return enemyTower;
    }

    private void OnDisable()
    {
        currentTower.OnTowerDefeated -= EnemyTower_OnTowerDefeated;
    }

    private void EnemyTower_OnTowerDefeated(object sender, EventArgs e)
    {
        Debug.Log($"(EnemyTowerManager) - BattleManager_OnPlayerWinBattle:CurrentTower= {currentTower}");

        Destroy(currentTower.gameObject);
        currentFloorAmount++;
        currentTower = CreateNewTower(playerTower.GetComponentInChildren<Floor>().floorData.powerLevel);
    }

}
