using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Tower.Floor
{
    public class FloorEnemy : FloorBase
    {
        public override void OnEnable()
        {
            InputController.Instance.onTargetSet += Attack;

        }
        public override void OnDisable()
        {
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
    }
}
