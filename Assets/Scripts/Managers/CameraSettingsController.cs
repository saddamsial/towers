using Cinemachine;
using Data_and_Scriptable.GunSo;
using Managers;
using Tower;
using Tower.Floor;
using UnityEngine;
using PathCreation;
using GameStates;

public class CameraSettingsController : MonoBehaviour
{
    public CinemachineTargetGroup targetGroup;
    public CinemachineVirtualCamera editCam, gameCam;
    private CinemachineGroupComposer cinemachineGroupComposer;
    public Transform closeCameras, followObj, lookObj;
    public GameObject zoomBackButton, closeCameraCanvas, managerButton;
    int tempCameraNo;
    public PathCreator followPath, lookPath;
    private void OnEnable()
    {
        GameController.onFloorAdded += FloorAdded;
        GameController.onCloseCameraPressed += CloseCameraButton;
    }
    private void OnDisable()
    {
        GameController.onFloorAdded -= FloorAdded;
        GameController.onCloseCameraPressed -= CloseCameraButton;
    }
    private void Start()
    {
        cinemachineGroupComposer = editCam.GetCinemachineComponent<CinemachineGroupComposer>();

    }

    public void ZoomLevel(int floorCount)
    {
        floorCount -= 2;
        var duration = floorCount <= 0 ? 0.99f : 1 - 1.0f / 8 * floorCount;
        followObj.position = followPath.path.GetPointAtTime(duration);
        lookObj.position = lookPath.path.GetPointAtTime(duration);
    }

    private void Update()
    {

    }

    public void FloorAdded(Transform floor, int whichFloor, TowerController mainTower, GunSo gun, FloorMine prevFloor)
    {
        targetGroup.AddMember(floor.transform.GetChild(1), 1 - (whichFloor * 0.05f), whichFloor == 0 ? 5 : 2);
        cinemachineGroupComposer.m_MaximumFOV = 50 - whichFloor;
    }

    public void CloseCameraButton(int no)
    {
        if (GameStateManager.Instance.IsManagerEditMode()) return;
        if (tempCameraNo == no)
        {
            BackNormalEditMode();
            return;
        }

        if (tempCameraNo >= 0)
            closeCameras.GetChild(tempCameraNo).gameObject.SetActive(false);
        tempCameraNo = no;
        GameController.Instance.currentFocusedGun = no;
        closeCameras.GetChild(no).gameObject.SetActive(true);
        zoomBackButton.SetActive(true);
        closeCameraCanvas.SetActive(true);
        managerButton.SetActive(false);
    }

    public void BackNormalEditMode()
    {
        GameController.onZoomOutFromGun?.Invoke();
        /*
            closeCameras.GetChild(no).gameObject.SetActive(false);
            tempCameraNo = -1;
            zoomBackButton.SetActive(false);
        */
        GameController.Instance.currentFocusedGun = -1;
        // closeCameraCanvas.SetActive(false);
        zoomBackButton.SetActive(false);
        managerButton.SetActive(true);
        closeCameras.GetChild(tempCameraNo).gameObject.SetActive(false);
        tempCameraNo = -1;
    }
}
