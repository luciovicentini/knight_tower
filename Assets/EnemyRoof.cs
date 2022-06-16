using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoof : MonoBehaviour
{

    void Start()
    {

    }

    public int GetBossLevel() => GetComponentInChildren<EnemyBoss>().GetBossLevel();
}
