using System;
using Data_and_Scriptable.BulletSo;
using Unity.Mathematics;

using UnityEngine;
using Utils;
using Utils.PoolSystem;

namespace Bullets
{
    [Serializable]
    public class Projectile
    {
        public GameObject spawnEffect, movingObj, explodeEffect;
    }
    public class BulletBase : MonoBehaviour
    {
        [SerializeField] public BulletSo bullet;
        [SerializeField] private Transform skin;
        [SerializeField] private Projectile mySkin;
        public GameObject movingObj, tempSpawnEffect, tempExplodeEffect;
        public virtual void Init(Transform target, IDamageable damageable, Transform targetParent)
        {
            tempSpawnEffect = mySkin.spawnEffect.Spawn(transform.position, quaternion.identity);
            movingObj = mySkin.movingObj.Spawn(transform.position, quaternion.identity, transform);
            movingObj.transform.LookAt(target);
        }
        public void OnReached(Transform other, IDamageable damageable)
        {
            tempExplodeEffect = mySkin.explodeEffect.Spawn(other.position, quaternion.identity);
            damageable.Damage(bullet.damage);
            movingObj.transform.parent = null;
            OnHit(other);
            if (movingObj.activeInHierarchy)
                movingObj.Despawn();
        }
        private void OnHit(Transform other)
        {
            tempExplodeEffect.Despawn(.2f);
            tempSpawnEffect.Despawn();
            movingObj.transform.parent = null;
            gameObject.Despawn(.2f);
        }
    }
}

