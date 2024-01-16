using System.Collections;
using System.Collections.Generic;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace Tower.Floor
{
    public class FloorMine : FloorBase
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
            if (!mainTower.selectedFloors.Contains(transform)) return;
            attackTo = target;
        }
    }
}