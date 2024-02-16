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
        Transform clickedObject;
        public LayerMask myLayer, enemyLayer;
        public CameraSettingsController cameraSettings;
        void Start()
        {
        }

        private void LateUpdate()
        {
            if (!Input.GetKeyDown(KeyCode.Mouse0)) return;

            if (GameStateManager.Instance.IsGameState())
            {
                clickedObject = GetWorldPositionOnPlane(Input.mousePosition, enemyLayer).transform;
                if (!clickedObject || towerController.selectedFloors.Count < 1) return;
                onTargetSet?.Invoke(clickedObject);
            }
            if (GameStateManager.Instance.IsEditState())
            {
                clickedObject = GetWorldPositionOnPlane(Input.mousePosition, myLayer).transform;
                if (!clickedObject) return;
                // Debug.Log(clickedObject.name);
                GameController.onCloseCameraPressed?.Invoke(towerController.floors.IndexOf(clickedObject.gameObject));
            }

        }

        private RaycastHit GetWorldPositionOnPlane(Vector3 screenPosition, LayerMask layer)
        {
            ray = mainCam.ScreenPointToRay(screenPosition);
            Physics.Raycast(ray, out hit, 100, layer);
            if (hit.transform)//&& hit.transform.gameObject.layer == layer)
            {
                return hit;
            }

            return demoHit;
        }


        public Vector3 GetPos(Vector3 screenPos)
        {
            ray = mainCam.ScreenPointToRay(screenPos);
            var a = new Plane(Vector3.up, Vector3.zero);
            a.Raycast(ray, out var distance);
            return ray.GetPoint(distance);
        }
    }
}