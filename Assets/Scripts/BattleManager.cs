using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public event EventHandler<OnPlayerWinBattleEventArgs> OnPlayerWinBattle;
    public class OnPlayerWinBattleEventArgs : EventArgs
    {
        public int newPlayerLevel;
        public FloorData floorData;
    }

    public event EventHandler OnEnemyWinBattle;

    public static event EventHandler OnPlayerWinBossBattle;
    public static event EventHandler OnEnemyWinBossBattle;

    private void Awake()
    {
        Instance = this;
    }

    public void Resolve(int playerLevel, FloorData enemyFloorData)
    {
        if (playerLevel == enemyFloorData.powerLevel || playerLevel > enemyFloorData.powerLevel)
        {
            OnPlayerWinBattleEventArgs args = new OnPlayerWinBattleEventArgs { newPlayerLevel = playerLevel + enemyFloorData.powerLevel, floorData = enemyFloorData };
            OnPlayerWinBattle?.Invoke(this, args);
        }
        else
        {
            CinemachineShake.Instance.ShakeCamera(1f, .5f);
            OnEnemyWinBattle?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ResolveBossBattle(int playerLevel, int bossLevel)
    {
        if (playerLevel == bossLevel || playerLevel > bossLevel)
        {
            OnPlayerWinBossBattle?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            OnEnemyWinBossBattle?.Invoke(this, EventArgs.Empty);
        }
    }
}
