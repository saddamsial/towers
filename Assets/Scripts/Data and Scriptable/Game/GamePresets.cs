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
    public List<FloorSo> myFloorMaterials = new();
    public List<GunSo> myFloorGuns = new();
    public bool HideDefaults;
    [HideIf("HideDefaults")]
    public List<FloorSo> myFloorMaterialsDefault = new();
    [HideIf("HideDefaults")]
    public List<GunSo> myFloorGunsDefault = new();

    [Button]
    public void ResetData()
    {
        myFloorGuns = new List<GunSo>(myFloorGunsDefault);
        myFloorMaterials = new List<FloorSo>(myFloorMaterialsDefault);
    }
}
