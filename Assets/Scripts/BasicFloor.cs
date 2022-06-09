using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BasicFloor : MonoBehaviour
{
    public FloorData floorData { get; set; }
    public TextMeshPro powerLevelText { get; set; }

    public bool GetIsDefeated() => floorData.powerLevel >= 0;
}
