using UnityEngine;
using UnityEngine.InputSystem;

public class Cheat : MonoBehaviour {
    private EnemyTower enemyTower;

    private void Update() {
        if (Keyboard.current[Key.T].wasPressedThisFrame) DestroyTower();

        if (Keyboard.current[Key.R].wasPressedThisFrame) RestartGame();
    }

    private void DestroyTower() {
        if (null == enemyTower) enemyTower = FindObjectOfType<EnemyTower>();
        enemyTower.InvokeEventTowerDefeated();
    }

    private void RestartGame() {
        GameManager.Instance.ResetGame();
    }
}