using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
        return EnemyTower.Create(enemyTowerPosition, FloorData.GetFloorPowerLevels(currentFloorAmount, playerLevel));
    }

    private void OnEnable()
    {
        BattleManager.Instance.OnPlayerWinBattle += BattleManager_OnPlayerWinBattle;
    }

    private void BattleManager_OnPlayerWinBattle(object sender, BattleManager.OnPlayerWinBattleEventArgs e)
    {
        Debug.Log($"(EnemyTowerManager) - BattleManager_OnPlayerWinBattle:CurrentTower= {currentTower}");
        if (currentTower.IsTowerDefeated())
        {
            Destroy(currentTower.gameObject);
            currentFloorAmount++;
            currentTower = CreateNewTower(playerTower.GetComponentInChildren<Floor>().floorData.powerLevel);
        }
    }


}
