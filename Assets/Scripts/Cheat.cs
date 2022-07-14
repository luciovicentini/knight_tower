using UnityEngine;
using UnityEngine.InputSystem;

public class Cheat : MonoBehaviour {
    private EnemyTower enemyTower;

    private void Update() {
        if (Keyboard.current[Key.T].wasPressedThisFrame) DestroyTower();

        if (Keyboard.current[Key.R].wasPressedThisFrame) RestartGame();
        
        if (Keyboard.current[Key.F].wasPressedThisFrame) DestroyFloor();
    }

    private void DestroyTower() {
        if (null == enemyTower) enemyTower = FindObjectOfType<EnemyTower>();
        enemyTower.InvokeEventTowerDefeated();
    }

    private void DestroyFloor() {
        if (null == enemyTower) enemyTower = FindObjectOfType<EnemyTower>();
        EnemyFloor[] enemyFloors = enemyTower.GetFloors();
        Floor playerFloor = FindObjectOfType<Floor>();
        foreach (EnemyFloor enemyFloor in enemyFloors) {
            if (enemyFloor.IsDefeated()) continue;
            if (playerFloor.floorData.powerLevel >= enemyFloor.floorData.powerLevel) {
                BattleManager.Instance.Resolve(playerFloor.floorData.powerLevel, enemyFloor.floorData);
                break;
            }
        }
    }

    private void RestartGame() {
        GameManager.Instance.ResetGame();
    }
}