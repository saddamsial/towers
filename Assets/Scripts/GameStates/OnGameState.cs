using UnityEngine;
using UnityEngine.Events;

namespace GameStates
{
    public class OnGameState : GameState
    {
        public UnityEvent resetsForGameStart, exitStateEvents;

        public override void OnEnterState()
        {
            base.OnEnterState();
            resetsForGameStart?.Invoke();
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