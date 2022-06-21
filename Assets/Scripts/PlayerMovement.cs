using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDrag {
    private Vector2 draggingPosition;
    private EnemyFloor enemyFloorAttacked;
    private EnemyRoof enemyRoofAttacked;

    private readonly float floorDetectionRadius = .3f;
    private bool IsDragging;
    private Vector2 startPosition;
    private KnightSelector knightSelector;

    private void Awake() {
        startPosition = transform.position;
        draggingPosition = transform.position;
        knightSelector = transform.GetComponent<KnightSelector>();
    }

    private void Start() {
        BattleManager.Instance.OnPlayerWinBattle += BattleManager_OnPlayerWinBattle;
        BattleManager.Instance.OnEnemyWinBattle += BattleManager_OnEnemyWinBattle;
        
    }

    private void Update() {
        if (IsDragging) transform.position = draggingPosition;
    }

    private void OnDisable() {
        BattleManager.Instance.OnPlayerWinBattle -= BattleManager_OnPlayerWinBattle;
        BattleManager.Instance.OnEnemyWinBattle += BattleManager_OnEnemyWinBattle;
        
    }

    public void OnDragEnd() {
        if (!IsDragging) return;
        CheckFloorIsAttacked();
        CheckRoofIsAttacked();
        MovePlayer();
        CheckIsPlayerSelected();
        SendPlayerIsAttackingEvent();
        IsDragging = false;
    }

    private void CheckIsPlayerSelected() {
        if (Vector2.Distance(startPosition, draggingPosition) < 0.1f) {
            knightSelector.ToggleSelection();
        }
    }

    public void OnDragging(Vector2 position) {
        draggingPosition = position;
    }


    public void OnDragStart(Vector2 position) {
        draggingPosition = position;
        IsDragging = true;
    }

    public event EventHandler<OnPlayerAttackEnemyFloorEventArgs> OnPlayerAttackEnemyFloor;
    public class OnPlayerAttackEnemyFloorEventArgs : EventArgs {
        public EnemyFloor enemyFloor;
    }
    
    public static event EventHandler<EnemyRoof> OnPlayerAttackEnemyRoof;

    private void CheckFloorIsAttacked() {
        var colliders = Physics2D.OverlapCircleAll(transform.position, floorDetectionRadius);
        var enemyFloorList = new List<EnemyFloor>();

        foreach (var collider in colliders)
            if (collider.TryGetComponent(out EnemyFloor enemyFloor))
                enemyFloorList.Add(enemyFloor);

        if (enemyFloorList.Count == 0) {
            enemyFloorAttacked = null;
        }
        else {
            enemyFloorList = enemyFloorList.OrderBy(ef => Vector2.Distance(transform.position, ef.transform.position))
                .ToList();
            enemyFloorAttacked = enemyFloorList[0];
        }
    }

    private void MovePlayer() {
        if (enemyFloorAttacked != null)
            MoveToFloor();
        else
            MoveToStart();
    }

    private void MoveToFloor() {
        transform.position = enemyFloorAttacked.transform.position;
    }

    private void MoveToStart() {
        transform.position = startPosition;
    }

    private void SendPlayerIsAttackingEvent() {
        if (enemyFloorAttacked != null) {
            var e = new OnPlayerAttackEnemyFloorEventArgs { enemyFloor = enemyFloorAttacked };
            OnPlayerAttackEnemyFloor?.Invoke(this, e);
        }
    }

    private void CheckRoofIsAttacked() {
        if (enemyFloorAttacked == null) {
            var colliders = Physics2D.OverlapCircleAll(transform.position, floorDetectionRadius);
            foreach (var collider in colliders)
                if (collider.TryGetComponent(out EnemyRoof enemyRoof)) {
                    enemyRoofAttacked = enemyRoof;
                    OnPlayerAttackEnemyRoof?.Invoke(this, enemyRoofAttacked);
                }
        }
    }

    private void BattleManager_OnPlayerWinBattle(object sender, BattleManager.OnPlayerWinBattleEventArgs e) {
        MoveToStart();
    }

    private void BattleManager_OnEnemyWinBattle(object sender, EventArgs e) {
        MoveToStart();
    }
}