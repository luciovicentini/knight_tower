using System;
using UnityEngine;

public class CameraConfiner : MonoBehaviour {
    private void Awake() {
        EnemyTower.OnCreateEnemyTower += EnemyTower_OnCreateEnemyTower;
    }

    private void OnDisable() {
        EnemyTower.OnCreateEnemyTower -= EnemyTower_OnCreateEnemyTower;
    }

    private void EnemyTower_OnCreateEnemyTower(object sender, EventArgs e) {
        int floorAmount = ((EnemyTower)sender).GetFloorAmount();

        float scaleY = 5f;
        float positionY = 0f;

        if (floorAmount > 8) {
            scaleY = 12 * (float)floorAmount / 20 + 1;
            positionY = scaleY - 5;
        }

        transform.position = new Vector2(transform.position.x, positionY);
        transform.localScale = new Vector2(transform.localScale.x, scaleY);
    }
}