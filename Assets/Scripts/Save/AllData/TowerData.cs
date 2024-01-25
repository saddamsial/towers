using System.Collections;
using System.Collections.Generic;
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

            //SaveSystem.Instance.SaveData(_floorCount);
        }
    }

    public TowerData() : base("my tower")
    {

    }

    public override void Load()
    {
        Debug.Log("-->" + FloorCount);
        // if (FloorCount == -1)

        // _floorCount = ES3.Load("_FloorCount", DataController.Instance.GamePresets.myTowerFloorCount, eS3Settings);
        Debug.Log("->" + FloorCount);
    }

}
