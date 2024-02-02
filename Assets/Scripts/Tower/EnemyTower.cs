using System.Collections.Generic;
using GameStates;
using Managers;
using Tower.Floor;
using Unity.VisualScripting;
using UnityEngine;
using Utils.PoolSystem;

namespace Tower
{
    public class EnemyTower : MonoBehaviour
    {
        [SerializeField] private GameObject floorPrefab;
        public EnemyTowerSo enemyTowerSo;
        [SerializeField] private List<GameObject> floors = new();
        public GameObject tempFloor;

        public void OnEnable()
        {
            GameController.OnDied += RearrangeFloors;

        }

        public void Start()
        {
            GenerateTowerWithSo();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                GenerateTowerWithSo();
            }
        }

        public void GenerateTowerWithSo()
        {
            for (int i = 0; i < enemyTowerSo.floorTemps.Count; i++)
            {
                AddFloor(i);
            }
        }
        public void AddFloor(int c)
        {
            tempFloor = floorPrefab.Spawn(transform.localPosition + 1.6f * floors.Count * Vector3.up, transform.localRotation, transform);
            floors.Add(tempFloor);
            tempFloor.transform.GetChild(0).GetChild(enemyTowerSo.floorTemps[c].floorSo.skinNo).gameObject.SetActive(true);// TODO burayı daha düzgün yap
            var floorBase = tempFloor.GetComponent<FloorBase>();
            floorBase.mainTower = TowerController.Instance;
        }

        public void RearrangeFloors(FloorBase floorObj)
        {
            floors.Remove(floorObj.gameObject);
            if (floors.Count <= 0)
            {
                GameStateManager.Instance.SetState(GameStateManager.Instance.onCompleteState);
                return;
            }
            for (int i = 0; i < floors.Count; i++)
            {
                floors[i].GetComponent<FloorEnemy>().MoveToNewPositionAfterDestroy(transform.localPosition + 1.6f * i * Vector3.up, delay: 0.1f * i);
            }
        }
    }
}
