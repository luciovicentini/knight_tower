using System;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    public static BattleManager Instance;

    private void Awake() {
        Instance = this;
    }

    public event EventHandler<OnPlayerWinBattleEventArgs> OnPlayerWinBattle;

    public event EventHandler OnEnemyWinBattle;

    public static event EventHandler OnPlayerWinBossBattle;
    public static event EventHandler OnEnemyWinBossBattle;

    public void Resolve(int playerLevel, FloorData enemyFloorData) {
        if (playerLevel == enemyFloorData.powerLevel || playerLevel > enemyFloorData.powerLevel) {
            var args = new OnPlayerWinBattleEventArgs
                { newPlayerLevel = playerLevel + enemyFloorData.powerLevel, floorData = enemyFloorData };
            OnPlayerWinBattle?.Invoke(this, args);
        }
        else {
            CinemachineShake.Instance.ShakeCamera(1f, .5f);
            OnEnemyWinBattle?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ResolveBossBattle(int playerLevel, int bossLevel) {
        if (playerLevel == bossLevel || playerLevel > bossLevel)
            OnPlayerWinBossBattle?.Invoke(this, EventArgs.Empty);
        else
            OnEnemyWinBossBattle?.Invoke(this, EventArgs.Empty);
    }

    public class OnPlayerWinBattleEventArgs : EventArgs {
        public FloorData floorData;
        public int newPlayerLevel;
    }
}