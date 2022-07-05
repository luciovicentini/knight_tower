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
    private LivesManager livesManager;
    private UILivesHolder uiLivesHolder;
    private InputManager inputManager;

    private void Awake()
    {
        Instance = this;

        enemyTowerManager = FindObjectOfType<EnemyTowerManager>();
        playerFloor = FindObjectOfType<Floor>();
        livesManager = FindObjectOfType<LivesManager>();
        uiLivesHolder = FindObjectOfType<UILivesHolder>();
        inputManager = FindObjectOfType<InputManager>();
        
        ResetTowerLevel();
    }

    public void ResetGame() {
        ResetTowerLevel();
        playerFloor.ResetPlayerFloor();
        enemyTowerManager.ResetTowerManager();
        livesManager.ResetLives();
        uiLivesHolder.ResetLives();
        inputManager.Activate();
    }

    private void ResetTowerLevel() {
        nextTowerLevel = startingTowerLevel;
    }
}
