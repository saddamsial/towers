using UnityEngine;
using Utils;

namespace GameStates
{
    public class GameStateManager : Singleton<GameStateManager>
    {
        public GameState currentState;
        [SerializeField] private OnGameState onGameState;
        [SerializeField] private OnMenuState onMenuState;
        [SerializeField] private OnCompleteState onCompleteState;
        [SerializeField] private OnFailState onFailState;
        [SerializeField] private OnPauseState onPauseState;
        [SerializeField] private OnEditState onEditState;

        private void Start()
        {
            currentState = onCompleteState;
            SetState(onMenuState);
        }

        void Update()
        {
            // currentState.OnUpdateState();

            if (Input.GetKeyDown(KeyCode.P))
            {
                TogglePause();
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                SetState(onMenuState);
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                SetState(onGameState);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                SetState(onCompleteState);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(currentState.GetType());
                SetState(onEditState);
            }
        }

        public System.Type GetCurrentState()
        {
            return currentState.GetType();
        }

        private void SetState(GameState newState)
        {
            ExitState(currentState);
            currentState = newState;
            EnterState(newState);
        }

        private void EnterState(GameState state)
        {
            state.OnEnterState();
        }

        private void ExitState(GameState state)
        {
            state.OnExitState();
        }

        private void TogglePause()
        {
            if (currentState is OnPauseState)
            {
                SetState(onGameState); // Resume game
            }
            else
            {
                SetState(onPauseState); // Pause game
            }
        }
    }
}