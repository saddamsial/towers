using Managers;
using Tower;
using Tower.Floor;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class SpawnedManagerImageController : MonoBehaviour
{
    public bool managerPlaced;
    public ManagerButtonController myManagerButtonController;
    public LootItem myManager;
    string[] splited;
    Transform tempPlacedSpot;
    public Image myImage;
    public FloorMine myFloor;
    void OnEnable()
    {
        GameController.onManagerImagePressed += ImagePressed;
        GameController.onManagerImageReleased += Init;
    }
    public void OnDisable()
    {
        GameController.onManagerImagePressed -= ImagePressed;
        GameController.onManagerImageReleased -= Init;
    }
    void Start()
    {

    }
    public void Init(Transform placedManager, Transform managerImage)
    {
        if (managerImage != transform) return;
        tempPlacedSpot = placedManager;
        splited = placedManager.name.Split(' ');
        var index = int.Parse(splited[0]);
        gameObject.name = index.ToString();
        GameController.onManagerCheck?.Invoke(-1);
        // ManagersPanel.Instance.towerData.UpdateFloorManager(index, ManagersPanel.Instance.tempManagerButtonController.index, -1, false);
        // Debug.Log(index + "--" + ManagersPanel.Instance.tempManagerButtonController.index);
        managerPlaced = true;
        myImage.overrideSprite = myManagerButtonController.image.sprite;
        // Debug.Log(myImage.sprite + "---" + myManagerButtonController.image.sprite);
        if (myFloor == null)
        {
            myFloor = ManagersPanel.Instance.mainTower.floorMineList[index];
        }
        ManagersPanel.Instance.mainTower.floorMineList[ManagersPanel.Instance.mainTower.floorMineList.IndexOf(myFloor)].SetupManager(index);//.isManagerAssigned = true;
        // ManagersPanel.Instance.mainTower.floorMineList[ManagersPanel.Instance.mainTower.floorMineList.IndexOf(myFloor)].myManagerId = index;
        ManagersPanel.Instance.towerData.UpdateFloorManager(ManagersPanel.Instance.mainTower.floorMineList.IndexOf(myFloor),
            ManagersPanel.Instance.tempManagerButtonController.index, -1, false);
        // Debug.Log(index + " init");
    }
    public void ImagePressed(Vector2 clickPos, Transform clickedObj)
    {
        if (clickedObj != transform) return;
        tempPlacedSpot?.gameObject.SetActive(true);
        ManagersPanel.Instance.tempManagerButtonController = myManagerButtonController;
        myManagerButtonController.UpdateText(1);
        var index = int.Parse(gameObject.name);
        myImage.overrideSprite = myManagerButtonController.image.sprite;
        GameController.onManagerCheck?.Invoke(index);
        // Debug.Log("  index-" + index + "    manager id-" + ManagersPanel.Instance.tempManagerButtonController.index + "    floor-" + myFloor);
        ManagersPanel.Instance.towerData.UpdateFloorManager(ManagersPanel.Instance.mainTower.floorMineList.IndexOf(myFloor),
            ManagersPanel.Instance.tempManagerButtonController.index, 1, true);
        ManagersPanel.Instance.mainTower.floorMineList[ManagersPanel.Instance.mainTower.floorMineList.IndexOf(myFloor)].managerSpot.SetActive(true);
        ManagersPanel.Instance.mainTower.floorMineList[ManagersPanel.Instance.mainTower.floorMineList.IndexOf(myFloor)].RemoveManager();//myManagerId = -1;
        // ManagersPanel.Instance.mainTower.floorMineList[ManagersPanel.Instance.mainTower.floorMineList.IndexOf(myFloor)].isManagerAssigned = false;
        gameObject.SetActive(false);
    }
}
