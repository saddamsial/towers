using System;

[Serializable]
[ES3Serializable]
public class Data
{
    public string id;

    public Data(string id)
    {
        this.id = id;
    }

    public virtual void Load()
    {

    }
}
