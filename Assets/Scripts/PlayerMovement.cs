using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDrag
{
    public event EventHandler<OnPlayerAttackEnemyFloorEventArgs> OnPlayerAttackEnemyFloor;
    public class OnPlayerAttackEnemyFloorEventArgs : EventArgs
    {
        public EnemyFloor enemyFloor;
    }

    private float floorDetectionRadius = .3f;

    private Vector2 draggingPosition;
    private Vector2 startPosition;
    private bool IsDragging = false;
    private EnemyFloor enemyFloorAttacked;

    public void OnDragEnd()
    {
        if (!IsDragging) return;
        CheckFloorIsAttacked();
        MovePlayer();
        SendPlayerIsAttackingEvent();
        IsDragging = false;
    }

    public void OnDragging(Vector2 position)
    {
        draggingPosition = position;
    }


    public void OnDragStart(Vector2 position)
    {
        draggingPosition = position;
        IsDragging = true;
    }

    private void Awake()
    {
        startPosition = transform.position;
        draggingPosition = transform.position;
    }

    private void Start()
    {
        BattleManager.Instance.OnPlayerWinBattle += BattleManager_OnPlayerWinBattle;
        BattleManager.Instance.OnEnemyWinBattle += BattleManager_OnEnemyWinBattle;
    }

    private void OnDisable()
    {
        BattleManager.Instance.OnPlayerWinBattle -= BattleManager_OnPlayerWinBattle;
        BattleManager.Instance.OnEnemyWinBattle += BattleManager_OnEnemyWinBattle;

    }

    private void Update()
    {
        if (IsDragging)
        {
            transform.position = draggingPosition;
        }
    }

    private void CheckFloorIsAttacked()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, floorDetectionRadius);
        List<EnemyFloor> enemyFloorList = new List<EnemyFloor>();

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent<EnemyFloor>(out EnemyFloor enemyFloor))
            {
                enemyFloorList.Add(enemyFloor);
            }
        }

        if (enemyFloorList.Count == 0)
        {
            enemyFloorAttacked = null;
        }
        else
        {
            enemyFloorList = enemyFloorList.OrderBy(ef => Vector2.Distance(transform.position, ef.transform.position)).ToList();
            enemyFloorAttacked = enemyFloorList[0];
        }
    }

    private void MovePlayer()
    {
        if (enemyFloorAttacked != null)
        {
            MoveToFloor();
        }
        else
        {
            MoveToStart();
        }
    }

    private void MoveToFloor()
    {
        transform.position = enemyFloorAttacked.transform.position;
    }

    private void MoveToStart()
    {
        transform.position = startPosition;
    }

    private void SendPlayerIsAttackingEvent()
    {
        if (enemyFloorAttacked == null) return;
        OnPlayerAttackEnemyFloor?.Invoke(this, new OnPlayerAttackEnemyFloorEventArgs { enemyFloor = enemyFloorAttacked });
    }

    private void BattleManager_OnPlayerWinBattle(object sender, BattleManager.OnPlayerWinBattleEventArgs e)
    {
        MoveToStart();
    }

    private void BattleManager_OnEnemyWinBattle(object sender, EventArgs e)
    {
        MoveToStart();
    }
}
