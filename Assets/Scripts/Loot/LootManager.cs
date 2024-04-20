using System.Collections.Generic;
using UnityEngine;
using Utils;
using UnityEngine.UI;
using TMPro;
using Utils.PoolSystem;
using DG.Tweening;
using GameStates;
public class LootManager : Singleton<LootManager>
{
    public GameData data;
    public GamePresets gamePresets;
    public GameObject lootPanel, chestOpen, chestClose, step1, step2, step3, deck, spawnedObj;
    public Transform spawnPos, moveToPos, tempObj, gridView;
    public TMP_Text deckCount;
    public int remainingLootCount;
    public List<LootItem> currentLoot = new();
    public List<Transform> spawnedLootItems = new();

    void Start()
    {
        data = GameStateManager.Instance.gameData;//(GameData)DataPersistenceController.Instance.GetData("game", new GameData());
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Alpha2))
        // {
        //     NewLoot();
        // }
        if (Input.GetKeyDown(KeyCode.M))
        {
            data.AddNewManager(Random.Range(0, 9), 1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("gem: " + data.Gem);
            Debug.Log("gear: " + data.Gear);
            Debug.Log("ticket: " + data.Ticket);
            Debug.Log("money: " + data.Money);
            for (int i = 0; i < data.Managers.Count; i++)
            {
                Debug.Log("manager: " + data.Managers[i]);
            }
            Debug.Log("loot: " + data.LootCount);
        }
    }

    LootEarnType tempEarnType;
    public void NewLoot(int earnType)
    {
        gridView.gameObject.SetActive(false);
        step3.SetActive(false);
        lootPanel.SetActive(true);
        TaptoCollect(false);
        tempEarnType = (LootEarnType)earnType;
        currentLoot = LootReturn((LootEarnType)earnType);//gamePresets.LootList[data.LootCount].loot;
        remainingLootCount = currentLoot.Count;
        deckCount.text = remainingLootCount + "";
    }

    public List<LootItem> LootReturn(LootEarnType type) => type switch
    {
        LootEarnType.normal => gamePresets.LootList[data.LootCount].loot,
        LootEarnType.levelup => gamePresets.LevelUpLoots[Random.Range(0, gamePresets.LevelUpLoots.Count)].loot,
        _ => gamePresets.LootList[data.LootCount].loot,
    };
    public void Skip()
    {
        if (remainingLootCount != 0)
            for (int a = 0; a <= remainingLootCount; a++)
            {
                SpawnLootObject(true);
            }
        gridView.gameObject.SetActive(true);
        step1.SetActive(false);
        step2.SetActive(false);
        chestClose.transform.parent.gameObject.SetActive(false);
        deck.SetActive(false);
        for (int i = 0; i < spawnedLootItems.Count; i++)
        {
            var obj = spawnedLootItems[i];
            obj.gameObject.SetActive(true);
            obj.SetParent(gridView);
            var rectTransform = obj.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(200, 200);
        }
        step3.SetActive(true);
    }
    public void TaptoCollect(bool open)
    {
        chestClose.transform.parent.gameObject.SetActive(true);
        step1.SetActive(!open);
        step2.SetActive(open);
        chestClose.SetActive(!open);
        chestOpen.SetActive(open);
        deck.SetActive(open);
        if (open) SpawnLootObject();
        else ClearList();
    }
    public void TapForNextItem()
    {
        if (remainingLootCount <= 0) { Skip(); return; }
        tempObj.gameObject.SetActive(false);
        SpawnLootObject();
    }
    public void Claim()
    {
        for (int i = 0; i < currentLoot.Count; i++)
        {
            switch (currentLoot[i].lootType)
            {
                case LootType.manager:
                    data.AddNewManager(currentLoot[i].managerId, 1);
                    break;
                case LootType.gear:
                    data.Gear += currentLoot[i].amount;
                    break;
                case LootType.gem:
                    data.Gem += currentLoot[i].amount;
                    break;
                case LootType.money:
                    data.Money += currentLoot[i].amount;
                    break;
                case LootType.ticket:
                    data.Ticket += currentLoot[i].amount;
                    break;
            }

        }
        data.LootCount++;
        if (tempEarnType == LootEarnType.levelup)
        {
            data.Level++;
        }
        lootPanel.SetActive(false);
    }
    private void ClearList()
    {
        for (int i = 0; i < spawnedLootItems.Count; i++)
        {
            spawnedLootItems[0].gameObject.SetActive(false);
            spawnedLootItems.RemoveAt(0);
        }
        if (spawnedLootItems.Count > 0)
            ClearList();
    }
    RectTransform rectTransform;
    private void SpawnLootObject(bool skiped = false)
    {
        remainingLootCount--;
        deckCount.text = remainingLootCount + "";
        tempObj = spawnedObj.Spawn(spawnPos.position, Quaternion.identity, lootPanel.transform).transform;
        tempObj.localScale = Vector3.one * 0.2f;
        rectTransform = tempObj.GetComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(0, 0);//rectTransform.offsetMin.x
        rectTransform.offsetMax = new Vector2(0, 0);//rectTransform.offsetMax.x
        spawnedLootItems.Add(tempObj);
        var amountText = tempObj.GetChild(1).GetComponent<TMP_Text>();
        amountText.text = "";
        if (!skiped)
        {
            tempObj.DOScale(1.2f * Vector3.one, 1.6f).SetEase(Ease.InOutBounce);
            tempObj.DOMove(moveToPos.position, 1.6f, true).SetEase(Ease.InBounce).OnComplete(() =>
            {
                amountText.text = currentLoot[remainingLootCount].amount > 0 ? currentLoot[remainingLootCount].amount + "" : "";
                // rectTransform.offsetMin = new Vector2(0, 0);//rectTransform.offsetMin.x
                // rectTransform.offsetMax = new Vector2(0, 0);//rectTransform.offsetMax.x;
            });

        }
        else
        {
            tempObj.position = moveToPos.position;
            amountText.text = amountText.text = currentLoot[remainingLootCount].amount > 0 ? currentLoot[remainingLootCount].amount + "" : "";
            tempObj.localScale = 1.2f * Vector3.one;
        }
        tempObj.GetChild(0).GetComponent<Image>().sprite = currentLoot[remainingLootCount].sprite;
    }
}