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

            ES3.Save(id + "_FloorCount", _floorCount, eS3Settings);
        }
    }

    public TowerData(string id) : base(id)
    {

    }

    public override void Load(ES3Settings eS3Settings)
    {
        Debug.Log("-->" + FloorCount);
        // if (FloorCount == -1)
        _floorCount = ES3.Load(id + "_FloorCount", DataController.Instance.gamePresets.myTowerFloorCount, eS3Settings);
        Debug.Log("->" + FloorCount);
    }


}
