using UnityEngine;
using Utils;
using UnityEngine.SceneManagement;
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
        public bool editManagerEditMode;
        public GameData gameData;

        private void Start()
        {
            gameData = (GameData)DataPersistenceController.Instance.GetData("game", new GameData());
            Debug.Log("play count-> " + gameData.playCount);
            if (gameData.playCount == 0)
            {
                gameData.FirstFillManagers();
            }
            gameData.playCount++;
            currentState = onCompleteState;
            SetState(onMenuState);
        }

        void Update()
        {
            // if (Input.GetKeyDown(KeyCode.M))
            // {
            //     SetState(onMenuState);
            // }
            if (Input.GetKeyDown(KeyCode.G))
            {
                SetState(onGameState);
            }
        }
        public System.Type GetCurrentState()
        {
            return currentState.GetType();
        }
        public bool IsManagerEditMode()
        {
            return editManagerEditMode && IsEditState();
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