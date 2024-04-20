using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace GameStates
{
    public class OnEditState : GameState
    {
        public UnityEvent stateChangeEvents, exitStateEvents;

        public override void OnEnterState()
        {
            base.OnEnterState();
            stateChangeEvents?.Invoke();
        }

        public override void OnUpdateState()
        {
        }

        public override void OnExitState()
        {
            base.OnExitState();
            exitStateEvents?.Invoke();
        }
    }
}