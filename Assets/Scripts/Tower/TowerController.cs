using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameStates;
using Lean.Touch;
using NaughtyAttributes;
using UnityEngine;
using Tower.Floor;
using Utils.PoolSystem;
using Utils;
using Cinemachine;
using UnityEngine.Animations;
using Managers;
using Data_and_Scriptable.GunSo;

namespace Tower
{
    public class TowerController : Singleton<TowerController>
    {
        [SerializeField] private GameObject floorPrefab;
        public List<GameObject> floors = new();
        public GameObject tempFloor;
        [ReorderableList]
        public List<Transform> selectedFloors = new();
        public LeanSelectByFinger selections;
        public TowerData data;
        public GamePresets gamePresets;
        FloorMine prevFloorMine;
        public void Start()
        {
            data = (TowerData)DataPersistenceController.Instance.GetData("tower", new TowerData());
            //Debug.Log(data.FloorCount);
            for (int i = 0; i < data.floorCount; i++)
            {
                AddFloor(i, false);
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                AddNewFloor();
            }
        }

        public virtual void AddFloor(int whichFloor, bool isNewFloor = true)
        {
            if (isNewFloor)
                data.FloorCount++;
            //Debug.Log(data.Guns[whichFloor]);
            tempFloor = floorPrefab.Spawn(transform.localPosition + 1.6f * floors.Count * Vector3.up, transform.localRotation, transform);
            if (floors.Count > 0)
            {
                prevFloorMine = floors[^1].GetComponent<FloorMine>();
            }
            floors.Add(tempFloor);
            // if (isNewFloor && floors.Count < gamePresets.myFloorGuns.Count - 1) floors[^1].GetComponent<FloorMine>().addFloorButton.gameObject.SetActive(true);
            GameController.onFloorAdded?.Invoke(tempFloor.transform, whichFloor, this, data.Guns[whichFloor], prevFloorMine);

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

        protected void ResetSelected()
        {
            StartCoroutine(ClearSelected());
        }

        protected IEnumerator ClearSelected()
        {
            yield return new WaitForEndOfFrame(); //WaitForSeconds(0.1f);
            selectedFloors.Clear();
        }
    }
}