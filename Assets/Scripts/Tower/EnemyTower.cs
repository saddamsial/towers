using GameStates;
using Managers;
using Tower.Floor;
using UnityEngine;

namespace Tower
{
    public class EnemyTower : TowerBase
    {
        public TowerController mainTower;
        public EnemyTowerSo enemyTowerSo;
        public TargetSelectorEnemy targetSelector;
        public EnemyHealthFillManager enemyHp;

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
            // yield return new WaitForSeconds(0.1f);
            enemyTowerSo = Resources.Load<EnemyTowerSo>("EnemyTowers/enemy tower " + (GameStateManager.Instance.gameData.EnemyLevel + 1));
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
            var initialHp = 0f;
            targetSelector.FirstFill();
            for (int i = 0; i < floors.Count; i++)
            {
                floors[i].GetComponent<FloorEnemy>().AttackToEnemy(targetSelector.SelectTarget(enemyTowerSo.floorTemps[i].difficulty, floors[i].transform),
                enemyTowerSo.floorTemps[i].difficulty);
                initialHp += floors[i].GetComponent<FloorEnemy>().myHealth.Current;
            }

            enemyHp.Init(initialHp);
        }

        public override void RearrangeFloors(FloorBase floorObj)
        {
            base.RearrangeFloors(floorObj);
            if (floors.Count <= 0)
            {
                GameStateManager.Instance.SetState(GameStateManager.Instance.onCompleteState);
                return;
            }
        }
    }
}
