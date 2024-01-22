using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataBase
{
    public string id;
    public ES3Settings eS3Settings;

    public DataBase(string id)
    {
        this.id = id;
    }

    public virtual void LoadData(ES3Settings eS3Settings)
    {
        this.eS3Settings = eS3Settings;
    }
}
