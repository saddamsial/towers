using Data_and_Scriptable.BulletSo;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Utils;
using Utils.PoolSystem;

namespace Bullets
{
    public class BulletBase : MonoBehaviour
    {
        [SerializeField] public BulletSo bullet;
        [SerializeField] private Transform skin;
        [SerializeField] private TrailRenderer trailRenderer;

        public virtual void Init(Transform target)
        {
            trailRenderer.Clear();
            for (int i = 0; i < skin.childCount; i++)
            {
                skin.GetChild(i).gameObject.SetActive(i == (int)bullet.bulletType);
            }


        }

        public void OnReached(Transform other)
        {
            if (other.GetChild(0).TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(bullet.damage);
            }

            OnHit();
        }

        private void OnHit()
        {
            gameObject.Despawn();
            bullet.effect.Spawn(transform.position, Quaternion.identity).Despawn(bullet.despawnTime);
        }

    }
}