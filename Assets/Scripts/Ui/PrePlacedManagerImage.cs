using System.Collections;
using System.Collections.Generic;
using Tower.Floor;
using UnityEngine;

public class PrePlacedManagerImage : MonoBehaviour
{
    public GameObject assignButton;
    public ManagerButtonController managerButtonController;
    public LootItem myItem;
    public FloorMine myFloor;
    public void Init(LootItem item, ManagerButtonController managerButton)
    {
        myItem = item;
        managerButtonController = managerButton;
        transform.GetChild(item.managerId).gameObject.SetActive(true);
        transform.GetChild(item.managerId).name = item.managerId.ToString();
        var manager = transform.GetChild(item.managerId).GetComponent<SpawnedManagerImageController>();
        manager.myManagerButtonController = managerButton;
        myFloor.assignedManagerSpot = transform.GetChild(item.managerId).gameObject;
    }
}
