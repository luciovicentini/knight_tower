using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyFloor : MonoBehaviour
{
    private int powerLevel;
    private TextMeshPro powerLevelText;

    public static EnemyFloor Create(Transform parent, Vector3 position)
    {
        Transform floorTransform = Instantiate(GameAssets.Instance.pfEnemyFloor, Vector3.zero, Quaternion.identity, parent);
        floorTransform.localPosition = position;

        EnemyFloor enemyFloor = floorTransform.GetComponent<EnemyFloor>();
        return enemyFloor;
    }

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
        powerLevel = 0;
        UpdateText();
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        powerLevelText = transform.Find("levelIndicator").Find("levelIndicatorText").GetComponent<TextMeshPro>();
    }

    void Start()
    {
        UpdateText();    
    }

    private void UpdateText()
    {
        powerLevelText.SetText(powerLevel.ToString());
    }

    public void SetPowerLevel(int level) => powerLevel = level;

    public int GetPowerLevel() => powerLevel;


}
