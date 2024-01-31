using System.Collections;
using System.Collections.Generic;
using Data_and_Scriptable.GunSo;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace Tower.Floor
{
    public class FloorMine : FloorBase
    {
        public override void OnEnable()
        {
            GameController.OnDied += Die;
            InputController.Instance.onTargetSet += Attack;
            // AttachGun();
        }

        public override void OnDisable()
        {
            GameController.OnDied -= Die;
            if (InputController.Instance)
                InputController.Instance.onTargetSet -= Attack;
        }

        public override void Init(TowerController mainTower, GunSo gun)
        {
            base.Init(mainTower, gun);
            AttachGun(gun.myPrefab);
        }

        public override void Attack(Transform target)
        {
            if (!mainTower.selectedFloors.Contains(transform)) return;
            attackTo = target;
            attachedGun.canShoot = true;
        }

        public override void Die(FloorBase diedObj)
        {
            base.Die(diedObj);
            if (diedObj.transform == attackTo)
            {
                attackTo = null;
                attachedGun.canShoot = false;
            }
        }
    }
}