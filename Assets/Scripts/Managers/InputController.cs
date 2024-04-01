using System;
using GameStates;
using Tower;
using UnityEngine;
using Utils;

namespace Managers
{
    public class InputController : Singleton<InputController>
    {
        public Action<Transform> onTargetSet;
        [SerializeField] private Camera mainCam;
        private RaycastHit demoHit;
        private RaycastHit hit;
        private Ray ray;
        [SerializeField] private TowerController towerController;
        Transform clickedObject, releasedObject;
        public Transform spawnedManagerImage;
        public LayerMask myLayer, enemyLayer, uiLayer;
        public CameraSettingsController cameraSettings;
        void Start()
        {
        }

        private void LateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (GameStateManager.Instance.IsGameState())
                {
                    clickedObject = GetWorldPositionOnPlane(Input.mousePosition, enemyLayer).transform;
                    if (!clickedObject || towerController.selectedFloors.Count < 1) return;
                    onTargetSet?.Invoke(clickedObject);
                }
                else if (GameStateManager.Instance.IsManagerEditMode())
                {
                    clickedObject = GetWorldPositionOnPlane(Input.mousePosition, uiLayer).transform;//UiRaycast().transform;
                    if (!clickedObject) return;
                    GameController.onManagerImagePressed?.Invoke(GetPos(Input.mousePosition, 19.5f));
                }
                else if (GameStateManager.Instance.IsEditState())
                {
                    clickedObject = GetWorldPositionOnPlane(Input.mousePosition, myLayer).transform;
                    if (!clickedObject) return;
                    GameController.onCloseCameraPressed?.Invoke(towerController.floors.IndexOf(clickedObject.gameObject));
                }
            }
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (GameStateManager.Instance.IsManagerEditMode())
                {
                    if (!spawnedManagerImage) return;
                    spawnedManagerImage.position = GetPos(Input.mousePosition, 19.5f);
                }
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (GameStateManager.Instance.IsEditState())
                {
                    releasedObject = GetWorldPositionOnPlane(Input.mousePosition, myLayer).transform;
                    GameController.onManagerImageReleased?.Invoke(clickedObject);
                }
            }
        }
        private RaycastHit GetWorldPositionOnPlane(Vector3 screenPosition, LayerMask layer)
        {
            ray = mainCam.ScreenPointToRay(screenPosition);
            Physics.Raycast(ray, out hit, 1000, layer);
            Debug.DrawRay(ray.origin, ray.direction, Color.red, 1, true);
            if (hit.transform)
            {
                return hit;
            }
            return demoHit;
        }
        public Vector3 GetPos(Vector3 screenPos, float z)
        {
            ray = mainCam.ScreenPointToRay(screenPos);
            var a = new Plane(mainCam.transform.TransformDirection(Vector3.forward), z);
            //Plane a = new(Vector3.forward, new Vector3(0, 0, z));
            a.Raycast(ray, out var distance);
            return ray.GetPoint(distance);
        }

        // public RaycastHit UiRaycast()
        // {
        //     ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     Physics.Raycast(ray, out hit, 1000);
        //     Debug.DrawRay(ray.origin, ray.direction, Color.red, 1, true);

        //     if (hit.transform == null) return demoHit;

        //     Debug.Log(hit.transform);
        //     return hit;

        // }
    }
}


/*
 if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (GameStateManager.Instance.IsGameState())
                {
                    clickedObject = GetWorldPositionOnPlane(Input.mousePosition, enemyLayer).transform;
                    if (!clickedObject || towerController.selectedFloors.Count < 1) return;
                    onTargetSet?.Invoke(clickedObject);
                }
                if (GameStateManager.Instance.IsManagerEditMode())
                {
                    clickedObject = GetWorldPositionOnPlane(Input.mousePosition, myLayer).transform;
                    if (!clickedObject) return;
                    GameController.onManagerImagePressed?.Invoke(clickedObject.position);
                }
                else if (GameStateManager.Instance.IsEditState())
                {
                    clickedObject = GetWorldPositionOnPlane(Input.mousePosition, myLayer).transform;
                    if (!clickedObject) return;
                    GameController.onCloseCameraPressed?.Invoke(towerController.floors.IndexOf(clickedObject.gameObject));
                }
            }

            if (GameStateManager.Instance.IsEditState())
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    if (!spawnedManagerImage) return;
                    spawnedManagerImage.position = Input.mousePosition;

                }
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    releasedObject = GetWorldPositionOnPlane(Input.mousePosition, myLayer).transform;
                    GameController.onManagerImageReleased?.Invoke(clickedObject);

                }
            }
*/
