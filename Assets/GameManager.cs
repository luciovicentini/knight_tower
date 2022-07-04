using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private int startingTowerLevel = 0;
    [SerializeField] public int nextTowerLevel;
    
    private EnemyTowerManager enemyTowerManager;
    private Floor playerFloor;

    private void Awake()
    {
        Instance = this;

        enemyTowerManager = GameObject.FindObjectOfType<EnemyTowerManager>();
        playerFloor = GameObject.FindObjectOfType<Floor>();
        nextTowerLevel = startingTowerLevel;
    }

    private void Start() {
        LivesManager.OnPlayerDie += LivesManager_OnPlayerDie;
    }

    private void LivesManager_OnPlayerDie(object sender, EventArgs e) {
        ResetGame();
    }

    public void ResetGame() {
        nextTowerLevel = startingTowerLevel;
        playerFloor.ResetPlayerFloor();
        enemyTowerManager.ResetTowerManager();
    }
}
