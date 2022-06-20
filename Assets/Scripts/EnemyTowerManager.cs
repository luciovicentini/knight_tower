using System;
using UnityEngine;

public class EnemyTowerManager : MonoBehaviour {
    [SerializeField] private int currentFloorAmount = 4;

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

        CreateNextTower();
        // currentTower = CreateNewTower(++currentFloorAmount, new FloorData(powerLevel: 4500000, levelSymbol: "CC"));
    }

    private void OnDisable() {
        currentTower.OnTowerDefeated -= EnemyTower_OnTowerDefeated;
    }

    private EnemyTower CreateNewTower(int floorAmount, FloorData playerFloorData) {
        FloorDataUtil.UpdatePlayerLevel(ref playerFloorData);
        playerFloor.SetPlayerLevel(playerFloorData);
        var floorDataList = FloorDataUtil.GetFloorPowerLevels(floorAmount, playerFloorData);
        var bossData = FloorDataUtil.GetBossLevel(floorDataList, playerFloorData);
        var enemyTower = EnemyTower.Create(enemyTowerPosition, floorDataList, bossData);
        enemyTower.OnTowerDefeated += EnemyTower_OnTowerDefeated;
        return enemyTower;
    }

    private void EnemyTower_OnTowerDefeated(object sender, EventArgs e) {
        CreateNextTower();
    }

    private void CreateNextTower() {
        if (currentTower != null) Destroy(currentTower.gameObject);
        currentTower = CreateNewTower(++currentFloorAmount, playerTower.GetComponentInChildren<Floor>().floorData);
    }

    public void ResetTowerManager() {
        currentFloorAmount = 0;
        CreateNextTower();
    }
}