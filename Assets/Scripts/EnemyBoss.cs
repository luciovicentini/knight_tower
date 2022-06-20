using System;
using System.Collections;
using UnityEngine;

public class EnemyBoss : MonoBehaviour {
    private FloorData bossData;
    private readonly Vector3 bossMovingInStartPosition = new(4, 10, 0);
    private LevelIndicator levelIndicator;
    private readonly float movingInDuration = 1f;

    private Vector3 roofPosition;

    private void Awake() {
        transform.position += bossMovingInStartPosition;
        roofPosition = new Vector3(0, 0.3f, 0);
        levelIndicator = GetComponentInChildren<LevelIndicator>();
    }

    private void Start() {
        EnemyTower.OnAllFloorsDefeated += EnemyTower_OnAllFloorsDefeated;
    }

    private void OnDisable() {
        EnemyTower.OnAllFloorsDefeated -= EnemyTower_OnAllFloorsDefeated;
    }

    private void EnemyTower_OnAllFloorsDefeated(object sender, EventArgs e) {
        StartCoroutine(MoveBossToRoof());
    }

    private IEnumerator MoveBossToRoof() {
        var timeElapsed = 0f;
        var startPosition = transform.localPosition;
        while (timeElapsed < movingInDuration) {
            transform.localPosition = Vector3.Lerp(startPosition, roofPosition, timeElapsed / movingInDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = roofPosition;
    }

    public void SetLevel(FloorData boss) {
        bossData = boss;
        levelIndicator.SetPowerLevelText(bossData.GetFullPowerLevelString());
    }

    public FloorData GetBossData() {
        return bossData;
    }
}