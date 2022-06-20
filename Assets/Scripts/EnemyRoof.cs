using UnityEngine;

public class EnemyRoof : MonoBehaviour {
    
    public FloorData GetBossData() {
        return GetComponentInChildren<EnemyBoss>().GetBossData();
    }
}