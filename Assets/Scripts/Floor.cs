using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private int powerLevel;
    private TextMeshPro powerLevelText;

    public static Floor Create(Transform parent, Vector3 position)
    {
        Transform floorTransform = Instantiate(GameAssets.Instance.pfEnemyFloor, Vector3.zero, Quaternion.identity, parent);
        floorTransform.localPosition = position;

        Floor floor = floorTransform.GetComponent<Floor>();
        return floor;
    }

    private void Awake()
    {
        powerLevelText = transform.Find("levelIndicator").Find("levelIndicatorText").GetComponent<TextMeshPro>();
    }

    // Start is called before the first frame update
    void Start()
    {
        powerLevelText.SetText(powerLevel.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPowerLevel(int level) => powerLevel = level;
    
}
