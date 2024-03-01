using System.Collections.Generic;
using System.Linq;
using GameStates;
using Managers;
using Tower.Floor;
using UnityEngine;
using Utils;
using Utils.PoolSystem;

namespace Tower
{
    public class EnemyTower : TowerBase
    {
        public TowerController mainTower;
        public EnemyTowerSo enemyTowerSo;

        public List<FloorMine> floorsMain = new();
        public List<FloorMine> floorsHealth2Low;
        public List<FloorMine> floorsPower2Least = new();
        public List<FloorMine> floorsNotTargeted = new();
        public List<FloorMine> floorsFreezed = new();
        public void OnEnable()
        {
            GameController.OnDied += RearrangeFloors;
        }

        protected void OnDisable()
        {
            GameController.OnDied -= RearrangeFloors;
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
        public override void AddFloor(int c)
        {
            base.AddFloor(c);
            floors.Add(tempFloor);
            var floorEnemy = tempFloor.GetComponent<FloorEnemy>();
            floorEnemy.Init(enemyTowerSo.floorTemps[c], this, mainTower);
        }
        public void GameStarted()
        {
            floorsMain = new List<FloorMine>(mainTower.floorMineList);
            SelectTarget();
        }

        public FloorMine SelectTarget()
        {
            var tempTarget = floorsMain[0];

            floorsHealth2Low = new List<FloorMine>(floorsMain.Where(x => x.attachedGun != null).OrderBy(x => x.myHealth.Current));
            floorsPower2Least = new List<FloorMine>(floorsMain.Where(x => x.attachedGun != null).OrderBy(x => x.attachedGun.myGun.myBullet.damage));
            floorsNotTargeted = new List<FloorMine>(floorsMain.Where(x => x.IsTargeted)).ToList();
            floorsFreezed = new List<FloorMine>(floorsMain.Where(x => x.IsFreezed)).ToList();

            tempTarget.IsTargeted = true;
            return tempTarget;
        }
    }
}
