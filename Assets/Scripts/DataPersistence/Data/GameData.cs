using System;
using System.Collections.Generic;
using UnityEngine;
[ES3Serializable]
public class GameData : Data
{
    public int lootCount, playCount, money, gem, gear, ticket;
    public Dictionary<int, int> managers = new();
    public int LootCount
    {
        get => lootCount;
        set
        {
            lootCount = value;
            ES3.Save(id + "lootCount", lootCount);
        }
    }
    public int PlayCount
    {
        get => playCount;
        set
        {
            playCount = value;
            ES3.Save(id + "PlayCount", playCount);
        }
    }
    public int Money
    {
        get => money;
        set
        {
            money = value;
            ES3.Save(id + "money", money);
        }
    }
    public int Gem
    {
        get => gem;
        set
        {
            gem = value;
            ES3.Save(id + "gem", gem);
        }
    }
    public int Gear
    {
        get => gear;
        set
        {
            gear = value;
            ES3.Save(id + "gear", gear);
        }
    }
    public int Ticket
    {
        get => ticket;
        set
        {
            ticket = value;
            ES3.Save(id + "ticket", ticket);
        }
    }
    public Dictionary<int, int> Managers
    {
        get => managers;
        set
        {
            managers = new Dictionary<int, int>(value);
            ES3.Save(id + "managers", managers);
        }
    }
    public GameData() : base("game")
    {
        // playCount = 0;
        Load();
    }
    public override void Load()
    {
        playCount = ES3.Load(id + "game", 0);
        money = ES3.Load(id + "money", 100);
        gear = ES3.Load(id + "gear", 0);
        ticket = ES3.Load(id + "ticket", 0);
        gem = ES3.Load(id + "gem", 0);
        managers = ES3.Load(id + "managers", new Dictionary<int, int>());
    }
    public void FirstFillManagers()
    {
        for (int i = 0; i < 9; i++)
        {
            AddOrUpdate(Managers, i, 0);
            Managers = Managers;
        }
    }
    public void AddNewManager(int managerId)
    {
        // Managers.Add(managerId, Managers[managerId]);
        AddOrUpdate(Managers, managerId, 1);
        Managers = Managers;
    }
    void AddOrUpdate(Dictionary<int, int> dic, int key, int newValue)
    {
        if (dic.TryGetValue(key, out int val))
        {
            // yay, value exists!
            dic[key] = val + newValue;
        }
        else
        {
            // darn, lets add the value
            dic.Add(key, newValue);
        }
    }
}
