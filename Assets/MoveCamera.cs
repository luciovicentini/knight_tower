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

    private Transform cameraConfiner;

    private void Awake()
    {
        cameraConfiner = GameObject.Find("/CameraHandler/CameraConfiner")?.transform;
    }

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
            Vector3 dir = ((Vector3)draggingPosition - transform.position);
            float y = transform.position.y + dir.y * 1f * Time.deltaTime;
            y = Mathf.Clamp(y, 0, cameraConfiner.localScale.y - 4);
            transform.position = new Vector2(transform.position.x, y);
        }
    }

    public void OnDragEnd()
    {
        isDragging = false;
    }

    public void OnDragging(Vector2 position)
    {
        draggingPosition = position;
    }

    public void OnDragStart(Vector2 position)
    {
        isDragging = true;
    }
}
