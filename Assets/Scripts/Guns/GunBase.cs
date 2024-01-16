using System;
using Bullets;
using Data_and_Scriptable.GunSo;
using UnityEngine;
using Utils.PoolSystem;
using DG.Tweening;
using Managers;
using Tower.Floor;
using Utils;

namespace Guns
{
    public class GunBase : MonoBehaviour
    {
        private bool canShoot;
        public GunSo myGun;
        private float frequency;
        public FloorBase myFloor;
        public Transform skin;
        public Transform spawnPosition;
        public Health health;
        private void OnEnable()
        {
            GameController.onGunPlaced += Init;
            health.OnDied += Die;
        }

        private void OnDisable()
        {
            GameController.onGunPlaced -= Init;
            health.OnDied -= Die;
        }

        private void Init(GameObject gun, FloorBase floor)
        {
            if (gun != gameObject) return;
            myFloor = floor;
            canShoot = true;
            skin.GetChild((int)myGun.myBullet.bulletType).gameObject.SetActive(true);
        }

        private void Update()
        {
            if (!canShoot) return;

            if (frequency > 0)
            {
                frequency -= Time.deltaTime;
            }
            else
            {
                frequency = myGun.frequency;
                Shoot();
            }
        }

        protected virtual void Shoot()
        {
            if (!myFloor.attackTo) return; //{ canShoot = false; return; };
            var bullet = myGun.myBullet.prefab.Spawn(spawnPosition.position, Quaternion.identity);
            bullet.GetComponent<BulletBase>().Init(myFloor.attackTo.GetComponent<FloorBase>().gunPosition);
        }

        public virtual void Die()
        {
            canShoot = false;
            myFloor.Detach();
            gameObject.Despawn();
        }
    }
}