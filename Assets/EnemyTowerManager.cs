using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTowerManager : MonoBehaviour
{

    private void Awake()
    {
        Transform playerTower = GameObject.Find("player").transform;
        Vector3 screenWorldPosition = UtilsClass.GetScreenWorldPosition();
        Vector3 towerPosition = new Vector3(screenWorldPosition.x - (playerTower.position.x * -1), playerTower.position.y, 0f);
        
        InstanciateNewTower(towerPosition);
    }

    private void InstanciateNewTower(Vector3 position)
    {
        Tower.Create(position, GetFloorPowerLevels(4));
    }

    private List<int> GetFloorPowerLevels(int floorCount, int playerLevel = 1)
    {
        List<int> floorPowerLevels = new List<int>();
        floorPowerLevels.Add ( playerLevel == 1 ? 1 : playerLevel - 1);

        for (int i = 1; i < floorCount; i++)
        {
            int lastPowerLevel = floorPowerLevels[i - 1];
            floorPowerLevels.Add(lastPowerLevel + Random.Range(1, lastPowerLevel + 1));
        }
        return floorPowerLevels;
    }
}
