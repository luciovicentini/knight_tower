using UnityEngine;

public class EnemyRoof : MonoBehaviour {
    private void Start() {
    }

    public FloorData GetBossData() {
        return GetComponentInChildren<EnemyBoss>().GetBossData();
    }
}