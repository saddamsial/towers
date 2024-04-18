using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data_and_Scriptable.GunSo;
using GameStates;
using Guns;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Tower.Floor
{
    public class FloorMine : FloorBase
    {
        public bool isTargeted;
        public bool isManagerAssigned;
        public int myManagerId;
        public Button addFloorButton;
        public GameObject upgradeButton;
        public GameObject managerSpot;
        public GameObject assignedManagerSpot;
        public GraphicRaycaster myCanvasRaycaster;
        public PrePlacedManagerImage prePlacedManagerImage;
        public override void OnEnable()
        {
            GameController.onDied += Die;
            InputController.Instance.onTargetSet += Attack;
            GameController.onFloorAdded += FloorAdded;
            GameController.onEditMode += OnEditMode;
            GameController.onSwapGun += SwapGun;
            GameController.onManagerEditMode += ManagerPanel;
            GameController.onManagerCheck += ManagerCheck;

            addFloorButton.onClick.RemoveAllListeners();
        }
        public override void OnDisable()
        {
            GameController.onDied -= Die;
            GameController.onFloorAdded -= FloorAdded;
            GameController.onEditMode -= OnEditMode;
            GameController.onSwapGun -= SwapGun;
            GameController.onManagerEditMode -= ManagerPanel;
            GameController.onManagerCheck -= ManagerCheck;

            if (InputController.Instance)
                InputController.Instance.onTargetSet -= Attack;
        }
        public void ManagerCheck(int clickedManager)
        {
            if (isManagerAssigned) { managerSpot.SetActive(false); return; }
            managerSpot.SetActive(attachedGun.gunIdForManager == clickedManager);
            if (clickedManager == -1)
                managerSpot.SetActive(true);
        }
        public void SetupManager(int id)
        {
            attachedGun.speedMultiplier = 1.2f;
            myHealth.damageMultiplier = 1.2f;
            myManagerId = id;
            isManagerAssigned = true;
        }
        public void RemoveManager()
        {
            isManagerAssigned = false;
            attachedGun.speedMultiplier = 1;
            myHealth.damageMultiplier = 1;
            myManagerId = -1;
        }
        public override void Attack(Transform target)
        {
            if (!mainTower.selectedFloors.Contains(transform) || !attachedGun) return;
            attackTo = target;
            attachedGun.RotateToTarget();
        }
        public void ManagerAttack()
        {
            if (!GameStateManager.Instance.IsGameState()) return;
            if (!isManagerAssigned) return;
            var t = GetEnemyFloorToAttack();
            if (t == null) { attachedGun.canShoot = false; return; }
            attackTo = t;
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
        public void FloorAdded(Transform floor, int whichFloor, TowerController mainTower, GunSo gun, FloorMine previousFloor, EnemyTower enemyTower)
        {
            if (transform != floor) return;
            this.mainTower = mainTower;
            this.enemyTower = enemyTower;
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
            UpgradeButtonMaxLevelCheck();
            myCanvasRaycaster.enabled = state;
            if (mainTower.floors[^1].transform == transform && mainTower.floors.Count < mainTower.gamePresets.maxPossibleFloor - 1)
                addFloorButton.gameObject.SetActive(state);

            if (attachedGun)
                attachedGun.ResetRotation();
        }
        public void UpgradeButtonMaxLevelCheck()
        {
            var myIndex = mainTower.floors.IndexOf(gameObject);
            if (!mainTower.data.UpdateFloorLevel(myIndex))
                upgradeButton.SetActive(false);
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
            else upgradeButton.SetActive(false);
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
            if (isManagerAssigned)
            {
                managerSpot.SetActive(false);
            }
            upgradeButton.SetActive(!isOpen);
            UpgradeButtonMaxLevelCheck();
            var myIndex = mainTower.floors.IndexOf(gameObject);
            myManagerId = mainTower.data.FloorManagers[myIndex];
        }
        public void ManagerPanelModeGame()
        {
            if (!isManagerAssigned) return;
            assignedManagerSpot.transform.GetChild(0).gameObject.SetActive(false);
            assignedManagerSpot.transform.GetChild(1).gameObject.SetActive(true);
            StartCoroutine(ManagerAutoAttack());
        }
        public void ManagerPanelModeEdit()
        {
            if (!isManagerAssigned) return;
            assignedManagerSpot.transform.GetChild(0).gameObject.SetActive(true);
            assignedManagerSpot.transform.GetChild(1).gameObject.SetActive(false);
        }
        public IEnumerator ManagerAutoAttack()
        {
            yield return new WaitForSeconds(Random.Range(0.9f, 1.5f));
            if (!attachedGun.canShoot || attachedGun.isLaser)
                ManagerAttack();
        }
        public Transform GetEnemyFloorToAttack()
        {
            if (enemyTower.floors.Count <= 0) return null;
            return enemyTower.floors[Random.Range(0, enemyTower.floors.Count)].transform;
        }
    }
}