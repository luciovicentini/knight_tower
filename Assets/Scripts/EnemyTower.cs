using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : MonoBehaviour
{
    public event EventHandler OnTowerDefeated;

    private EnemyFloor[] floors;

    public static EnemyTower Create(Vector3 position, List<FloorData> floorLevels)
    {
        Transform enemyTowerTransform = Instantiate(GameAssets.Instance.pfEnemyTower, position, Quaternion.identity);

        EnemyFloor[] floors = new EnemyFloor[floorLevels.Count];

        int index = 0;
        foreach (FloorData floorData in floorLevels)
        {
            Vector3 floorPosition = (floorData.floorNumber - 1) * new Vector3(0, 1f, 0);
            EnemyFloor floor = EnemyFloor.Create(enemyTowerTransform, floorPosition);
            floor.floorData = floorData;
            floors[index] = floor;
            index++;
        }

        enemyTowerTransform.Find("roof").localPosition = (floorLevels.Count * new Vector3(0f, 1f, 0f)) - new Vector3(0f, .4f, 0f);
        EnemyTower enemyTower = enemyTowerTransform.GetComponent<EnemyTower>();
        enemyTower.floors = floors;
        return enemyTower;
    }

    private void Start()
    {
        for (int i = 0; i < floors.Length; i++)
        {
            floors[i].OnFloorDefeated += EnemyFloor_OnFloorDefeated;
        }

    }

    private void EnemyFloor_OnFloorDefeated(object sender, EventArgs e)
    {
        if (IsTowerDefeated())
        {
            OnTowerDefeated?.Invoke(this, EventArgs.Empty);
        }
    }

    private bool IsTowerDefeated()
    {
        foreach (var enemyFloor in floors)
        {
            if (!enemyFloor.IsDefeated())
            {
                return false;
            }
        }
        return true;
    }
}
