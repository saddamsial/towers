using System;
using Bullets;
using Data_and_Scriptable.GunSo;
using UnityEngine;
using Utils.PoolSystem;
using DG.Tweening;
using Managers;
using Tower.Floor;
using Utils;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;

namespace Guns
{
    public class GunBase : MonoBehaviour
    {
        public bool canShoot, coolDown, isLaser;
        public GunSo myGun;
        [HideIf("isLaser")]
        public int tempBulletCount;
        private float frequency;
        private float coolDownTime;
        public FloorBase myFloor;
        public Transform skin;
        [ReorderableList]
        public List<Transform> spawnPosition = new();
        public Transform turretPivot;
        public Health health;
        private void OnEnable()
        {
            GameController.onGunPlaced += Init;
            GameController.OnDied += Died;
        }

        private void OnDisable()
        {
            GameController.onGunPlaced -= Init;
            GameController.OnDied -= Died;
        }

        private void Init(GameObject gun, FloorBase floor)
        {
            if (gun != gameObject) return;
            myFloor = floor;
            canShoot = true;
            skin.GetChild((int)myGun.myBullet.bulletType).gameObject.SetActive(true);
        }

        protected virtual void Update()
        {
            if (!canShoot) return;

            if (coolDown)
            {
                if (coolDownTime > 0)
                {
                    coolDownTime -= Time.deltaTime;
                }
                else
                {
                    tempBulletCount = 0;
                    coolDown = false;
                }
            }
            else
            {
                if (frequency > 0)
                {
                    frequency -= Time.deltaTime;
                }
                else
                {
                    frequency = myGun.frequency;
                    if (!myFloor.attackTo) return;
                    Shoot();
                }
            }
        }
        public void RotateToTarget()
        {
            turretPivot.DODynamicLookAt(myFloor.attackTo.GetComponent<FloorBase>().gunPosition.position, 0.3f).OnComplete(() =>
            {
                if (isLaser)
                {
                    Shoot();
                }
            });
        }
        protected virtual void Shoot()
        {
            var bullet = myGun.myBullet.prefab.Spawn(spawnPosition[0].position, Quaternion.identity);
            bullet.GetComponent<BulletBase>().Init(myFloor.attackTo.GetComponent<FloorBase>().gunPosition);

            tempBulletCount++;
            if (tempBulletCount == myGun.ammoCount)
            {
                coolDownTime = myGun.coolDownTime;
                coolDown = true;
            }
        }

        public void Died(FloorBase diedObj)
        {
            if (diedObj.attachedGunObj != transform) return;
            canShoot = false;
        }
    }
}