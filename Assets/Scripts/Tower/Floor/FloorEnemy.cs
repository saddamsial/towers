using System.Collections;
using System.Collections.Generic;
using Data_and_Scriptable.GunSo;
using DG.Tweening;
using Managers;
using UnityEngine;

namespace Tower.Floor
{
    public class FloorEnemy : FloorBase
    {
        public override void OnEnable()
        {
            GameController.OnDied += Die;
            InputController.Instance.onTargetSet += Attack;
        }
        public override void OnDisable()
        {
            GameController.OnDied -= Die;
            if (InputController.Instance)
                InputController.Instance.onTargetSet -= Attack;
        }

        public override void Attack(Transform target)
        {
            if (target != transform) return;
            StartCoroutine(DisableOutline(target));
        }

        public IEnumerator DisableOutline(Transform target)
        {
            outline.OutlineParameters.Enabled = true;
            yield return new WaitForSeconds(0.5f);
            outline.OutlineParameters.Enabled = false;
        }

        public override void Die(FloorBase diedObj)
        {
            base.Die(diedObj);
        }

        public void MoveToNewPositionAfterDestroy(Vector3 newPos, float delay)
        {
            transform.DOMove(newPos, 0.5f + delay).SetEase(Ease.InBack);
        }
    }
}
