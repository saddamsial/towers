using UnityEngine;
using UnityEngine.Events;

namespace GameStates
{
    public class OnFailState : GameState
    {
        public UnityEvent stateChangeEvents;
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
        }
    }
}