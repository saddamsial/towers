using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameStates;
using Lean.Touch;
using UnityEngine;
using Utils;

namespace Tower
{
    public class TowerController : MonoBehaviour
    {
        public List<Transform> selectedFloors = new();
        public LeanSelectByFinger selections;

        void Start()
        {
        }

        void Update()
        {
        }

        public void AddToList()
        {
            if (GameStateManager.Instance.GetCurrentState() != typeof(OnGameState)) return;

            foreach (var s in selections.Selectables.Where(s =>
                         !selectedFloors.Contains(s.transform) && !s.GetComponentInParent<EnemyTower>()))
            {
                selectedFloors.Add(s.transform);
            }
        }

        public void ResetSelected()
        {
            StartCoroutine(ClearSelected());
        }

        IEnumerator ClearSelected()
        {
            yield return new WaitForEndOfFrame(); //WaitForSeconds(0.1f);
            selectedFloors.Clear();
        }
    }
}