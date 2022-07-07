using System;
using UnityEngine;

public class Knight : MonoBehaviour {
    
    private Floor playerFloor;
    private KnightSelector knightSelector;

    private void Awake() {
        playerFloor = transform.parent.GetComponent<Floor>();
        knightSelector = transform.GetComponent<KnightSelector>();
    }

    private void Start() {
        PlayerMovement.OnPlayerAttackEnemyFloor += PlayerMovement_OnPlayerAttackEnemyFloor;
        PlayerMovement.OnPlayerAttackEnemyRoof += PlayerMovement_OnPlayerAttackEnemyRoof;
        EnemyFloor.OnEnemyFloorPressed += EnemyFloor_OnEnemyFloorPressed;
        EnemyRoof.OnEnemyRoofPressed += EnemyRoof_OnEnemyRoofPressed;
    }

    private void OnDisable() {
        PlayerMovement.OnPlayerAttackEnemyFloor -= PlayerMovement_OnPlayerAttackEnemyFloor;
        PlayerMovement.OnPlayerAttackEnemyRoof -= PlayerMovement_OnPlayerAttackEnemyRoof;
        EnemyFloor.OnEnemyFloorPressed -= EnemyFloor_OnEnemyFloorPressed;
        EnemyRoof.OnEnemyRoofPressed -= EnemyRoof_OnEnemyRoofPressed;
    }

    private void PlayerMovement_OnPlayerAttackEnemyFloor(object sender,
        PlayerMovement.PlayerAttackEnemyFloorEventArgs args) {
        Battle(args.EnemyFloor);
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