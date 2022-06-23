using System;
using UnityEngine;

public class EnemyFloor : BasicFloor, IPressed {

    public static event EventHandler OnEnemyFloorPressed; 
    
    public static EnemyFloor Create(Transform parent, Vector3 position, FloorData data) {
        var floorTransform = Instantiate(GameAssets.Instance.pfEnemyFloor, Vector3.zero, Quaternion.identity, parent);
        floorTransform.localPosition = position;

        var enemyFloor = floorTransform.GetComponent<EnemyFloor>();
        enemyFloor.floorData = data;
        return enemyFloor;
    }

    private void BattleManager_OnPlayerWinBattle(object sender, BattleManager.OnPlayerWinBattleEventArgs e) {
        if (e.floorData.floorNumber != floorData.floorNumber) return;
        floorData.powerLevel = 0;
        UpdateText();
        GetComponent<BoxCollider2D>().enabled = false;
        if (IsDefeated()) OnFloorDefeated?.Invoke(this, EventArgs.Empty);
    }
    
    public event EventHandler OnFloorDefeated;

    private void Awake() {
        levelIndicator = GetComponentInChildren<LevelIndicator>();
        BattleManager.Instance.OnPlayerWinBattle += BattleManager_OnPlayerWinBattle;
    }

    private void Start() {
        UpdateText();
    }

    private void OnDisable() {
        BattleManager.Instance.OnPlayerWinBattle -= BattleManager_OnPlayerWinBattle;
    }

    private void UpdateText() {
        levelIndicator.SetPowerLevelText(floorData.GetFullPowerLevelString());
    }

    public bool IsDefeated() {
        return floorData.powerLevel <= 0;
    }

    public void OnPressed() {
        OnEnemyFloorPressed?.Invoke(this, EventArgs.Empty);
    }
}