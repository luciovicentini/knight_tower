using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public static Tower Create(Vector3 position, List<int> floorLevels)
    {
        Transform towerTransform = Instantiate(GameAssets.Instance.pfEnemyTower, position, Quaternion.identity);
        
        foreach (int level in floorLevels)
        {
            int floorNumber = floorLevels.IndexOf(level);
            Vector3 floorPosition = floorNumber * new Vector3(0, 1f, 0);
            EnemyFloor floor = EnemyFloor.Create(towerTransform, floorPosition);
            floor.SetPowerLevel(level);
        }

        towerTransform.Find("roof").localPosition = (floorLevels.Count * new Vector3(0f, 1f, 0f)) - new Vector3(0f, .4f, 0f);
        Tower tower = towerTransform.GetComponent<Tower>();
        return tower;
    }


}