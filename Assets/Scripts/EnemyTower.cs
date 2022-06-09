using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : MonoBehaviour
{
    public static EnemyTower Create(Vector3 position, List<FloorData> floorLevels)
    {
        Transform enemyTowerTransform = Instantiate(GameAssets.Instance.pfEnemyTower, position, Quaternion.identity);
        LogFloorLevels(floorLevels);
        foreach (FloorData floorData in floorLevels)
        {
            Debug.Log($"OnEnemyTowerCreate {floorData.ToString()}");
            Vector3 floorPosition = (floorData.floorNumber - 1) * new Vector3(0, 1f, 0);
            EnemyFloor floor = EnemyFloor.Create(enemyTowerTransform, floorPosition);
            floor.floorData = floorData;
        }

        enemyTowerTransform.Find("roof").localPosition = (floorLevels.Count * new Vector3(0f, 1f, 0f)) - new Vector3(0f, .4f, 0f);
        return enemyTowerTransform.GetComponent<EnemyTower>();
    }

    private static void LogFloorLevels(List<FloorData> floorDatas)
    {
        foreach (FloorData item in floorDatas)
        {
            Debug.Log($"[EnemyTower](Create) - FloorData = {item.ToString()}");
        }
    }

    public bool IsTowerDefeated()
    {
        EnemyFloor[] enemyFloorList = transform.GetComponentsInChildren<EnemyFloor>();

        foreach (var enemyFloor in enemyFloorList)
        {
            Debug.Log($"[EnemyTower](IsTowerDefeated) IsEnemyFloorDefeated? {enemyFloor.GetIsDefeated()}");
            if (!enemyFloor.GetIsDefeated())
            {
                return false;
            }
        }
        return true;
    }
}
