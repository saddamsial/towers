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


        private TowerData _data;

        public void Start()
        {
            _data = (TowerData)DataController.Instance.GetData("my tower", new TowerData("my tower"));

        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                AddFloor();
            }
        }

        public virtual void AddFloor()
        {
            _data._floorCount++;
            tempFloor = floorPrefab.Spawn(transform.localPosition + 1.6f * floors.Count * Vector3.up, transform.localRotation, transform);
            floors.Add(tempFloor);
            var floorBase = tempFloor.GetComponent<FloorBase>();
            floorBase.mainTower = this;
        }

        protected void AddToList()
        {
            if (GameStateManager.Instance.GetCurrentState() != typeof(OnGameState)) return;

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