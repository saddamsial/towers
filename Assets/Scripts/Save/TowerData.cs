using System;
using System.Collections.Generic;
using Data_and_Scriptable.GunSo;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[ES3Serializable]
public class TowerData : Data
{
    public int _floorCount = -1;
    public int FloorCount
    {
        get => _floorCount;
        set
        {
            _floorCount = value;

            ES3.Save(id + "_FloorCount", _floorCount, eS3Settings);
        }
    }

    public List<GunSo> guns = new();
    public List<GunSo> Guns
    {
        get => guns;
        set
        {
            guns = new List<GunSo>(value);
            ES3.Save(id + "_FloorGuns", guns, eS3Settings);
        }
    } //= new();

    public TowerData(string id) : base(id)
    {

    }

    public override void Load(ES3Settings eS3Settings)
    {
        if (_floorCount == -1)
            _floorCount = ES3.Load(id + "_FloorCount", DataController.Instance.gamePresets.myTowerFloorCount, ES3Settings.defaultSettings);
        if (guns.Count == 0)
            guns = ES3.Load(id + "_FloorGuns", DataController.Instance.gamePresets.myFloorGuns, eS3Settings);
    }


}
