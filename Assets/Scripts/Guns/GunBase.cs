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
using GameStates;
using System.Collections;

namespace Guns
{
    public class GunBase : MonoBehaviour
    {
        public bool canShoot, coolDown, isLaser;
        public GunSo myGun;
        GameObject bullet;
        [HideIf("isLaser")]
        public int tempBulletCount;
        private float frequency;
        private float coolDownTime;
        public float speedMultiplier;
        public FloorBase myFloor;
        public Transform skin;
        [ReorderableList]
        public List<Transform> spawnPosition = new();
        public Transform turretPivot;
        public Health health;
        private Quaternion firstRotation;
        private Coroutine _freeze;

        private void OnEnable()
        {
            GameController.onGunPlaced += Init;
            GameController.onDied += Died;
            GameController.onFreeze += Freezed;
        }
        private void OnDisable()
        {
            GameController.onGunPlaced -= Init;
            GameController.onDied -= Died;
            GameController.onFreeze -= Freezed;
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
            if (isLaser) return;

            if (coolDown)
            {
                if (coolDownTime > 0)
                {
                    coolDownTime -= Time.deltaTime * speedMultiplier;
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
                    frequency -= Time.deltaTime * speedMultiplier;
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
                bullet = myGun.myBullet.prefab.Spawn(spawnPosition[i].position, Quaternion.identity);
                var tempFloorBase = myFloor.attackTo.GetComponent<FloorBase>();
                bullet.GetComponent<BulletBase>().Init(tempFloorBase.shootPositions[i],
                    tempFloorBase.attachedGunObj.GetComponent<IDamageable>(), myFloor.attackTo);

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
        public void Freezed(Transform freezedFloor, bool isPowerup = false)
        {
            if (myFloor.transform != freezedFloor.transform) return;
            if (isPowerup)
            {
                if (_freeze != null)
                {
                    StopCoroutine(_freeze);
                }
                _freeze = StartCoroutine(Freeze(true));
                canShoot = false;
            }
            else
            {
                _freeze ??= StartCoroutine(Freeze(false));
            }
        }
        public IEnumerator Freeze(bool isPower)
        {
            speedMultiplier = isPower ? 0.0f : .75f;
            yield return new WaitForSecondsRealtime(2);
            speedMultiplier = 1;
            canShoot = true;
            _freeze = null;
        }
    }
}