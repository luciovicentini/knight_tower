using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        PlayerMovement.OnPlayerAttackEnemyRoof += PlayerMovement_OnPlayerAttackEnemyRoof;
    }

    private void OnDisable()
    {
        playerMovement.OnPlayerAttackEnemyFloor -= PlayerMovement_OnPlayerAttackEnemyFloor;
        PlayerMovement.OnPlayerAttackEnemyRoof -= PlayerMovement_OnPlayerAttackEnemyRoof;
    }

    private void PlayerMovement_OnPlayerAttackEnemyFloor(object sender, PlayerMovement.OnPlayerAttackEnemyFloorEventArgs args)
    {
        Battle(args.enemyFloor);
    }

    private void PlayerMovement_OnPlayerAttackEnemyRoof(object sender, int bossLevel)
    {
        BattleManager.Instance.ResolveBossBattle(playerFloor.floorData.powerLevel, bossLevel);
    }

    private void Battle(EnemyFloor enemyFloor)
    {
        BattleManager.Instance.Resolve(playerFloor.floorData.powerLevel, enemyFloor.floorData);
    }
}
