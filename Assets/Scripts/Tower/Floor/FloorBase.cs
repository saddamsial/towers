using System.Collections;
using EPOOutline;
using GameStates;
using Guns;
using Managers;
using NaughtyAttributes;
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
        [SerializeField] public Transform attackTo;
        public Outlinable outline;
        public TowerController mainTower;
        public GameObject attachedGun;
        public bool enableDemoItems;

        [ShowIf("enableDemoItems")]
        [BoxGroup("Demo Items")]
        public GameObject demoGun;

        public abstract void OnEnable();

        public abstract void OnDisable();

        public abstract void Attack(Transform target);


        [Button]
        public void AttachGun(GunBase gun = null)
        {
            if (!gun) //demo purpose
            {
                var gunObj = demoGun.Spawn(gunPosition.position, transform.rotation);
                GameController.onGunPlaced?.Invoke(gunObj, this);
                attachedGun = gunObj;
                gunObj.transform.parent = gunPosition;
            }
        }

        public void Detach()
        {
            Debug.Log("removed");
            attackTo = null;
            attachedGun.Despawn();
        }


        public void SetOutlinableState(bool state)
        {
            if (GameStateManager.Instance.GetCurrentState() != typeof(OnGameState)) return;

            outline.enabled = state;
        }
    }
}