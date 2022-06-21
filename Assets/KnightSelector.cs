using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSelector : MonoBehaviour {
    [SerializeField] private bool isSelected;
    [SerializeField] private Transform selectionKnightPrefab;

    private void Awake() {
        BattleManager.OnBattleStart += BattleManager_OnBattleStart;
    }

    private void OnDisable() {
        BattleManager.OnBattleStart -= BattleManager_OnBattleStart;
    }

    private void BattleManager_OnBattleStart(object sender, EventArgs e) {
        isSelected = false;
    }

    private void Update() {
        selectionKnightPrefab.gameObject.SetActive(isSelected);
    }

    public void ToggleSelection() {
        isSelected = !isSelected;
    }

    public bool IsSelected() => isSelected;
}