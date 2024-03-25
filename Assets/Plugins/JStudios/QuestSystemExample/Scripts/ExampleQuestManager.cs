using JStudios.QuestSystem.Scripts.ScriptableObjects;
using JStudios.QuestSystem.Scripts.ScriptableObjects.Quests;
using JStudios.QuestSystemExample.Scripts.ScriptableObjects;
using UnityEngine;

namespace JStudios.QuestSystemExample.Scripts
{
    public class ExampleQuestManager : MonoBehaviour
    {
        [SerializeField] private PlayerStats PlayerStats;
        [SerializeField] private QuestSystemCore QuestSystemCore;

        [ContextMenu("Reset Example")]
        public void ResetExample()
        {
            Debug.Log("Reset Example");
            foreach (var quest in QuestSystemCore.QuestList.AllAssets)
            {
                quest.SoftReset();
            }

            PlayerStats.Level = 0;
            PlayerStats.Points = 0;
        }
        
        private void Awake()
        {
            ResetExample();

            QuestSystemCore.Channel.OnQuestAvailabilityChangedEvent += OnQuestAvailabilityChangedEvent;
            PlayerStats.OnPointsGainedEvent += OnPointsGainedEvent;
        }

        private void OnPointsGainedEvent()
        {
            QuestSystemCore.Channel.Refresh();
            QuestSystemCore.Channel.Progress();
        }

        private void Start()
        {
            QuestSystemCore.Refresh();
            
            foreach (var questListAllAsset in QuestSystemCore.QuestList.AllAssets)
            {
                if (questListAllAsset.IsAvailable())
                {
                    questListAllAsset.Activate();
                }
            }
        }

        private void OnQuestAvailabilityChangedEvent(Quest quest)
        {
            if (!quest.IsAvailable()) return;
            
            quest.Activate();
        }
    }
}