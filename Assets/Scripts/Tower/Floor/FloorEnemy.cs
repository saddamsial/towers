using System.Collections;
using Managers;
using UnityEngine;
using Utils;
using static EnemyTowerSo;

namespace Tower.Floor
{
    public class FloorEnemy : FloorBase
    {
        public EnemyTower enemyTower;
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
            this.mainTower = mainTower;
            this.enemyTower = enemyTower;
            skins.GetChild(floor.floorSo.skinNo).gameObject.SetActive(true);
            AttachGun(floor.gunToAttach.myPrefab);
            myHealth = attachedGun.GetComponent<Health>();
            myHealth.myFloor = this;
            myHealth.SetupHealth(floor.Health);
        }
        public void AttackToEnemy(FloorMine floor)
        {
            if (!floor) return;
            attackTo = floor.transform;
            attachedGun.RotateToTarget();
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
                AttackToEnemy(enemyTower.SelectTarget());
            }
        }
    }
}
