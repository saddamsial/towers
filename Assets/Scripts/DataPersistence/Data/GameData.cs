using System;
using System.Diagnostics;

public class GameData : Data
{
    public int playCount;

    public GameData() : base("game")
    {
        playCount = 0;
    }

    public override void Load()
    {
        base.Load();


        playCount = ES3.Load(id + "game", 0);

        Debug.Print("->" + playCount);
    }

}
