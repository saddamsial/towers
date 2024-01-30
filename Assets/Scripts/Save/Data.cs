using System;
using UnityEngine;

[Serializable]
[ES3Serializable]
public class Data
{
    public string id;
    public ES3Settings eS3Settings;

    public Data(string id)
    {
        this.id = id;
    }

    public virtual void Load(ES3Settings eS3Settings)
    {
        this.eS3Settings = eS3Settings;
    }

}
