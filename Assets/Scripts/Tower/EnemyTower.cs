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
        public TargetSelectorEnemy targetSelector;

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
            GenerateTowerWithSo();
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
            targetSelector.FirstFill();
            for (int i = 0; i < floors.Count; i++)
            {
                floors[i].GetComponent<FloorEnemy>().AttackToEnemy(targetSelector.SelectTarget(enemyTowerSo.floorTemps[i].difficulty));
            }
        }
    }
}
