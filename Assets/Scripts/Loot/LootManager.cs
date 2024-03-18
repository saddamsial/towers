using System.Collections.Generic;
using UnityEngine;
using Utils;
using UnityEngine.UI;

public class LootManager : Singleton<LootManager>
{
    public GameData data;
    public GamePresets gamePresets;
    public GameObject lootPanel;

    void Start()
    {
        data = (GameData)DataPersistenceController.Instance.GetData("game", new GameData());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            NewLoot();
        }
    }

    public void NewLoot()
    {
        var currentLoot = gamePresets.LootList[data.lootCount].loot;
        // for (int i = 0; i < currentLoot.Count; i++)
        // {
        //     Debug.Log(currentLoot[i].name);
        // }
    }

    public void Skip()
    {

    }
}
