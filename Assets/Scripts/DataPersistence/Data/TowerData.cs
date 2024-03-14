using System;
using System.Collections.Generic;
using Data_and_Scriptable.GunSo;
using UnityEngine;

[ES3Serializable]
public class TowerData : Data
{
    public int floorCount = -1;
    public int FloorCount
    {
        get => floorCount;
        set
        {
            floorCount = value;
            ES3.Save(id + "FloorCount", floorCount);
        }
    }
    public List<GunSo> guns = new();
    public List<GunSo> Guns
    {
        get => guns;
        set
        {
            guns = new List<GunSo>(value);
            ES3.Save(id + "guns", guns);
        }
    }
    public List<int> floorLevels = new();
    public List<int> FloorLevels
    {
        get => floorLevels;
        set
        {
            floorLevels = new List<int>(value);
            ES3.Save(id + "levels", floorLevels);
        }
    }
    public TowerData() : base("tower")
    {
        Load();
    }
    public override void Load()
    {
        floorCount = ES3.Load(id + "FloorCount", DataPersistenceController.Instance.presets.myTowerFloorCount);
        guns = ES3.Load(id + "guns", DataPersistenceController.Instance.presets.myFloorGuns);
        floorLevels = ES3.Load(id + "levels", DataPersistenceController.Instance.presets.myFloorLevels);
    }
    public void UpdateFloorGun(int index, GunSo gun)
    {
        Guns[index] = gun;
        Guns = Guns;
    }
    public void UpdateFloorLevel(int index)
    {
        FloorLevels[index]++;
        FloorLevels = FloorLevels;
    }

}
