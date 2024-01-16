using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameStates
{
    public abstract class GameState : MonoBehaviour
    {
        [SerializeField] private GameObject myPanel;


        public virtual void OnEnterState()
        {
            myPanel.SetActive(true);
        }

        public abstract void OnUpdateState();

        public virtual void OnExitState()
        {
            myPanel.SetActive(false);
        }
    }
}