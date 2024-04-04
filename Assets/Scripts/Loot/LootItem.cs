using System.Collections;
using System.Collections.Generic;
using Data_and_Scriptable.BulletSo;
using Data_and_Scriptable.GunSo;
using NaughtyAttributes;
using UnityEngine;

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
