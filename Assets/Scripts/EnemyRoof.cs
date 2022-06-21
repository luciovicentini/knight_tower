using System;
using UnityEngine;

public class EnemyRoof : MonoBehaviour, IPressed {

    public static event EventHandler OnEnemyRoofPressed; 
    public FloorData GetBossData() {
        return GetComponentInChildren<EnemyBoss>().GetBossData();
    }

    public void OnPressed() {
        OnEnemyRoofPressed?.Invoke(this, EventArgs.Empty);
    }
}