using System.Collections.Generic;
using UnityEngine;
using Utils;

public class DataController : Singleton<DataController>
{

    public ES3Settings eS3Settings;

    public List<Data> data = new();
    Data tempData;

    public GamePresets gamePresets;

    private void Awake()
    {
        gamePresets = Resources.Load<GamePresets>("Game Presets");
        eS3Settings = new ES3Settings(gamePresets.dataLocation + ".json", ES3.Location.File);

        ES3.CacheFile(gamePresets.dataLocation + ".json", eS3Settings);

        Load();
    }

    public void Start()
    {
    }

    public Data GetData(string id, Data defaultData)
    {
        for (var i = 0; i < data.Count; i++)
        {
            if (data[i].id == id)
            {
                tempData = data[i];
            }
        }
        if (tempData == null)
        {
            Debug.Log("not found");

            tempData = defaultData;
            data.Add(tempData);
        }
        tempData.Load(eS3Settings);

        return tempData;
    }


    private void Load()
    {

        data = ES3.Load("_data", new List<Data>(), eS3Settings);

        data ??= new();
    }

    public void Save()
    {

        ES3.Save("_data", data, eS3Settings);
        // ES3.StoreCachedFile(gamePresets.dataLocation + ".json", eS3Settings);
    }

    public void DeleteData()
    {
        ES3.DeleteFile(gamePresets.dataLocation + ".json", eS3Settings);
    }


    private void OnApplicationQuit()
    {
#if UNITY_EDITOR
        Save();
#endif
    }

    private void OnApplicationPause(bool state)
    {
        if (state)
            Save();
    }
}
