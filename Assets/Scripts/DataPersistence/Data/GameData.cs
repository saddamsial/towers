using System;
using System.Diagnostics;
[ES3Serializable]
public class GameData : Data
{
    public int playCount;

    public GameData() : base("game")
    {
        playCount = 0;
        Load();
    }

    public override void Load()
    {
        playCount = ES3.Load(id + "game", 0);

        Debug.Print("->" + playCount);
    }

}
