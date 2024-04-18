using System.Collections;
using Managers;
using UnityEngine;
using Utils;
using static EnemyTowerSo;

namespace Tower.Floor
{
    public class FloorEnemy : FloorBase
    {
        FloorTemp tempFloor;
        public Transform skins;
        public override void OnEnable()
        {
            GameController.onDied += Die;
            InputController.Instance.onTargetSet += Attack;
        }
        public override void OnDisable()
        {
            GameController.onDied -= Die;
            if (InputController.Instance)
                InputController.Instance.onTargetSet -= Attack;
        }
        public void Init(FloorTemp floor, EnemyTower enemyTower, TowerController mainTower)
        {
            tempFloor = floor;
            this.mainTower = mainTower;
            this.enemyTower = enemyTower;
            skins.GetChild(floor.floorSo.skinNo).gameObject.SetActive(true);
            AttachGun(floor.gunToAttach.myPrefab);
            myHealth = attachedGun.GetComponent<Health>();
            myHealth.myFloor = this;
            var lvl = floor.floorSo.name.Split(' ');
            myHealth.SetupHealth(floor.Health + ((int.Parse(lvl[0]) - 4) * 0.5f));

        }
        public void AttackToEnemy(FloorMine floor, int difficulty)
        {
            if (attackTo != null)
            {
                attackTo.GetComponent<FloorMine>().isTargeted = false;
                attackTo = null;
            }
            if (!floor) return;
            floor.isTargeted = true;
            attackTo = floor.transform;
            attachedGun.RotateToTarget(difficulty);
        }
        public override void Attack(Transform target)
        {
            //TODOtower buraya eğer kullanıcının ilk saldırmasını beklemek istiyorsan kontrol koy
            if (target != transform) return;
            StartCoroutine(DisableOutline(target));
        }
        public IEnumerator DisableOutline(Transform target)
        {
            outline.OutlineParameters.Enabled = true;
            yield return new WaitForSeconds(0.5f);
            outline.OutlineParameters.Enabled = false;
        }
        public override void Die(FloorBase diedObj)
        {
            base.Die(diedObj);
            if (diedObj.transform != attackTo)
            {
                AttackToEnemy(enemyTower.targetSelector.SelectTarget(tempFloor.difficulty, transform), tempFloor.difficulty);
            }
        }
    }
}
