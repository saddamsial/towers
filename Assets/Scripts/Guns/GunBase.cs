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
using Unity.Mathematics;
using GameStates;

namespace Guns
{
    public class GunBase : MonoBehaviour
    {
        public bool canShoot, coolDown, isLaser, freezed;
        public GunSo myGun;
        [HideIf("isLaser")]
        public int tempBulletCount;
        private float frequency;
        private float coolDownTime;
        public float freezeCountDown, freezeTime;
        public FloorBase myFloor;
        public Transform skin;
        [ReorderableList]
        public List<Transform> spawnPosition = new();
        public Transform turretPivot;
        public Health health;
        private Quaternion firstRotation;
        private void OnEnable()
        {
            GameController.onGunPlaced += Init;
            GameController.OnDied += Died;
            freezeCountDown = freezeTime;
        }

        private void OnDisable()
        {
            GameController.onGunPlaced -= Init;
            GameController.OnDied -= Died;
        }

        public float GetFrequency()
        {
            return (1 - myGun.frequency) * 2;
        }

        private void Init(GameObject gun, FloorBase floor)
        {
            if (gun != gameObject) return;
            firstRotation = turretPivot.rotation;
            myFloor = floor;
            canShoot = true;
            skin.GetChild((int)myGun.myBullet.bulletType).gameObject.SetActive(true);
        }

        protected virtual void Update()
        {
            if (!canShoot) return;

            if (freezed)
            {
                if (freezeCountDown > 0)
                {
                    freezeCountDown -= Time.deltaTime;
                }
                else
                {
                    freezeCountDown = freezeTime;
                    freezed = false;
                }
                return;
            }
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
                    frequency = GetFrequency();
                    if (!myFloor.attackTo) { canShoot = false; return; }
                    Shoot();
                }
            }
        }
        public void RotateToTarget()
        {
            turretPivot.DOKill();
            if (myFloor.attackTo == null) return;
            if (tempBulletCount > 0)
            {
                coolDownTime = myGun.coolDownTime;
                coolDown = true;
            }
            frequency = GetFrequency();
            canShoot = true;

            // Debug.Log(myFloor.attackTo, myFloor.attackTo);
            turretPivot.DODynamicLookAt(myFloor.attackTo.GetComponent<FloorBase>().gunPosition.position, 0.3f).OnComplete(() =>
            {
                if (isLaser)
                {
                    Shoot();
                }
            });
        }

        public void ResetRotation()
        {
            turretPivot.DORotateQuaternion(firstRotation, 0.4f);//.rotation = firstRotation;
        }
        protected virtual void Shoot()
        {
            if (!GameStateManager.Instance.IsGameState()) { canShoot = false; ResetRotation(); return; }
            for (int i = 0; i < spawnPosition.Count; i++)
            {
                var bullet = myGun.myBullet.prefab.Spawn(spawnPosition[i].position, Quaternion.identity);
                bullet.GetComponent<BulletBase>().Init(myFloor.attackTo.GetComponent<FloorBase>().shootPositions[i],
                    myFloor.attackTo.GetComponent<FloorBase>().attachedGunObj.GetComponent<IDamageable>());

                // if (myGun.ammoCount == 0) break;
                tempBulletCount++;
                if (tempBulletCount == myGun.ammoCount)
                {
                    coolDownTime = myGun.coolDownTime;
                    coolDown = true;
                }
            }
        }

        public void Died(FloorBase diedObj)
        {
            if (diedObj.attachedGunObj != transform) return;
            canShoot = false;

        }
    }
}