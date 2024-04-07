using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrePlacedManagerImage : MonoBehaviour
{
    public GameObject assignButton;
    public ManagerButtonController managerButtonController;
    public LootItem myItem;
    public void Init(LootItem item)
    {
        myItem = item;
        transform.GetChild(item.managerId).gameObject.SetActive(true);
    }
}
