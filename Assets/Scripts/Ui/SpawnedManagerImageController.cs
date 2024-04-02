using Managers;
using UnityEngine;

public class SpawnedManagerImageController : MonoBehaviour
{
    public bool managerPlaced;
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
        managerPlaced = true;
    }
    public void ImagePressed(Vector2 clickPos, Transform clickedObj)
    {
        if (clickedObj != transform) return;
        Destroy(gameObject);
    }
}
