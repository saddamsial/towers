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
    public void Init(LootItem item, int id)
    {
        image.sprite = item.sprite;
        ManagersPanel.Instance.gameData.Managers.TryGetValue(id, out var a);
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
