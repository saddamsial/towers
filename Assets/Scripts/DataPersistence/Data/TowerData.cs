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
    } //= new();

    public void UpdateFloorGun(int index, GunSo gun)
    {
        Guns[index] = gun;
        Guns = Guns;
    }

    public TowerData() : base("tower")
    {
        Load();
    }

    public override void Load()
    {
        // Debug.Log("->?" + floorCount);
        floorCount = ES3.Load(id + "FloorCount", DataPersistenceController.Instance.presets.myTowerFloorCount);
        guns = ES3.Load(id + "guns", DataPersistenceController.Instance.presets.myFloorGuns);

        // Debug.Log("->" + floorCount);


    }

}
