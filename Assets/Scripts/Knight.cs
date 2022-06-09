using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    private Floor playerFloor;
    private PlayerMovement playerMovement;


    private void Awake()
    {
        playerFloor = transform.parent.GetComponent<Floor>();
        playerMovement = transform.GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        playerMovement.OnPlayerAttackEnemyFloor += PlayerMovement_OnPlayerAttackEnemyFloor;
    }

    private void PlayerMovement_OnPlayerAttackEnemyFloor(object sender, PlayerMovement.OnPlayerAttackEnemyFloorEventArgs args)
    {
        Battle(args.enemyFloor);
    }

    private void Battle(EnemyFloor enemyFloor)
    {
        BattleManager.Instance.Resolve(playerFloor.floorData.powerLevel, enemyFloor.floorData);
    }
}
