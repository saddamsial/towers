using System;
using System.Collections.Generic;
using Data_and_Scriptable.GunSo;
using UnityEngine;

public class TowerData : Data
{
    public int _floorCount = -1;
    public int FloorCount
    {
        get => _floorCount;
        set
        {
            _floorCount = value;
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
        FloorCount = 2;
        Guns = new List<GunSo>();
    }

    public override void Load()
    {
        base.Load();

        Debug.Log("->?" + _floorCount);
        _floorCount = ES3.Load(id + "FloorCount", 2);

        Debug.Log("->" + _floorCount);


    }

}
