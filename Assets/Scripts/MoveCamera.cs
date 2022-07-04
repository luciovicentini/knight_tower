using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Screen = UnityEngine.Device.Screen;

public class MoveCamera : MonoBehaviour, IDrag {
    [SerializeField] private float draggingMarginY = 2f;
    
    private EnemyTower enemyTower;
    private Vector2 draggingPosition;
    private Vector2 startingPosition;
    private float moveSpeed = 5f;
    
    private bool isDragging;
    

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
        if (IsDraggingUp()) {
            MoveCameraUp();
        } else if (IsDraggingDown()) {
            MoveCameraDown();
        }
    }

    private void MoveCameraUp() {
        MoveCameraDir(Vector2.up);
    }

    private void MoveCameraDown() {
        MoveCameraDir(Vector2.down);
    }

    private void MoveCameraDir(Vector2 dir) {
        float y = transform.position.y + dir.y * moveSpeed * Time.deltaTime;
        float enemyRoofY = Mathf.Clamp(enemyTower.GetRoofWorldPosition().y, 0, float.PositiveInfinity);
        y = Mathf.Clamp(y, 0, enemyRoofY);
        transform.position = new Vector3(0, y,0);
    }

    private bool IsDraggingUp() {
        if (draggingPosition.y > UtilsClass.GetTopRightWorldCameraPosition().y - draggingMarginY) {
            return true;
        }

        return false;
    }

    private bool IsDraggingDown() {
        if (draggingPosition.y < UtilsClass.GetDownLeftWorldCameraPosition().y + draggingMarginY) {
            return true;
        }

        return false;
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
        ResetPosition();
    }

    private void BattleManager_OnPlayerWinBattle(object sender, BattleManager.OnPlayerWinBattleEventArgs e) {
        ResetPosition();
    }

    private void ResetPosition() {
        transform.position = startingPosition;
    }
}