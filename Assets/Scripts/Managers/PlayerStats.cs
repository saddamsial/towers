using System.Collections;
using System.Data.Common;
using DG.Tweening;
using GameStates;
using Managers;
using UnityEngine;
using Utils;

public class PlayerStats : Singleton<PlayerStats>
{
    public GameData data;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        data = GameStateManager.Instance.gameData;
        UpdateMoney(0);
        UpdateGem(0);
        // UpdateStep(0);
        // UpdateLevel(0);
        // UpdateTicket(0);
        // UpdateGear(0);
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
        if (Input.GetKeyDown(KeyCode.S))
            UpdateStep(1);
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
    public void UpdateStep(int amount)
    {
        data.Step += amount;
    }
    public void UpdateLevel(int amount)
    {
        data.Level += amount;
    }
    public void AddInGameMoney()
    {
        data.Money += UserInterfaceController.Instance.tempMoney;
    }
    public bool MoneyCheck(int money)
    {
        var result = money <= data.Money;
        if (!result) UserInterfaceController.Instance.moneyCount.GetComponent<DOTweenAnimation>().RecreateTweenAndPlay();
        else UpdateMoney(-money);
        return result;
    }
    public bool GemCheck(int gem)
    {
        var result = gem <= data.Gem;
        if (!result) UserInterfaceController.Instance.gemCount.GetComponent<DOTweenAnimation>().RecreateTweenAndPlay();
        else UpdateGem(-gem);
        return result;
    }
}
