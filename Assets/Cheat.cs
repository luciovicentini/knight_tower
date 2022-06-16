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

        if (Keyboard.current[Key.R].wasPressedThisFrame)
        {
            RestartGame();
        }
    }

    private void DestroyTower()
    {
        if (enemyTower == null)
        {
            enemyTower = GameObject.FindObjectOfType<EnemyTower>();
        }
        enemyTower.InvokeEventTowerDefeated();
    }

    private void RestartGame()
    {
        GameManager.Instance.ResetGame();
    }
}
