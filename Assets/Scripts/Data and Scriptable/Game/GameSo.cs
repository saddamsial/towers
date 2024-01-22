using System.Collections;
using System.Collections.Generic;
using Data_and_Scriptable.GunSo;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data")]
public class GameSo : ScriptableObject
{
    [ReorderableList]
    [Label("Levels")]
    public List<EnemyTowerSo> levels = new();
    public int myTowerFloorCount;
    public List<FloorSo> myFloorMaterials = new();
    public List<GunSo> myFloorGuns = new();
}
