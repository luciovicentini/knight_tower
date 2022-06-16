using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTowerManager : MonoBehaviour
{

    [SerializeField]
    private int currentFloorAmount = 4;

    private Transform playerTower;
    private Vector3 enemyTowerPosition;
    private EnemyTower currentTower;

    private void Awake()
    {
        playerTower = GameObject.Find("player").transform;
    }

    private void Start()
    {
        Vector3 screenWorldPosition = UtilsClass.GetScreenWorldPosition();
        enemyTowerPosition = new Vector3(screenWorldPosition.x - (playerTower.position.x * -1), playerTower.position.y, 0f);
        currentTower = CreateNewTower(++currentFloorAmount);
    }

    private EnemyTower CreateNewTower(int floorAmount, int playerLevel = 1)
    {
        List<FloorData> floorDataList = FloorData.GetFloorPowerLevels(floorAmount, playerLevel);
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
        CreateNextTower();
    }

    private void CreateNextTower()
    {
        Destroy(currentTower.gameObject);
        currentTower = CreateNewTower(++currentFloorAmount, playerTower.GetComponentInChildren<Floor>().floorData.powerLevel);
    }

    public void ResetTowerManager()
    {
        currentFloorAmount = 0;
        CreateNextTower();
    }

}
