using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace GameStates
{
    public class OnEditState : GameState
    {
        [SerializeField] private GameObject editCam, mainCam;
        public UnityEvent stateChangeEvents;

        public override void OnEnterState()
        {
            base.OnEnterState();
            mainCam.SetActive(false);
            editCam.SetActive(true);
            stateChangeEvents?.Invoke();
        }

        public override void OnUpdateState()
        {
        }

        public override void OnExitState()
        {
            base.OnExitState();
            mainCam.SetActive(true);
            editCam.SetActive(false);
        }
    }
}