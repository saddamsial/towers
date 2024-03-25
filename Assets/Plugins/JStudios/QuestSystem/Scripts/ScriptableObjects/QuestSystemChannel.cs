// Written By Asaf Benjaminov @ JStudios 2022

using JStudios.QuestSystem.Scripts.Interfaces;
using JStudios.QuestSystem.Scripts.ScriptableObjects.Objectives;
using JStudios.QuestSystem.Scripts.ScriptableObjects.Quests;
using JStudios.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace JStudios.QuestSystem.Scripts.ScriptableObjects
{
    /// <summary>
    /// Main channel of communication for the QuestSystem
    /// </summary>
    // [CreateAssetMenu(fileName = "Quest System Channel", menuName = "JStudios/Quest System/Floaters/Channel", order = 99)]
    public class QuestSystemChannel : JAsset
    {
        public UnityAction<Objective> OnObjectiveProgressEvent;
        public UnityAction<Objective> OnObjectiveStartedEvent;
        public UnityAction<Objective> OnObjectivePendingCompleteEvent;
        public UnityAction<Objective> OnObjectiveCompletedEvent;
        public UnityAction<Objective> OnObjectiveStateChangedEvent;

        public UnityAction<Quest> OnQuestProgressEvent; 
        public UnityAction<Quest> OnQuestStartedEvent;
        public UnityAction<Quest> OnQuestPendingCompleteEvent;
        public UnityAction<Quest> OnQuestCompletedEvent;
        public UnityAction<Quest> OnQuestStateChangedEvent;
        public UnityAction<Quest> OnQuestAvailabilityChangedEvent;
        
        public ObjectiveList ObjectiveList;
        public QuestList QuestList;
        public DummyContext DummyContext;
        
        public void Progress(IQuestContext context = null, float amountOfProgress = 1)
        {
            context ??= QuestSystemCore.GlobalContextInstance;
            
            var quests = QuestList.GetQuestsByContext(context);
            
            foreach (var quest in quests)
            {
                quest.Progress(context, amountOfProgress);
            }
        }
        
        private void OnEnable()
        {
            SubscribeObjectives();
            SubscribeQuests();
        }

        private void SubscribeQuests()
        {
            if (QuestList == null) return;
            
            UnsubscribeQuests();

            foreach (var quest in QuestList.AllAssets)
            {
                quest.OnCompletedEvent += OnQuestCompletedListener;
                quest.OnStateChangedEvent += OnQuestStateChangedListener;
                quest.OnProgressEvent += OnQuestProgressListener;
                quest.OnActiveEvent += OnQuestStartedListener;
                quest.OnPendingCompletedEvent += OnQuestPendingCompleteListener;
                quest.OnAvailabilityChangedEvent += OnQuestAvailabilityChangedListener;
            }
        }

        private void SubscribeObjectives()
        {
            if (ObjectiveList == null) return;
            
            UnsubscribeObjectives();

            foreach (var objective in ObjectiveList.AllAssets)
            {
                objective.OnCompletedEvent += OnObjectiveCompletedListener;
                objective.OnStateChangedEvent += OnObjectiveStateChangedListener;
                objective.OnProgressEvent += OnObjectiveProgressListener;
                objective.OnActiveEvent += OnObjectiveStartedListener;
                objective.OnPendingCompletedEvent += OnObjectivePendingCompleteListener;
            }
        }

        private void OnDestroy()
        {
            UnsubscribeObjectives();

            UnsubscribeQuests();
        }

        private void UnsubscribeQuests()
        {
            if (QuestList != null)
            {
                foreach (var quest in QuestList.AllAssets)
                {
                    quest.OnCompletedEvent -= OnQuestCompletedListener;
                    quest.OnStateChangedEvent -= OnQuestStateChangedListener;
                    quest.OnProgressEvent -= OnQuestProgressListener;
                    quest.OnActiveEvent -= OnQuestStartedListener;
                    quest.OnPendingCompletedEvent -= OnQuestPendingCompleteListener;
                    quest.OnAvailabilityChangedEvent -= OnQuestAvailabilityChangedListener;
                }
            }
        }

        private void UnsubscribeObjectives()
        {
            if (ObjectiveList != null)
            {
                foreach (var objective in ObjectiveList.AllAssets)
                {
                    objective.OnCompletedEvent -= OnObjectiveCompletedListener;
                    objective.OnStateChangedEvent -= OnObjectiveStateChangedListener;
                    objective.OnProgressEvent -= OnObjectiveProgressListener;
                    objective.OnActiveEvent -= OnObjectiveStartedListener;
                    objective.OnPendingCompletedEvent -= OnObjectivePendingCompleteListener;
                }
            }
        }


        private void OnObjectiveStartedListener(Objective objective)
        {
            OnObjectiveStartedEvent?.Invoke(objective);
        }
        
        private void OnObjectiveProgressListener(Objective objective)
        {
            OnObjectiveProgressEvent?.Invoke(objective);
        }
        
        private void OnObjectiveStateChangedListener(Objective objective)
        {
            OnObjectiveStateChangedEvent?.Invoke(objective);
        }
        
        private void OnObjectivePendingCompleteListener(Objective objective)
        {
            OnObjectivePendingCompleteEvent?.Invoke(objective);
        }
        
        private void OnObjectiveCompletedListener(Objective objective)
        {
            OnObjectiveCompletedEvent?.Invoke(objective);
        }
        
        private void OnQuestStartedListener(Quest quest)
        {
            OnQuestStartedEvent?.Invoke(quest);
        }
        
        private void OnQuestProgressListener(Quest quest)
        {
            OnQuestProgressEvent?.Invoke(quest);
        }
        
        private void OnQuestStateChangedListener(Quest quest)
        {
            OnQuestStateChangedEvent?.Invoke(quest);
        }
        
        private void OnQuestPendingCompleteListener(Quest quest)
        {
            OnQuestPendingCompleteEvent?.Invoke(quest);
        }
        
        private void OnQuestCompletedListener(Quest quest)
        {
            OnQuestCompletedEvent?.Invoke(quest);
        }
        
        private void OnQuestAvailabilityChangedListener(Quest quest)
        {
            OnQuestAvailabilityChangedEvent?.Invoke(quest);
        }

        [ContextMenu("Validate")]
        public void Validate()
        {
            foreach (var questListAllAsset in QuestList.AllAssets)
            {
                questListAllAsset.Validate();
            }
        }
        
        public void Refresh()
        {
            foreach (var quest in QuestList.AllAssets)
            {
                quest.Refresh();
            }
        }
    }
}