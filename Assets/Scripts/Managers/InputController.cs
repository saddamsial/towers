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

        void Start()
        {
        }

        private void LateUpdate()
        {
            if (!Input.GetKeyDown(KeyCode.Mouse0) ||
                !GameStateManager.Instance.IsGameState()) return;
            var enemyTowerTarget = GetWorldPositionOnPlane(Input.mousePosition).transform;
            if (!enemyTowerTarget || towerController.selectedFloors.Count < 1) return;
            onTargetSet?.Invoke(enemyTowerTarget);
        }

        private RaycastHit GetWorldPositionOnPlane(Vector3 screenPosition)
        {
            ray = mainCam.ScreenPointToRay(screenPosition);
            Physics.Raycast(ray, out hit, 100);
            if (hit.transform && hit.transform.gameObject.layer == LayerMask.NameToLayer("enemy"))
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