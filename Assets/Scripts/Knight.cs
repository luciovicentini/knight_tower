using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour, IDrag
{

    private Vector2 draggingPosition;
    private Vector2 startPosition;
    private bool IsDragging = false;

    private Floor playerFloor;
    private EnemyFloor enemyFloorAttacked;

    private void Awake()
    {
        startPosition = transform.position;
        draggingPosition = transform.position;
        playerFloor = transform.parent.GetComponent<Floor>();
        Debug.Log(playerFloor);
    }

    private void Update()
    {
        if (IsDragging)
        {
            transform.position = draggingPosition;
        }
    }

    public void OnDragEnd()
    {
        if (!IsDragging) return;
        MoveToClosestFloor();
        Battle();
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

    private void MoveToClosestFloor()
    {
        if (enemyFloorAttacked == null)
        {
            transform.position = startPosition;
        } else
        {
            transform.position = enemyFloorAttacked.transform.position;
        }
    }

    private void Battle()
    {
        Debug.Log(BattleManager.Instance);
        Debug.Log(playerFloor);
        Debug.Log(enemyFloorAttacked);
        if (enemyFloorAttacked != null) { 
            BattleManager.Instance.Resolve(playerFloor.GetPowerLevel(), enemyFloorAttacked.GetPowerLevel());
        }
    }
}
