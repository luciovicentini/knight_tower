using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    private Vector3 bossMovingInStartPosition = new Vector3(4, 10, 0);
    private float movingInDuration = 2f;

    private Vector3 roofPosition;
    private LevelIndicator levelIndicator;

    private void Awake()
    {
        transform.position += bossMovingInStartPosition;
        roofPosition = new Vector3(0, 0.3f, 0);
        levelIndicator = GetComponentInChildren<LevelIndicator>();
    }

    private void Start()
    {
        EnemyTower.OnAllFloorsDefeated += EnemyTower_OnAllFloorsDefeated;
    }

    private void OnDisable()
    {
        EnemyTower.OnAllFloorsDefeated -= EnemyTower_OnAllFloorsDefeated;
    }

    private void EnemyTower_OnAllFloorsDefeated(object sender, EventArgs e)
    {
        StartCoroutine(MoveBoss());
    }

    private IEnumerator MoveBoss()
    {
        float timeElapsed = 0f;
        Vector3 startPosition = transform.localPosition;
        while (timeElapsed < movingInDuration)
        {
            transform.localPosition = Vector3.Lerp(startPosition, roofPosition, timeElapsed / movingInDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = roofPosition;
    }

    public void SetLevel(int bossLevel)
    {
        levelIndicator.SetPowerLevelText(bossLevel.ToString());
    }
}
