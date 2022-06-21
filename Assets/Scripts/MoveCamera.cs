using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour, IDrag {
    private EnemyTower enemyTower;
    private Vector2 draggingPosition;
    private bool isDragging;
    private Vector2 startingPosition;
    private float moveSpeed = 5f;

    private void Awake() {
        BattleManager.Instance.OnPlayerWinBattle += BattleManager_OnPlayerWinBattle;
        BattleManager.Instance.OnEnemyWinBattle += BattleManager_OnEnemyWinBattle;
        EnemyTower.OnCreateEnemyTower += EnemyTower_OnCreateEnemyTower;
    }

    private void Start() {
        startingPosition = transform.position;
        
    }

    private void OnDisable() {
        BattleManager.Instance.OnPlayerWinBattle -= BattleManager_OnPlayerWinBattle;
        BattleManager.Instance.OnEnemyWinBattle -= BattleManager_OnEnemyWinBattle;
        EnemyTower.OnCreateEnemyTower -= EnemyTower_OnCreateEnemyTower;
    }

    private void EnemyTower_OnCreateEnemyTower(object sender, EventArgs e) {
        enemyTower = (EnemyTower)sender;
    }

    private void Update() {
        if (!isDragging) return;
        
        Vector3 dir = ((Vector3)draggingPosition - transform.position).normalized;
        float y = transform.position.y + dir.y * moveSpeed * Time.deltaTime;
        y = Mathf.Clamp(y, 0, enemyTower.GetRoofWorldPosition().y);
        transform.position = new Vector3(0,y,0);
    }

    public void OnDragEnd() {
        isDragging = false;
    }

    public void OnDragging(Vector2 position) {
        draggingPosition = position;
    }

    public void OnDragStart(Vector2 position) {
        isDragging = true;
    }

    private void BattleManager_OnEnemyWinBattle(object sender, EventArgs e) {
        // ResetPosition();
    }

    private void BattleManager_OnPlayerWinBattle(object sender, BattleManager.OnPlayerWinBattleEventArgs e) {
        // ResetPosition();
    }

    private void ResetPosition() {
        transform.position = startingPosition;
    }
}