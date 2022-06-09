using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDrag
{
    public event EventHandler<OnPlayerAttackEnemyFloorEventArgs> OnPlayerAttackEnemyFloor;
    public class OnPlayerAttackEnemyFloorEventArgs : EventArgs
    {
        public EnemyFloor enemyFloor;
    }

    private Vector2 draggingPosition;
    private Vector2 startPosition;
    private bool IsDragging = false;
    private EnemyFloor enemyFloorAttacked;

    private void Awake()
    {
        startPosition = transform.position;
        draggingPosition = transform.position;
    }

    public void OnDragEnd()
    {
        if (!IsDragging) return;
        SendPlayerIsAttackingEvent();
        MovePlayer();
        IsDragging = false;
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

    public void OnDragging(Vector2 position)
    {
        draggingPosition = position;
    }


    public void OnDragStart(Vector2 position)
    {
        draggingPosition = position;
        IsDragging = true;
    }

    private void Update()
    {
        if (IsDragging)
        {
            transform.position = draggingPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyFloor enemyFloorTouched = collision.GetComponent<EnemyFloor>();
        if (enemyFloorTouched != null)
        {
            enemyFloorAttacked = enemyFloorTouched;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemyFloor enemyFloorTouched = collision.GetComponent<EnemyFloor>();
        if (enemyFloorAttacked == enemyFloorTouched) enemyFloorAttacked = null;
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
}
