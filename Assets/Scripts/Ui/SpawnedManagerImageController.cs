using Managers;
using UnityEngine;

public class SpawnedManagerImageController : MonoBehaviour
{
    public bool managerPlaced;
    public ManagerButtonController myManagerButtonController;
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
        // ManagersPanel.Instance.tempManagerButtonController = myManagerButtonController;
        // myManagerButtonController.UpdateText(-1);
        managerPlaced = true;
    }
    public void ImagePressed(Vector2 clickPos, Transform clickedObj)
    {
        if (clickedObj != transform) return;
        tempPlacedSpot.gameObject.SetActive(true);
        ManagersPanel.Instance.tempManagerButtonController = myManagerButtonController;
        myManagerButtonController.UpdateText(1);
        Destroy(gameObject);
    }
}
