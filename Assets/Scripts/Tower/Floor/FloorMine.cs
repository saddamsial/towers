using System.Collections;
using System.Collections.Generic;
using Data_and_Scriptable.GunSo;
using GameStates;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Tower.Floor
{
    public class FloorMine : FloorBase
    {
        public Button addFloorButton;
        public GameObject upgradeButton;
        public GraphicRaycaster myCanvasRaycaster;
        public override void OnEnable()
        {
            GameController.OnDied += Die;
            InputController.Instance.onTargetSet += Attack;
            GameController.onFloorAdded += FloorAdded;
            GameController.onEditMode += OnEditMode;
            GameController.swapGun += SwapGun;

            addFloorButton.onClick.RemoveAllListeners();
        }
        public override void OnDisable()
        {
            GameController.OnDied -= Die;
            GameController.onFloorAdded -= FloorAdded;
            GameController.onEditMode -= OnEditMode;
            GameController.swapGun -= SwapGun;

            if (InputController.Instance)
                InputController.Instance.onTargetSet -= Attack;
        }
        public override void Attack(Transform target)
        {
            if (!mainTower.selectedFloors.Contains(transform)) return;
            attackTo = target;
            attachedGun.RotateToTarget();
            attachedGun.canShoot = true;
        }
        public override void Die(FloorBase diedObj)
        {
            base.Die(diedObj);
            if (diedObj.transform == attackTo)
            {
                if (attachedGun.isLaser)
                {
                    attachedGun.GetComponent<LaserGun>().DamageState(false, true);
                }
                attackTo = null;
                attachedGun.canShoot = false;
                attachedGun.ResetRotation();
            }
        }
        public void FloorAdded(Transform floor, int whichFloor, TowerController mainTower, GunSo gun, FloorMine previousFloor)
        {
            if (transform != floor) return;
            this.mainTower = mainTower;
            var isEdit = GameStateManager.Instance.IsEditState();
            if (previousFloor != null)
            {
                // previousFloor.myCanvasRaycaster.enabled = false;
                previousFloor.addFloorButton.gameObject.SetActive(false);
            }
            addFloorButton.onClick.RemoveAllListeners();
            addFloorButton.onClick.AddListener(mainTower.AddNewFloor);
            if (whichFloor < mainTower.gamePresets.maxPossibleFloor - 1) addFloorButton.gameObject.SetActive(isEdit);
            upgradeButton.SetActive(isEdit);
            AttachGun(gun.myPrefab);
            myCanvasRaycaster.enabled = isEdit;
        }
        public void OnEditMode(bool state)
        {
            upgradeButton.SetActive(state);
            myCanvasRaycaster.enabled = state;
            if (mainTower.floors[^1].transform == transform && mainTower.floors.Count < mainTower.gamePresets.maxPossibleFloor - 1)
                addFloorButton.gameObject.SetActive(state);
            attachedGun.ResetRotation();
        }
        public void UpgradeButton()
        {
            Debug.Log("upgrade pressed", gameObject);
        }
        public void SwapGun(GameObject newGun = null)
        {
            if (mainTower.floors.IndexOf(gameObject) != GameController.Instance.currentFocusedGun) return;
            Debug.Log("this gun");
            AttachGun(newGun);
        }
    }
}