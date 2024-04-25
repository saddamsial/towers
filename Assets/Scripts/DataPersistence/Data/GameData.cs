using System;
using System.Collections.Generic;

[ES3Serializable]
public class GameData : Data
{
    public Action<int> onMoneyUpdated, onGemUpdated, onGearUpdated, onTicketUpdated, onStepUpdated, onLevelUpdated, onGameCountUpdated;
    private int lootCount, playCount, money, gem, gear, ticket, level, step, enemyLevel;
    public Dictionary<int, int> managers = new();
    public int Step
    {
        get => step;
        set
        {
            step = value;
            ES3.Save(id + "step", step);
            onStepUpdated?.Invoke(Step);
            // Debug.Log("step: " + Step);
        }
    }
    public int Level
    {
        get => level;
        set
        {
            level = value;
            ES3.Save(id + "level", level);
            onLevelUpdated?.Invoke(Level);
            // Debug.Log("level: " + Level);
        }
    }
    public int EnemyLevel
    {
        get => enemyLevel;
        set
        {
            enemyLevel = value;
            ES3.Save(id + "enemyLevel", enemyLevel);
        }
    }
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
            onGameCountUpdated?.Invoke(PlayCount);
        }
    }
    public int Money
    {
        get => money;
        set
        {
            money = value;
            ES3.Save(id + "money", money);
            onMoneyUpdated?.Invoke(Money);
            // Debug.Log("money: " + Money);
        }
    }
    public int Gem
    {
        get => gem;
        set
        {
            gem = value;
            ES3.Save(id + "gem", gem);
            onGemUpdated?.Invoke(Gem);
        }
    }
    public int Gear
    {
        get => gear;
        set
        {
            gear = value;
            ES3.Save(id + "gear", gear);
            onGearUpdated?.Invoke(Gear);
        }
    }
    public int Ticket
    {
        get => ticket;
        set
        {
            ticket = value;
            ES3.Save(id + "ticket", ticket);
            onTicketUpdated?.Invoke(Ticket);
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
        Load();
    }
    public override void Load()
    {
        playCount = ES3.Load(id + "PlayCount", 0);
        money = ES3.Load(id + "money", 100);
        gear = ES3.Load(id + "gear", 0);
        ticket = ES3.Load(id + "ticket", 0);
        gem = ES3.Load(id + "gem", 0);
        step = ES3.Load(id + "step", 0);
        level = ES3.Load(id + "level", 0);
        enemyLevel = ES3.Load(id + "enemyLevel", 0);
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
    public void AddNewManager(int managerId, int value)
    {
        AddOrUpdate(Managers, managerId, value);
        Managers = Managers;
        ManagersPanel.Instance.Init();
    }

    void AddOrUpdate(Dictionary<int, int> dic, int key, int newValue)
    {
        if (dic.TryGetValue(key, out int val))
        {
            dic[key] = val + newValue;
            // Debug.Log("---  " + key + "." + dic[key]);
        }
        else
        {
            dic.Add(key, newValue);
            // Debug.Log("---  " + key + ".." + dic[key]);
        }
    }
}
