using System;
using System.Collections.Generic;
using Data_and_Scriptable.GunSo;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data")]
public class GamePresets : ScriptableObject
{
    public string dataLocation;
    // [ReorderableList]
    // [Label("Levels")]
    // public List<EnemyTowerSo> levels = new();
    public int myTowerFloorCount, maxPossibleFloor, enemyFloorMoney;
    public List<int> myFloorLevels = new();
    public List<int> myFloorManagers = new();
    public List<int> gunUnlockPrices = new();
    public List<int> addFloorGemCounts = new();
    public List<GunSo> myFloorGuns = new();
    public List<int> stepCountsForEachLevel = new();
    public bool showLoots;
    [ShowIf("showLoots")]
    [ReorderableList]
    [Label("Loots")]
    public List<Loots> LootList = new();
    [ShowIf("showLoots")]
    [ReorderableList]
    [Label("Level Up Loots")]
    public List<Loots> LevelUpLoots = new();
    public bool HideDefaults;
    [HideIf("HideDefaults")]
    public List<int> myFloorLevelsDefault = new();
    [HideIf("HideDefaults")]
    public List<int> myFloorManagersDefault = new();
    [HideIf("HideDefaults")]
    public List<GunSo> myFloorGunsDefault = new();
    [Serializable]
    public class Loots
    {
        public List<LootItem> loot = new();
    }
    [Button]
    public void ResetData()
    {
        myTowerFloorCount = 2;
        myFloorGuns = new List<GunSo>(myFloorGunsDefault);
        myFloorLevels = new List<int>(myFloorLevelsDefault);
        myFloorManagers = new List<int>(myFloorManagersDefault);
    }
}
