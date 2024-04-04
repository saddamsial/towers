using System;
using System.Collections;
using System.Collections.Generic;
using Data_and_Scriptable.GunSo;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data")]
public class GamePresets : ScriptableObject
{
    public string dataLocation;
    [ReorderableList]
    [Label("Levels")]
    public List<EnemyTowerSo> levels = new();
    public int myTowerFloorCount, maxPossibleFloor;
    public List<int> myFloorLevels = new();
    public List<int> myFloorManagers = new();
    public List<GunSo> myFloorGuns = new();
    public bool HideDefaults;
    [HideIf("HideDefaults")]
    public List<int> myFloorLevelsDefault = new();
    [HideIf("HideDefaults")]
    public List<int> myFloorManagersDefault = new();
    [HideIf("HideDefaults")]
    public List<GunSo> myFloorGunsDefault = new();
    [ReorderableList]
    [Label("Loots")]
    public List<Loots> LootList = new();

    [Button]
    public void ResetData()
    {
        myFloorGuns = new List<GunSo>(myFloorGunsDefault);
        myFloorLevels = new List<int>(myFloorLevelsDefault);
        myFloorManagers = new List<int>(myFloorManagersDefault);
    }

    [Serializable]
    public class Loots
    {
        public List<LootItem> loot = new();
    }
}
