using System;
using System.Collections.Generic;
using Data_and_Scriptable.GunSo;
using UnityEngine;

public class TowerData : Data
{
    public int floorCount = -1;
    public int FloorCount
    {
        get => floorCount;
        set
        {
            floorCount = value;
        }
    }

    public List<GunSo> guns = new();
    public List<GunSo> Guns
    {
        get => guns;
        set
        {
            guns = new List<GunSo>(value);
            // ES3.Save(id + "_FloorGuns", guns);
        }
    } //= new();

    public TowerData() : base("tower")
    {
        Load();
    }

    public override void Load()
    {
        // base.Load();

        // Debug.Log("->?" + floorCount);
        FloorCount = ES3.Load(id + "FloorCount", DataPersistenceController.Instance.presets.myTowerFloorCount);
        Guns = ES3.Load(id + "guns", DataPersistenceController.Instance.presets.myFloorGuns);

        // Debug.Log("->" + floorCount);


    }

}
