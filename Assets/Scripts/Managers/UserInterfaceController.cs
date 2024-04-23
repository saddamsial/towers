using DG.Tweening;
using GameStates;
using TMPro;
using Tower;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Managers
{
    public class UserInterfaceController : Singleton<UserInterfaceController>
    {
        [SerializeField] private Transform managersPanel;
        [SerializeField] private TowerController mainTower;
        [SerializeField] private GamePresets gamePresets;
        GameData gameData;
        public TMP_Text moneyCount, gemCount, ticketCount, gearCount, levelCount, inGameMoney;
        public Image stepFillImage;
        public int tempMoney;
        public GameObject levelUpPanel;

        protected override void OnDisable()
        {
            gameData.onMoneyUpdated -= UpdateMoney;
            gameData.onGemUpdated -= UpdateGem;
            gameData.onGearUpdated -= UpdateGear;
            gameData.onTicketUpdated -= UpdateTicket;
            gameData.onStepUpdated -= UpdateStep;
            gameData.onLevelUpdated -= UpdateLevel;
            base.OnDisable();
        }
        private void Start()
        {
            gameData = GameStateManager.Instance.gameData;
            gameData.onMoneyUpdated += UpdateMoney;
            gameData.onGemUpdated += UpdateGem;
            gameData.onGearUpdated += UpdateGear;
            gameData.onTicketUpdated += UpdateTicket;
            gameData.onStepUpdated += UpdateStep;
            gameData.onLevelUpdated += UpdateLevel;
            LevelStepFirstSetup();
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
        private void UpdateMoney(int value)
        {
            moneyCount.text = value.ToString();
        }
        private void UpdateGear(int value)
        {
            gearCount.text = value.ToString();
        }
        private void UpdateTicket(int value)
        {
            ticketCount.text = value.ToString();
        }
        private void UpdateGem(int value)
        {
            gemCount.text = value.ToString();
        }
        private void UpdateStep(int value)
        {
            // stepFillImage.fillAmount = StepValueCalculation();
            DOTween.To(() => stepFillImage.fillAmount, x => stepFillImage.fillAmount = x, StepValueCalculation(), .1f).OnComplete(() =>
            {
                if (stepFillImage.fillAmount >= .99f)
                {
                    levelUpPanel.SetActive(true);
                }
            });


        }
        private void UpdateLevel(int value)
        {
            levelUpPanel.SetActive(false);
            levelCount.text = "Level " + (value + 1).ToString();
            stepFillImage.fillAmount = 0;
            gameData.Step = 0;
        }
        public void LevelStepFirstSetup()
        {
            UpdateStep(0);
            levelCount.text = "Level " + (gameData.Level + 1).ToString();
        }
        public float StepValueCalculation()
        {
            var requiredStepCount = gamePresets.stepCountsForEachLevel[gameData.Level];
            var fillVal = 1.0f / requiredStepCount * gameData.Step;
            return fillVal;
        }
        public void InGameMoney(int m)
        {
            tempMoney += m;
            inGameMoney.text = tempMoney.ToString();
        }
    }
}
