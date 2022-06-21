using System;
using UnityEngine;

public class Knight : MonoBehaviour {
    private Floor playerFloor;
    private PlayerMovement playerMovement;
    private KnightSelector knightSelector;

    private void Awake() {
        playerFloor = transform.parent.GetComponent<Floor>();
        playerMovement = transform.GetComponent<PlayerMovement>();
        knightSelector = transform.GetComponent<KnightSelector>();
    }

    private void Start() {
        playerMovement.OnPlayerAttackEnemyFloor += PlayerMovement_OnPlayerAttackEnemyFloor;
        PlayerMovement.OnPlayerAttackEnemyRoof += PlayerMovement_OnPlayerAttackEnemyRoof;
        EnemyFloor.OnEnemyFloorPressed += EnemyFloor_OnEnemyFloorPressed;
        EnemyRoof.OnEnemyRoofPressed += EnemyRoof_OnEnemyRoofPressed;
    }

    private void OnDisable() {
        playerMovement.OnPlayerAttackEnemyFloor -= PlayerMovement_OnPlayerAttackEnemyFloor;
        PlayerMovement.OnPlayerAttackEnemyRoof -= PlayerMovement_OnPlayerAttackEnemyRoof;
        EnemyFloor.OnEnemyFloorPressed -= EnemyFloor_OnEnemyFloorPressed;
        EnemyRoof.OnEnemyRoofPressed -= EnemyRoof_OnEnemyRoofPressed;
    }

    private void PlayerMovement_OnPlayerAttackEnemyFloor(object sender,
        PlayerMovement.OnPlayerAttackEnemyFloorEventArgs args) {
        Battle(args.enemyFloor);
    }

    private void PlayerMovement_OnPlayerAttackEnemyRoof(object sender, EnemyRoof enemyRoof) {
        Battle(enemyRoof);
    }
    
    private void EnemyFloor_OnEnemyFloorPressed(object sender, EventArgs e) {
        if (!knightSelector.IsSelected()) return;
        EnemyFloor enemyFloor = (EnemyFloor)sender;
        Battle(enemyFloor);
    }
    
    
    private void EnemyRoof_OnEnemyRoofPressed(object sender, EventArgs e) {
        if (!knightSelector.IsSelected()) return;
        EnemyRoof enemyRoof = (EnemyRoof)sender;
        Battle(enemyRoof);
    }

    private void Battle(EnemyFloor enemyFloor) {
        BattleManager.Instance.Resolve(playerFloor.floorData.powerLevel, enemyFloor.floorData);
    }

    private void Battle(EnemyRoof enemyRoof) {
        BattleManager.Instance.ResolveBossBattle(playerFloor.floorData.powerLevel, enemyRoof.GetBossData().powerLevel);
    }
}