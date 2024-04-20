using System.Collections;
using System.Data.Common;
using GameStates;
using UnityEngine;
using Utils;

public class PlayerStats : Singleton<PlayerStats>
{
    public GameData data;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        data = GameStateManager.Instance.gameData;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            UpdateMoney(500);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            UpdateGem(50);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            UpdateTicket(5);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            UpdateGear(10);
    }
    public void UpdateMoney(int amount)
    {
        data.Money += amount;
    }
    public void UpdateGem(int amount)
    {
        data.Gem += amount;
    }
    public void UpdateTicket(int amount)
    {
        data.Ticket += amount;
    }
    public void UpdateGear(int amount)
    {
        data.Gear += amount;
    }
}
