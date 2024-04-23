using UnityEngine;
using UnityEngine.Events;

namespace GameStates
{
    public class OnCompleteState : GameState
    {
        public UnityEvent stateChangeEvents;
        public override void OnEnterState()
        {
            base.OnEnterState();
            stateChangeEvents?.Invoke();
            GameStateManager.Instance.gameData.EnemyLevel++;
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