using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveCamera : MonoBehaviour, IDrag
{
    private bool isDragging;
    private Vector2 draggingPosition;
    private Vector2 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;
        BattleManager.Instance.OnPlayerWinBattle += BattleManager_OnPlayerWinBattle;
        BattleManager.Instance.OnEnemyWinBattle += BattleManager_OnEnemyWinBattle;
    }

    private void OnDisable()
    {
        BattleManager.Instance.OnPlayerWinBattle -= BattleManager_OnPlayerWinBattle;
        BattleManager.Instance.OnEnemyWinBattle -= BattleManager_OnEnemyWinBattle;
    }

    private void BattleManager_OnEnemyWinBattle(object sender, EventArgs e)
    {
        ResetPosition();
    }

    private void BattleManager_OnPlayerWinBattle(object sender, BattleManager.OnPlayerWinBattleEventArgs e)
    {
        ResetPosition();
    }

    private void ResetPosition()
    {
        transform.position = startingPosition;
    }

    private void Update()
    {
        if (isDragging)
        {
            transform.position = draggingPosition;
        }
    }

    public void OnDragEnd()
    {
        isDragging = false;
        Debug.Log($"[MoveCamera](OnDragEnd) - isDragging = {isDragging}");
    }

    public void OnDragging(Vector2 position)
    {
        draggingPosition = position;
        Debug.Log($"[MoveCamera](OnDragging) - position = {position}");
    }

    public void OnDragStart(Vector2 position)
    {
        isDragging = true;
        Debug.Log($"[MoveCamera](OnDragStart) - position = {position}");

    }
}
