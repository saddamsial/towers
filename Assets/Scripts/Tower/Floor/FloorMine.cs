using System.Collections;
using System.Collections.Generic;
using Data_and_Scriptable.GunSo;
using GameStates;
using Guns;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Tower.Floor
{
    public class FloorMine : FloorBase
    {
        public bool isTargeted;
        public Button addFloorButton;
        public GameObject upgradeButton;
        public GameObject managerSpot;
        public GraphicRaycaster myCanvasRaycaster;
        public override void OnEnable()
        {
            GameController.onDied += Die;
            InputController.Instance.onTargetSet += Attack;
            GameController.onFloorAdded += FloorAdded;
            GameController.onEditMode += OnEditMode;
            GameController.onSwapGun += SwapGun;
            GameController.onManagerEditMode += ManagerPanel;

            addFloorButton.onClick.RemoveAllListeners();
        }
        public override void OnDisable()
        {
            GameController.onDied -= Die;
            GameController.onFloorAdded -= FloorAdded;
            GameController.onEditMode -= OnEditMode;
            GameController.onSwapGun -= SwapGun;
            GameController.onManagerEditMode -= ManagerPanel;

            if (InputController.Instance)
                InputController.Instance.onTargetSet -= Attack;
        }
        public override void Attack(Transform target)
        {
            if (!mainTower.selectedFloors.Contains(transform) || !attachedGun) return;
            attackTo = target;
            attachedGun.RotateToTarget();
        }
        public override void Die(FloorBase diedObj)
        {
            base.Die(diedObj);

            if (diedObj.transform == transform)
            {
                if (attachedGun.isLaser)
                {
                    attachedGun.GetComponent<LaserGun>().DamageState(false, true);
                }
            }
        }
        public void FloorAdded(Transform floor, int whichFloor, TowerController mainTower, GunSo gun, FloorMine previousFloor)
        {
            if (transform != floor) return;
            this.mainTower = mainTower;
            if (!mainTower.floorMineList.Contains(this))
                mainTower.floorMineList.Add(this);
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
            managerSpot.name = whichFloor + " assign";
            SkinSet(whichFloor);
        }
        private void SkinSet(int index)
        {
            var listIndex = mainTower.data.FloorLevels[index];
            myHealth.SetupHealth(listIndex);
            skin.GetChild(listIndex).gameObject.SetActive(true);
        }
        public void OnEditMode(bool state)
        {
            upgradeButton.SetActive(state);
            myCanvasRaycaster.enabled = state;
            if (mainTower.floors[^1].transform == transform && mainTower.floors.Count < mainTower.gamePresets.maxPossibleFloor - 1)
                addFloorButton.gameObject.SetActive(state);

            if (attachedGun)
                attachedGun.ResetRotation();
        }
        public void UpgradeButton()
        {
            var myIndex = mainTower.floors.IndexOf(gameObject);
            if (mainTower.data.UpdateFloorLevel(myIndex))
            {
                foreach (Transform c in skin)
                {
                    c.gameObject.SetActive(false);
                }
                SkinSet(myIndex);
            }
        }
        public void SwapGun(GameObject newGun = null)
        {
            var index = mainTower.floors.IndexOf(gameObject);
            if (index != GameController.Instance.currentFocusedGun) return;
            mainTower.data.UpdateFloorGun(index, newGun.GetComponent<GunBase>().myGun);
            AttachGun(newGun);
        }

        public void ManagerPanel(bool isOpen)
        {
            managerSpot.SetActive(isOpen);
            upgradeButton.SetActive(!isOpen);
        }
    }
}