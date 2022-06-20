using System;
using UnityEngine;

public class MoveCamera : MonoBehaviour, IDrag {
    private Transform cameraConfiner;
    private Vector2 draggingPosition;
    private bool isDragging;
    private Vector2 startingPosition;

    private void Awake() {
        cameraConfiner = GameObject.Find("/CameraHandler/CameraConfiner")?.transform;
    }

    private void Start() {
        startingPosition = transform.position;
        BattleManager.Instance.OnPlayerWinBattle += BattleManager_OnPlayerWinBattle;
        BattleManager.Instance.OnEnemyWinBattle += BattleManager_OnEnemyWinBattle;
    }

    private void Update() {
        if (isDragging) {
            var dir = (Vector3)draggingPosition - transform.position;
            var y = transform.position.y + dir.y * 1f * Time.deltaTime;
            y = Mathf.Clamp(y, 0, cameraConfiner.localScale.y - 4);
            transform.position = new Vector2(transform.position.x, y);
        }
    }

    private void OnDisable() {
        BattleManager.Instance.OnPlayerWinBattle -= BattleManager_OnPlayerWinBattle;
        BattleManager.Instance.OnEnemyWinBattle -= BattleManager_OnEnemyWinBattle;
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