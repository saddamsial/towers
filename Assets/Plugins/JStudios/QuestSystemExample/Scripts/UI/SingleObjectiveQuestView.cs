using System;
using JStudios.QuestSystem.Scripts.Enums;
using JStudios.QuestSystem.Scripts.ScriptableObjects.Quests;
using UnityEngine;

namespace JStudios.QuestSystemExample.Scripts.UI
{
    public class SingleObjectiveQuestView : QuestView<Quest>
    {
        public ProgressBar QuestProgressBar;
        

        private void Awake()
        {
            QuestProgressBar.SetPercentage(0);
        }

        public override void SetQuest(Quest quest)
        {
            Quest = quest;
            quest.OnProgressEvent += OnProgressEvent;
            quest.OnCompletedEvent += OnCompletedEvent;
            quest.OnPendingCompletedEvent += OnPendingCompleteEvent;
            quest.OnStateChangedEvent += OnStateChangedEvent;

            QuestName.SetText(quest.Name);
            SetStateText(quest);
        }

        private void OnStateChangedEvent(Quest quest)
        {
            SetStateText(quest);
        }

        private void SetStateText(Quest quest)
        {
            var state = Enum.GetName(typeof(ActiveState), quest.State);
            QuestState.SetText(state);
        }

        private void OnPendingCompleteEvent(Quest arg0)
        {
            QuestProgressBar.SetColor(Color.yellow);
        }

        private void OnCompletedEvent(Quest arg0)
        {
            QuestProgressBar.SetColor(Color.gray);
        }

        private void OnProgressEvent(Quest quest)
        {
            QuestProgressBar.SetPercentage(quest.PercentageCompleted);
        }
    }
}