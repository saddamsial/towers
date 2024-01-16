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
        [SerializeField] private BulletSo bullet;
        [SerializeField] private Transform skin;
        [SerializeField] private TrailRenderer trailRenderer;

        public virtual void Init(Transform target)
        {
            trailRenderer.Clear();
            skin.GetChild((int)bullet.bulletType).gameObject.SetActive(true);
            transform.DOJump(target.position + Vector3.up * 0.5f, 1f, 1, bullet.speed).SetEase(Ease.Linear)
                .OnComplete(() => OnReached(target));
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