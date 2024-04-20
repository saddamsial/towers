using System;
using Data_and_Scriptable.BulletSo;
using NaughtyAttributes;
using UnityEngine;
[Serializable]
public enum LootEarnType
{
    normal = 0,
    levelup = 1,
    purchase = 2,
    gift = 3,
    store = 4
}
public enum LootType
{
    money,
    gem,
    gear,
    ticket,
    manager
}
[CreateAssetMenu(menuName = "Loot Item")]
public class LootItem : ScriptableObject
{
    public LootType lootType;
    public Sprite sprite;
    [HideIf("lootType", LootType.manager)]
    public int amount;
    [ShowIf("lootType", LootType.manager)]
    public int managerId;
    [ShowIf("lootType", LootType.manager)]
    public BulletTypes type;
}
