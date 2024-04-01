using GameStates;
using UnityEngine;
using Utils;

namespace Managers
{
    public class UserInterfaceController : Singleton<UserInterfaceController>
    {
        [SerializeField] private Transform managersPanel;
        private void Start()
        {

        }

        public void ManagerEditMode()
        {
            GameStateManager.Instance.editManagerEditMode = !GameStateManager.Instance.editManagerEditMode;
            managersPanel.gameObject.SetActive(GameStateManager.Instance.editManagerEditMode);
        }
    }
}
