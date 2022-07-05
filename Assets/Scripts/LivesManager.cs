using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour {

    [SerializeField] private int livesAmount = 3;
    private int remainingLives;

    public static event EventHandler<int> OnPlayerLoseLife;
    public static event EventHandler OnPlayerDie;
    private void Awake() {
        ResetLives();
    }

    // Start is called before the first frame update
    void Start()
    {
        BattleManager.Instance.OnEnemyWinBattle += BattleManager_OnEnemyWinBattle;
        BattleManager.OnEnemyWinBossBattle += BattleManager_OnEnemyWinBattle;
    }

    private void OnDisable() {
        BattleManager.Instance.OnEnemyWinBattle -= BattleManager_OnEnemyWinBattle;
        BattleManager.OnEnemyWinBossBattle += BattleManager_OnEnemyWinBattle;
    }

    private void BattleManager_OnEnemyWinBattle(object sender, EventArgs e) {
        remainingLives--;
        OnPlayerLoseLife?.Invoke(this, remainingLives);
        if (remainingLives <= 0) {
            Debug.Log("Invoking OnPlayerDie");
            OnPlayerDie?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ResetLives() {
        remainingLives = livesAmount;
    }
}
