using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowerManager : MonoBehaviour
{
    [SerializeField]
    private int startingFloorAmount = 2;

    private Transform playerTower;
    private Vector3 enemyTowerPosition;
    private int currentFloorAmount;
    private EnemyTower currentTower;

    private void Awake()
    {
        playerTower = GameObject.Find("player").transform;
        Vector3 screenWorldPosition = UtilsClass.GetScreenWorldPosition();
        enemyTowerPosition = new Vector3(screenWorldPosition.x - (playerTower.position.x * -1), playerTower.position.y, 0f);
        currentFloorAmount = startingFloorAmount;
        currentTower = CreateNewTower();
    }

    private EnemyTower CreateNewTower(int playerLevel = 1)
    {
        EnemyTower enemyTower = EnemyTower.Create(enemyTowerPosition, FloorData.GetFloorPowerLevels(currentFloorAmount, playerLevel));
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
