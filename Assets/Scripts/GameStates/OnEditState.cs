using Managers;
using UnityEngine;

namespace GameStates
{
    public class OnEditState : GameState
    {
        [SerializeField] private GameObject editCam;

        public override void OnEnterState()
        {
            base.OnEnterState();
            editCam.SetActive(true);
        }

        public override void OnUpdateState()
        {
        }

        public override void OnExitState()
        {
            base.OnExitState();
            editCam.SetActive(false);
        }
    }
}