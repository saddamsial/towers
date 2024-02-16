using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Data_and_Scriptable.GunSo;
using Managers;
using Tower;
using Tower.Floor;
using UnityEngine;

public class CameraSettingsController : MonoBehaviour
{
    public CinemachineTargetGroup targetGroup;
    public CinemachineVirtualCamera editCam;
    private CinemachineGroupComposer cinemachineGroupComposer;
    public Transform closeCameras;
    public GameObject zoomBackButton;
    int tempCameraNo;
    private void OnEnable()
    {
        GameController.onFloorAdded += FloorAdded;
    }
    private void OnDisable()
    {
        GameController.onFloorAdded -= FloorAdded;
    }
    private void Start()
    {
        cinemachineGroupComposer = editCam.GetCinemachineComponent<CinemachineGroupComposer>();

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
        if (tempCameraNo == no)
        {
            closeCameras.GetChild(no).gameObject.SetActive(false);
            tempCameraNo = -1;
            zoomBackButton.SetActive(false);
            return;
        }
        if (tempCameraNo > 0)
            closeCameras.GetChild(tempCameraNo).gameObject.SetActive(false);
        tempCameraNo = no;
        closeCameras.GetChild(no).gameObject.SetActive(true);
        zoomBackButton.SetActive(true);
    }

    public void BackNormalEditMode()
    {
        zoomBackButton.SetActive(false);
        closeCameras.GetChild(tempCameraNo).gameObject.SetActive(false);
        tempCameraNo = -1;
    }
}
