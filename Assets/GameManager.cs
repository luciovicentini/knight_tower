using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    private EnemyTowerManager enemyTowerManager;
    private Floor playerFloor;

    private void Awake()
    {
        Instance = this;

        enemyTowerManager = GameObject.FindObjectOfType<EnemyTowerManager>();
        playerFloor = GameObject.FindObjectOfType<Floor>();
    }

    public void ResetGame()
    {
        playerFloor.ResetPlayerFloor();
        enemyTowerManager.ResetTowerManager();
    }
}
