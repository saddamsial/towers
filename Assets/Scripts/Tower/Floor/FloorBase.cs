using System;
using System.Net.Http.Headers;
using Data_and_Scriptable.GunSo;
using EPOOutline;
using GameStates;
using Guns;
using Managers;
using NaughtyAttributes;
using UnityEngine;
using Utils.PoolSystem;

//machine gun, canon, flame gun, rocket, laser, ice cannon, bomb launcher, tesla, shockwave, plasma
//wood, stone, metal, semi gold, gold, diamond, platinum 
namespace Tower.Floor
{
    public abstract class FloorBase : MonoBehaviour
    {
        [SerializeField] private float height;
        [SerializeField] public Transform gunPosition;
        public Transform attackTo;

        public Outlinable outline;
        public TowerController mainTower;
        public GameObject attachedGunObj;
        public GunBase attachedGun;
        public GameObject tempGun;

        public abstract void OnEnable();
        public abstract void OnDisable();

        public abstract void Attack(Transform target);

        public virtual void Init(TowerController mainTower, GunSo gun)
        {
            this.mainTower = mainTower;
        }
        public void AttachGun(GameObject tempGun = null)
        {
            if (attachedGunObj != null)
            {
                attachedGunObj.Despawn();
                attachedGunObj = null;
                attachedGun = null;
            }
            var gunObj = (tempGun ? tempGun : this.tempGun).Spawn(gunPosition.position, transform.rotation);
            attachedGunObj = gunObj;
            attachedGun = attachedGunObj.GetComponent<GunBase>();
            gunObj.transform.parent = gunPosition;
            GameController.onGunPlaced?.Invoke(gunObj, this);
        }
        public void Detach()
        {
            Debug.Log("removed");
            attackTo = null;
            attachedGunObj = null;
        }
        public void SetOutlinableState(bool state)
        {
            if (GameStateManager.Instance && GameStateManager.Instance.GetCurrentState() != typeof(OnGameState)) return;

            outline.enabled = state;
        }
        public virtual void Die(FloorBase diedObj)
        {
            if (diedObj.transform != transform) return;
            gameObject.Despawn();
        }
    }
}