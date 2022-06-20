using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITowerLevel : MonoBehaviour {
    private TextMeshProUGUI towerLevelText;
    private string towerLevelString = "Level: ";
    
    private void Awake() {
        towerLevelText = transform.Find("towerLevelText").GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable() {
        EnemyTower.OnCreateEnemyTower += EnemyTowerManager_OnNewEnemyTowerCreated;
    }

    private void OnDisable() {
        EnemyTower.OnCreateEnemyTower -= EnemyTowerManager_OnNewEnemyTowerCreated;
    }

    private void EnemyTowerManager_OnNewEnemyTowerCreated(object sender, EventArgs e) {
        towerLevelText.SetText(towerLevelString + (GameManager.Instance.nextTowerLevel - 1));
    }
}
