using Managers;
using UnityEngine;
using Utils;
using Utils.PoolSystem;

public class ManagersPanel : Singleton<ManagersPanel>
{
    public GameData gameData;
    public Transform tempManagerImage;
    public GameObject managerImagePrefab;
    public Canvas managerCanvas;

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
        gameData = (GameData)DataPersistenceController.Instance.GetData("game", new GameData());


    }

    public void SpawnManagerImage(Vector2 pos)
    {
        tempManagerImage = managerImagePrefab.Spawn(pos, Quaternion.identity, transform).transform;
        tempManagerImage.localRotation = Quaternion.identity;
        tempManagerImage.localScale = Vector3.one * 4;
        InputController.Instance.spawnedManagerImage = tempManagerImage;
    }

    public void SpawnedImagePlaced(Transform spot)
    {
        tempManagerImage.SetParent(managerCanvas.transform);
        tempManagerImage.localRotation = Quaternion.identity;
        tempManagerImage.localScale = Vector3.one * 3;
        tempManagerImage.position = Camera.main.WorldToScreenPoint(spot.position);
    }


}
