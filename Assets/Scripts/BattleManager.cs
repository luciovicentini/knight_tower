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

    public enum BattleResolve
    {
        PlayerWin,
        EnemyWin
    }

    private void Awake()
    {
        Instance = this;
    }

    public void Resolve(int playerLevel, FloorData enemyFloorData)
    {
        Debug.Log("[BattleManager](Resolve) - Player is attacking floor number " + enemyFloorData.floorNumber);
        Debug.Log("[BattleManager](Resolve) - Player Level " + playerLevel);
        Debug.Log("[BattleManager](Resolve) - Enemy Level " + enemyFloorData.powerLevel);
        if (playerLevel == enemyFloorData.powerLevel || playerLevel > enemyFloorData.powerLevel)
        {
            Debug.Log("[BattleManager](Resolve) - Player Won");
            OnPlayerWinBattleEventArgs args = new OnPlayerWinBattleEventArgs { newPlayerLevel = playerLevel + enemyFloorData.powerLevel, floorData = enemyFloorData };
            OnPlayerWinBattle?.Invoke(this, args);
        }
        else
        {
            Debug.Log("[BattleManager](Resolve) - Enemy Won");
            CinemachineShake.Instance.ShakeCamera(1f, .5f);
            OnEnemyWinBattle?.Invoke(this, EventArgs.Empty);
        }
    }
}
