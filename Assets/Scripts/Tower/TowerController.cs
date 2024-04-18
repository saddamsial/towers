using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameStates;
using Lean.Touch;
using NaughtyAttributes;
using UnityEngine;
using Tower.Floor;
using Managers;
using Data_and_Scriptable.GunSo;

namespace Tower
{
    public class TowerController : TowerBase
    {
        public List<FloorMine> floorMineList = new();
        [ReorderableList]
        public List<Transform> selectedFloors = new();
        public LeanSelectByFinger selections;
        public TowerData data;
        public GamePresets gamePresets;
        FloorMine prevFloorMine;
        public CameraSettingsController cameraSettings;
        public TargetSelectorEnemy targetSelectorEnemy;
        public EnemyTower enemyTower;
        public void OnEnable()
        {
            GameController.onDied += RearrangeFloors;
        }
        protected void OnDisable()
        {
            GameController.onDied -= RearrangeFloors;
        }
        public void Start()
        {
            gamePresets = Instantiate(gamePresets);
            data = (TowerData)DataPersistenceController.Instance.GetData("tower", new TowerData());
            //Debug.Log(data.FloorCount);
            for (int i = 0; i < data.floorCount; i++)
            {
                AddFloor(i, false);
            }
            cameraSettings.ZoomLevel(data.FloorCount);
        }
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                AddNewFloor();
            }
        }
        public override void AddFloor(int whichFloor, bool isNewFloor)
        {
            if (isNewFloor)
                data.FloorCount++;

            base.AddFloor(whichFloor);

            tempFloor.GetComponent<FloorMine>().mainTower = this;
            if (floors.Count > 0)
            {
                prevFloorMine = floors[^1].GetComponent<FloorMine>();
            }
            floors.Add(tempFloor);
            // if (isNewFloor && floors.Count < gamePresets.myFloorGuns.Count - 1) floors[^1].GetComponent<FloorMine>().addFloorButton.gameObject.SetActive(true);
            GameController.onFloorAdded?.Invoke(tempFloor.transform, whichFloor, this, data.Guns[whichFloor], prevFloorMine, enemyTower);
            cameraSettings.ZoomLevel(data.FloorCount);

        }
        public void EditModeOpen(bool state)
        {
            floors[^1].GetComponent<FloorMine>().addFloorButton.gameObject.SetActive(state);
        }
        public void AddNewFloor()
        {
            AddFloor(floors.Count, true);
        }
        protected void AddToList()
        {
            if (!GameStateManager.Instance.IsGameState()) return;

            foreach (var s in selections.Selectables.Where(s =>
                         !selectedFloors.Contains(s.transform) && !s.GetComponentInParent<EnemyTower>()))
            {
                selectedFloors.Add(s.transform);
            }
        }
        public override void RearrangeFloors(FloorBase floorObj)
        {
            base.RearrangeFloors(floorObj);
            floorMineList.Remove(floorObj.gameObject.GetComponent<FloorMine>());
            targetSelectorEnemy.FirstFill();
        }
        public void ResetSelected()
        {
            StartCoroutine(ClearSelected());
        }
        protected IEnumerator ClearSelected()
        {
            yield return new WaitForEndOfFrame(); //WaitForSeconds(0.1f);
            selectedFloors.Clear();
        }
        public GunSo FocusedGunSo()
        {
            if (GameController.Instance.currentFocusedGun < 0 ||
            !floors[GameController.Instance.currentFocusedGun].GetComponent<FloorBase>().attachedGun) return null;
            var so = floors[GameController.Instance.currentFocusedGun].GetComponent<FloorBase>().attachedGun.myGun;
            return so;
        }
        public void DeselectAfterGameDone()
        {
            foreach (var floor in floors)
            {
                floor.GetComponent<FloorBase>().outline.enabled = false;
            }
        }
        public override void ManagerPanelModeGame()
        {
            for (int i = 0; i < floorMineList.Count; i++)
            {
                floorMineList[i].ManagerPanelModeGame();
            }
        }
        public override void ManagerPanelModeEdit()
        {
            for (int i = 0; i < floorMineList.Count; i++)
            {
                floorMineList[i].ManagerPanelModeEdit();
            }
        }
    }
}