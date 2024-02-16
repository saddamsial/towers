using UnityEngine;
using Utils;
using UnityEngine.SceneManagement;
using UnityEditorInternal;
namespace GameStates
{
    public class GameStateManager : Singleton<GameStateManager>
    {
        public GameState currentState;
        public OnGameState onGameState;
        public OnMenuState onMenuState;
        public OnCompleteState onCompleteState;
        public OnFailState onFailState;
        public OnPauseState onPauseState;
        public OnEditState onEditState;

        public GameData data;

        private void Start()
        {
            data = (GameData)DataPersistenceController.Instance.GetData("game", new GameData());
            data.playCount++;
            //Debug.Log("play count-> " + data.playCount);
            currentState = onCompleteState;
            SetState(onMenuState);
        }

        void Update()
        {
            // currentState.OnUpdateState();

            // if (Input.GetKeyDown(KeyCode.P))
            // {
            //     TogglePause();
            // }

            if (Input.GetKeyDown(KeyCode.M))
            {
                SetState(onMenuState);
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                SetState(onGameState);
            }

            // if (Input.GetKeyDown(KeyCode.C))
            // {
            //     SetState(onCompleteState);
            // }

            // if (Input.GetKeyDown(KeyCode.E))
            // {
            //     Debug.Log(currentState.GetType());
            //     SetState(onEditState);
            // }
        }

        public System.Type GetCurrentState()
        {
            return currentState.GetType();
        }

        public bool IsEditState()
        {
            return GetCurrentState() == typeof(OnEditState);
        }

        public bool IsGameState()
        {
            return GetCurrentState() == typeof(OnGameState);
        }

        public bool IsMenuState()
        {
            return GetCurrentState() == typeof(OnMenuState);
        }

        public void SetState(GameState newState)
        {
            ExitState(currentState);
            // Debug.Log(newState.name);
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

        public void Restart()
        {
            SceneManager.LoadScene(0);
        }
    }
}