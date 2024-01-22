using System;
using System.Collections;
using System.Collections.Generic;
using Data_and_Scriptable.GunSo;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Tower")]
public class EnemyTowerSo : ScriptableObject
{
    [Serializable]
    public class FloorTemp
    {
        public FloorSo floorSo;
        public GunSo gunToAttach;
    }

    public List<FloorTemp> floorTemps = new();
}
