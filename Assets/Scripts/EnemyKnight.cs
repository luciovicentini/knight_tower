using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnight : MonoBehaviour
{
    private void OnEnable()
    {
        BattleManager.Instance.OnPlayerWinBattle += BattleManager_OnPlayerWinBattle;
    }

    private void OnDisable()
    {
        BattleManager.Instance.OnPlayerWinBattle -= BattleManager_OnPlayerWinBattle;
    }

    private void BattleManager_OnPlayerWinBattle(object sender, BattleManager.OnPlayerWinBattleEventArgs e)
    {
        // Destroy(this.gameObject);
    }
}
