using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ManagerButtonController : MonoBehaviour
{
    public Image image;
    public TMP_Text count;
    public int index;
    public LootItem tempItem;
    public void Init(LootItem item)
    {
        tempItem = item;
        image.sprite = item.sprite;
        index = item.managerId;
        ManagersPanel.Instance.gameData.Managers.TryGetValue(item.managerId, out var a);
        count.text = "" + a;
    }
    public void UpdateText(int val)
    {
        var a = int.Parse(count.text);
        a += val;
        count.text = "" + a;
    }
    public int GetCount()
    {
        return int.Parse(count.text);
    }
}
