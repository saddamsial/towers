using System;
using System.Collections;
using System.Collections.Generic;
using GameStates;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

[Serializable]
public enum ConditionType
{
    none,
    level,
    step,
    money,
    gem,
    gamecount,
    lootCount,
    enemyLevel
}
public class Visibility : MonoBehaviour
{
    public bool startVisible;
    GameData gameData;

    #region renderer
    [BoxGroup("Renderer")]
    public List<Object> myRendererList = new();
    #endregion

    #region conditions
    [BoxGroup("Condition")]
    public ConditionType conditionType;
    [BoxGroup("Condition")]
    [ShowIf("conditionType", ConditionType.level)]
    public int desiredLevel;
    [BoxGroup("Condition")]
    [ShowIf("conditionType", ConditionType.step)]
    public int desiredStep;
    [BoxGroup("Condition")]
    [ShowIf("conditionType", ConditionType.money)]
    public int desiredMoney;
    [BoxGroup("Condition")]
    [ShowIf("conditionType", ConditionType.gem)]
    public int desiredGem;
    [BoxGroup("Condition")]
    [ShowIf("conditionType", ConditionType.gamecount)]
    public int desiredGameCount;
    [BoxGroup("Condition")]
    [ShowIf("conditionType", ConditionType.lootCount)]
    public int desiredLootCount;
    [BoxGroup("Condition")]
    [ShowIf("conditionType", ConditionType.enemyLevel)]
    public int desiredEnemyLevel;

    [BoxGroup("Condition")]
    [Label("Show or Hide")]
    public bool conditionAction;
    #endregion
    IEnumerator Start()
    {
        SetVisible(startVisible);
        yield return new WaitForSeconds(0.1f);
        gameData ??= GameStateManager.Instance.gameData;

        Init();
        VisibilityManager.Instance.AddToList(conditionType, this);
    }
    private void OnEnable()
    {
        gameData ??= GameStateManager.Instance.gameData;
        if (gameData != null)
            Init();
    }
    public void Init()
    {
        if (conditionType == ConditionType.money)
        {
            gameData.onMoneyUpdated += CheckMoney;
            CheckMoney(gameData.Money);
        }
        else if (conditionType == ConditionType.gem)
        {
            gameData.onGemUpdated += CheckGem;
            CheckGem(gameData.Gem);
        }
        else if (conditionType == ConditionType.step)
        {
            gameData.onStepUpdated += CheckStep;
            CheckStep(gameData.TotalStep);
        }
        else if (conditionType == ConditionType.level)
        {
            gameData.onLevelUpdated += CheckLevel;
            CheckLevel(gameData.Level);
        }
        else if (conditionType == ConditionType.lootCount)
        {
            gameData.onLootCountUpdated += CheckLootCount;
            CheckLootCount(gameData.LootCount);
        }
        else if (conditionType == ConditionType.gamecount)
        {
            gameData.onGameCountUpdated += CheckGameCount;
            CheckGameCount(gameData.PlayCount);
        }
        else if (conditionType == ConditionType.enemyLevel)
        {
            gameData.onEnemyLevelUpdated += CheckEnemyLevel;
            CheckEnemyLevel(gameData.EnemyLevel);
        }

    }
    void OnDisable()
    {
        if (VisibilityManager.Instance && VisibilityManager.Instance.CheckContains(this).visibility != null)
        {
            VisibilityManager.Instance.RemoFromList(this);
        }
        if (conditionType == ConditionType.money)
            gameData.onMoneyUpdated -= CheckMoney;
        else if (conditionType == ConditionType.gem)
            gameData.onGemUpdated -= CheckGem;
        else if (conditionType == ConditionType.step)
            gameData.onStepUpdated -= CheckStep;
        else if (conditionType == ConditionType.level)
            gameData.onLevelUpdated -= CheckLevel;
        else if (conditionType == ConditionType.lootCount)
            gameData.onLevelUpdated -= CheckLootCount;
        else if (conditionType == ConditionType.gamecount)
            gameData.onGameCountUpdated -= CheckGameCount;
        else if (conditionType == ConditionType.enemyLevel)
            gameData.onEnemyLevelUpdated -= CheckEnemyLevel;
    }
    private void CheckMoney(int amount)
    {
        if (amount >= desiredMoney)
            SetVisible(conditionAction);
    }
    private void CheckGem(int amount)
    {
        if (amount >= desiredGem)
            SetVisible(conditionAction);
    }
    private void CheckStep(int amount)
    {
        amount = gameData.TotalStep;
        if (amount >= desiredStep)
            SetVisible(conditionAction);
    }
    private void CheckLevel(int amount)
    {
        if (amount >= desiredLevel)
            SetVisible(conditionAction);
    }
    private void CheckLootCount(int amount)
    {
        if (amount >= desiredLootCount)
            SetVisible(conditionAction);
    }
    private void CheckGameCount(int amount)
    {
        if (amount >= desiredGameCount)
            SetVisible(conditionAction);
    }
    private void CheckEnemyLevel(int amount)
    {
        if (amount >= desiredEnemyLevel)
            SetVisible(conditionAction);
    }

    public void SetVisible(bool val)
    {
        foreach (var obj in myRendererList)
        {
            var go = obj as GameObject;
            if (go.TryGetComponent(out Image c))
            {
                c.enabled = val;
            }
            else if (go.TryGetComponent(out Renderer r))
            {
                r.enabled = val;
            }
            else if (go.TryGetComponent(out TMP_Text t))
            {
                t.enabled = val;
            }
        }
    }
}

