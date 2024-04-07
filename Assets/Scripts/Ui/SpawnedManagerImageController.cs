using Managers;
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
        // Debug.Log(index + " init");
        ManagersPanel.Instance.towerData.UpdateFloorManager(index, ManagersPanel.Instance.tempManagerButtonController.index);
        managerPlaced = true;
    }
    public void ImagePressed(Vector2 clickPos, Transform clickedObj)
    {
        if (clickedObj != transform) return;
        tempPlacedSpot.gameObject.SetActive(true);
        ManagersPanel.Instance.tempManagerButtonController = myManagerButtonController;
        myManagerButtonController.UpdateText(1);
        var index = int.Parse(gameObject.name);
        // Debug.Log(index + " remove");
        ManagersPanel.Instance.towerData.UpdateFloorManager(index, -1);
        Destroy(gameObject);
    }
}
