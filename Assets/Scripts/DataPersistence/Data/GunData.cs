using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunData : Data
{
    public bool unlockState;
    public bool UnlockState
    {
        get => unlockState;
        set
        {
            unlockState = value;
            ES3.Save(id + "unlockState", unlockState);
        }
    }
    public GunData(string id) : base(id)
    {
        Load();
    }

    public override void Load()
    {
        unlockState = ES3.Load(id + "unlockState", false);
    }
}
