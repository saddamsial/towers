using GameStates;
using Tower;
using UnityEngine;
using Utils;

namespace Managers
{
    public class UserInterfaceController : Singleton<UserInterfaceController>
    {
        [SerializeField] private Transform managersPanel;
        [SerializeField] private TowerController mainTower;
        private void Start()
        {

        }

        public void ManagerEditMode()
        {
            GameStateManager.Instance.editManagerEditMode = !GameStateManager.Instance.editManagerEditMode;
            GameController.Instance.InvokeManagerMode(GameStateManager.Instance.editManagerEditMode);
            managersPanel.gameObject.SetActive(GameStateManager.Instance.editManagerEditMode);
            mainTower.floorMineList[^1].addFloorButton.gameObject.SetActive(!GameStateManager.Instance.editManagerEditMode);
        }
        public void CloseManagerEditOnBack()
        {
            if (GameStateManager.Instance.editManagerEditMode)
            {
                ManagerEditMode();
            }
        }
    }
}
