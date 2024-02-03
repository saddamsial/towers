using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Utils;

public class DataPersistenceController : Singleton<DataPersistenceController>
{
    private ES3Settings settings;
    public GamePresets presets;
    public List<Data> data = new();

    public void Start()
    {
        LoadGame();
    }

    public void LoadGame()
    {
        settings = new ES3Settings(presets.dataLocation, ES3.Location.Cache);
        ES3.CacheFile(presets.dataLocation, settings);
        data = ES3.Load("_data", new List<Data>(), settings);

        data ??= new();
    }
    public void SaveGame()
    {
        ES3.Save("_data", data, settings);
        ES3.StoreCachedFile(presets.dataLocation, settings);
    }

    public List<Data> FindAllData()
    {
        var dataPersistences = FindObjectsOfType<MonoBehaviour>().OfType<Data>();
        return new List<Data>(dataPersistences);
    }

    public Data GetData(string id, Data defaultData = null)
    {
        var data = FindData(id);

        if (data == null)
        {
            data = defaultData;
            this.data.Add(data);
        }

        return data;
    }

    private Data FindData(string id)
    {
        for (int i = 0; i < data.Count; i++)
        {
            if (id.Equals(data[i].id))
                return data[i];
        }

        return null;
    }


    private void OnApplicationQuit()
    {
#if UNITY_EDITOR
        SaveGame();
#endif
    }

    private void OnApplicationPause(bool state)
    {
        if (state)
            SaveGame();
    }

}
