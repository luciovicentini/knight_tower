using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cheat : MonoBehaviour
{
    private EnemyTower enemyTower;

    void Update()
    {
        if (Keyboard.current[Key.T].wasPressedThisFrame)
        {
            DestroyTower();
        }
    }

    private void DestroyTower()
    {
        if (enemyTower == null)
        {
            enemyTower = GameObject.FindObjectOfType<EnemyTower>();
        }
        enemyTower.InvokeEventTowerDefeated();

        /* EnemyTowerManager enemyTowerManager = GameObject.FindObjectOfType<EnemyTowerManager>();
        Floor playerFloor = GameObject.Find("player").GetComponentInChildren<Floor>();
        Destroy(enemyTower.gameObject);
        enemyTower = enemyTowerManager.CreateNewTower(playerFloor.floorData.powerLevel); */
    }
}
