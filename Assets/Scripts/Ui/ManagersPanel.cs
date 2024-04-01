using Managers;
using UnityEngine;
using Utils;
using Utils.PoolSystem;

public class ManagersPanel : Singleton<ManagersPanel>
{
    public GameData gameData;
    public Transform tempManagerImage;
    public GameObject managerImagePrefab;

    void OnEnable()
    {
        GameController.onManagerImagePressed += SpawnManagerImage;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        GameController.onManagerImagePressed -= SpawnManagerImage;
    }
    void Start()
    {
        gameData = (GameData)DataPersistenceController.Instance.GetData("game", new GameData());


    }

    public void SpawnManagerImage(Vector2 pos)
    {

        tempManagerImage = managerImagePrefab.Spawn(pos, Quaternion.identity, transform).transform;
        tempManagerImage.localRotation = Quaternion.identity;
        InputController.Instance.spawnedManagerImage = tempManagerImage;
    }
}
