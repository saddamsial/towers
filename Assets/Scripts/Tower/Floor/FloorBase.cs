using System.Collections;
using System.Collections.Generic;
using Data_and_Scriptable.GunSo;
using DG.Tweening;
using EPOOutline;
using GameStates;
using Guns;
using Managers;
using UnityEngine;
using Utils;
using Utils.PoolSystem;

//machine gun, canon, flame gun, rocket, laser, ice cannon, bomb launcher, tesla, shockwave, plasma
//wood, stone, metal, semi gold, gold, diamond, platinum 
namespace Tower.Floor
{
    public abstract class FloorBase : MonoBehaviour
    {
        [SerializeField] private float height;
        [SerializeField] public Transform gunPosition;
        public List<Transform> shootPositions = new();
        public Transform attackTo;
        public Outlinable outline;
        public TowerController mainTower;
        public GameObject attachedGunObj;
        public GunBase attachedGun;
        public GameObject tempGun;
        public Health myHealth;
        public bool IsFreezed { get; set; }
        public abstract void OnEnable();
        public abstract void OnDisable();

        public abstract void Attack(Transform target);

        public void AttachGun(GameObject tempGun = null)
        {
            if (attachedGunObj != null)
            {
                Detach();
            }
            var gunObj = (tempGun ? tempGun : this.tempGun).Spawn(gunPosition.position, transform.rotation);
            attachedGunObj = gunObj;
            attachedGun = attachedGunObj.GetComponent<GunBase>();
            gunObj.transform.parent = gunPosition;
            GameController.onGunPlaced?.Invoke(gunObj, this);
            if (!attachedGun) return;
            myHealth = attachedGunObj.GetComponent<Health>();
            myHealth.myFloor = this;
        }
        public void Detach()
        {
            //Debug.Log("removed");
            attachedGunObj.Despawn();
            attackTo = null;
            attachedGunObj = null;
        }
        public void SetOutlinableState(bool state)
        {
            if (GameStateManager.Instance && !GameStateManager.Instance.IsGameState() || !attachedGun) return;
            outline.enabled = state;
        }
        public virtual void Die(FloorBase diedObj)
        {
            if (diedObj.transform == attackTo)
            {
                if (attachedGun.isLaser)
                {
                    attachedGun.GetComponent<LaserGun>().DamageState(false, true);
                }
                attackTo = null;
                attachedGun.canShoot = false;
                attachedGun.ResetRotation();
            }
            if (diedObj.transform != transform) return;
            gameObject.Despawn();
        }

        public void MoveToNewPositionAfterDestroy(Vector3 newPos, float delay)
        {
            transform.DOMove(newPos, 0.5f + delay).SetEase(Ease.InBack);
        }
    }
}