using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public event EventHandler<OnPlayerWinBattleEventArgs> OnPlayerWinBattle;
    public class OnPlayerWinBattleEventArgs: EventArgs
    {
        public int newPlayerLevel;
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

    public void Resolve(int playerLevel, int enemyLevel)
    {
        Debug.Log("Resolving battle " + playerLevel + " " + enemyLevel);
        if (playerLevel == enemyLevel || playerLevel > enemyLevel)
            OnPlayerWinBattle?.Invoke(this, new OnPlayerWinBattleEventArgs { newPlayerLevel = playerLevel + enemyLevel});
        else
            OnEnemyWinBattle?.Invoke(this, EventArgs.Empty);
    }
}
