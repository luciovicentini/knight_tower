public class Floor : BasicFloor {
    private void Awake() {
        levelIndicator = GetComponentInChildren<LevelIndicator>();
        floorData = FloorData.One();
    }

    private void Start() {
        
        UpdateLevelIndicator();
    }

    private void OnEnable() {
        BattleManager.Instance.OnPlayerWinBattle += BattleManager_OnPlayerWinBattle;
    }

    private void OnDisable() {
        BattleManager.Instance.OnPlayerWinBattle -= BattleManager_OnPlayerWinBattle;
    }

    private void BattleManager_OnPlayerWinBattle(object sender, BattleManager.OnPlayerWinBattleEventArgs e) {
        floorData.powerLevel = e.newPlayerLevel;
        UpdateLevelIndicator();
    }

    private void UpdateLevelIndicator() {
        if (floorData == null) return;
        levelIndicator.SetPowerLevelText(floorData.GetFullPowerLevelString());
    }

    public void ResetPlayerFloor() {
        SetPlayerLevel(FloorData.One());
        UpdateLevelIndicator();
    }

    public void SetPlayerLevel(FloorData playerFloorData) {
        floorData = playerFloorData;
        UpdateLevelIndicator();
    }
}