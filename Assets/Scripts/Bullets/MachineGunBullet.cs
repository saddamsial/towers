using System;
using DG.Tweening;
using UnityEngine;
using Utils;

namespace Bullets
{
    public class MachineGunBullet : BulletBase
    {
        public override void Init(Transform target)
        {
            base.Init(target);
            // transform.DOJump(target.position + Vector3.up * 0.5f, 0.1f, 1, bullet.speed).SetEase(Ease.Linear)
            //     .OnComplete(() => OnReached(target));
            transform.DOMove(target.position + Vector3.up * 0.5f, bullet.speed).SetSpeedBased().SetEase(Ease.Linear)
            .OnComplete(() => OnReached(target));
        }
    }
}