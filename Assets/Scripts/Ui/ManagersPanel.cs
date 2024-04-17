using System.Collections;
using System.Collections.Generic;
using GameStates;
using Managers;
using Tower;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class ManagersPanel : Singleton<ManagersPanel>
{
    public GameData gameData;
    public TowerData towerData;
    public List<LootItem> managerItems = new();
    public List<ManagerButtonController> managerButtons = new();
    public Transform tempManagerImage;
    public GameObject managerImagePrefab, managerButtonPrefab, tempButton;
    public Canvas managerCanvas;
    public Transform managerImageUiPanel;
    public TowerController mainTower;
    public ManagerButtonController tempManagerButtonController;

    void OnEnable()
    {
        GameController.onManagerImagePressed += SpawnManagerImage;
        GameController.onManagerImageReleased += SpawnedImagePlaced;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        GameController.onManagerImagePressed -= SpawnManagerImage;
        GameController.onManagerImageReleased -= SpawnedImagePlaced;
    }
    void Start()
    {
        gameData = GameStateManager.Instance.gameData;
        towerData = mainTower.data;
    }
    public void RePlaceManagers()
    {
        for (int i = 0; i < mainTower.floorMineList.Count; i++)
        {
            if (towerData.FloorManagers[i] != -1)
            {
                // Debug.Log("****   " + towerData.FloorManagers[i]);
                mainTower.floorMineList[i].managerSpot.gameObject.SetActive(false);
                mainTower.floorMineList[i].prePlacedManagerImage.Init(managerItems[towerData.FloorManagers[i]], managerButtons[towerData.FloorManagers[i]]);
            }
            mainTower.floorMineList[i].prePlacedManagerImage.myFloor = mainTower.floorMineList[i];
        }
        // for (int i = 0; i < 9; i++)
        // {
        //     // Debug.Log(i + "  " + towerData.FloorManagers[i]);
        //     if (towerData.FloorManagers[i] != -1)
        //     {
        //         // tempManagerImage = Instantiate(managerImagePrefab, mainTower.floorMineList[i].managerSpot.transform.position, Quaternion.identity, transform).transform;
        //         // tempManagerImage.localRotation = Quaternion.identity;
        //         // SpawnedImagePlaced(mainTower.floorMineList[i].managerSpot.transform, tempManagerImage);
        //     }
        // }
    }
    public void Init()
    {
        gameData = GameStateManager.Instance.gameData;//(GameData)DataPersistenceController.Instance.GetData("game", new GameData());
        towerData = mainTower.data;
        for (int i = 0; i < managerImageUiPanel.childCount; i++)
        {
            Destroy(managerImageUiPanel.GetChild(i).gameObject);
        }
        foreach (var managers in gameData.Managers)
        {
            SpawnButtons(managers.Key);
        }
    }
    public void SpawnButtons(int key)
    {
        tempButton = Instantiate(managerButtonPrefab, managerImageUiPanel);
        var controller = tempButton.GetComponent<ManagerButtonController>();
        managerButtons.Add(controller);
        tempButton.transform.localRotation = Quaternion.identity;
        controller.Init(managerItems[key]);
    }
    //bottom-input methods
    public void SpawnManagerImage(Vector2 pos, Transform clickedObj)
    {
        if (tempManagerButtonController && tempManagerButtonController.GetCount() <= 0) return;

        tempManagerImage = Instantiate(managerImagePrefab, pos, Quaternion.identity, transform).transform;
        tempManagerImage.localRotation = Quaternion.identity;
        tempManagerImage.localScale = Vector3.one * 1.1f;
        // Debug.Log("obj:   " + clickedObj, clickedObj);
        // tempManagerImage.GetComponent<SpawnedManagerImageController>().myFloor = clickedObj.

        StartCoroutine(AssignDelay());
        InputController.Instance.spawnedManagerImage = tempManagerImage;
    }
    IEnumerator AssignDelay()
    {
        yield return new WaitForSeconds(0.1f);
        tempManagerImage.GetComponent<SpawnedManagerImageController>().myManagerButtonController = tempManagerButtonController;
        tempManagerImage.GetComponent<Image>().sprite = tempManagerButtonController.image.sprite;
    }
    public void SpawnedImagePlaced(Transform spot, Transform managerImage)
    {
        //this is for placing in screen space canvas
        // tempManagerImage.GetComponent<SpawnedManagerImageController>().myManagerButtonController = managerImage.GetComponentInParent<ManagerButtonController>();
        tempManagerImage.SetParent(managerCanvas.transform);
        tempManagerImage.localRotation = Quaternion.identity;
        tempManagerImage.localScale = Vector3.one * 3;
        tempManagerImage.position = Camera.main.WorldToScreenPoint(spot.position);
        //this is replacing from main canvas to camera space canvas 
        tempManagerImage.position = Camera.main.ScreenToWorldPoint(tempManagerImage.position);
        tempManagerImage.SetParent(transform.parent);
        tempManagerImage.localRotation = Quaternion.identity;
        tempManagerImage.localScale = Vector3.one * 7;
        // tempManagerImage.gameObject.SetActive(false);
        Destroy(tempManagerImage.gameObject);

        spot.transform.parent.GetComponentInChildren<PrePlacedManagerImage>().Init(tempManagerButtonController.tempItem, tempManagerButtonController);
    }
}
