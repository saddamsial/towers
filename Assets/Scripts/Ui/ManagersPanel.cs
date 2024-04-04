using System.Collections;
using System.Collections.Generic;
using GameStates;
using Managers;
using Tower;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;
using Utils.PoolSystem;

public class ManagersPanel : Singleton<ManagersPanel>
{
    public GameData gameData;
    public TowerData towerData;
    public List<LootItem> managerItems = new();
    public Transform tempManagerImage;
    public GameObject managerImagePrefab, managerButtonPrefab, tempButton;
    public Canvas managerCanvas;
    public Transform managerImageUiPanel;
    public TowerController mainTower;

    void OnEnable()
    {
        GameController.onManagerImagePressed += SpawnManagerImage;
        GameController.onManagerImageReleased += SpawnedImagePlaced;
        gameData = GameStateManager.Instance.gameData;//(GameData)DataPersistenceController.Instance.GetData("game", new GameData());
        towerData = mainTower.data;//(TowerData)DataPersistenceController.Instance.GetData("tower", new TowerData());
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        GameController.onManagerImagePressed -= SpawnManagerImage;
        GameController.onManagerImageReleased -= SpawnedImagePlaced;
    }
    void Start()
    {


    }
    public void Init()
    {
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
        tempButton.transform.localRotation = Quaternion.identity;
        tempButton.GetComponent<ManagerButtonController>().Init(managerItems[key], key);
    }
    //bottom-input methods

    public void SpawnManagerImage(Vector2 pos, Transform clickedObj)
    {
        if (tempManagerButtonController && tempManagerButtonController.GetCount() <= 0) return;

        tempManagerImage = Instantiate(managerImagePrefab, pos, Quaternion.identity, transform).transform;
        tempManagerImage.localRotation = Quaternion.identity;
        tempManagerImage.localScale = Vector3.one * 1.1f;
        StartCoroutine(AssignDelay());
        InputController.Instance.spawnedManagerImage = tempManagerImage;
    }
    IEnumerator AssignDelay()
    {
        yield return new WaitForSeconds(0.1f);
        tempManagerImage.GetComponent<SpawnedManagerImageController>().myManagerButtonController = tempManagerButtonController;
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
    }
    public ManagerButtonController tempManagerButtonController;
}
