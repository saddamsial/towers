using System;
using System.Collections.Generic;
using JStudios.QuestSystem.Scripts.Enums;
using JStudios.QuestSystem.Scripts.ScriptableObjects.Quests;
using UnityEngine;

namespace JStudios.QuestSystemExample.Scripts.UI
{
    public class MultipleObjectiveQuestView : QuestView<MultipleObjectiveQuest>
    {
        private readonly List<ObjectiveProgressView> _objectiveViews = new();
        [SerializeField] private ObjectiveProgressView ObjectiveProgressViewPrefab;
        [SerializeField] private RectTransform ObjectivesOwner;

        private void OnDisable()
        {
            UnsubscribeQuest();
        }

        public override void SetQuest(MultipleObjectiveQuest quest)
        {
            UnsubscribeQuest();
            
            Quest = quest;
            
            quest.OnStateChangedEvent += OnStateChangedEvent;
            
            var rectTransform = (transform as RectTransform);

            if (rectTransform == null) return;
            
            foreach (var objective in quest.Objectives)
            {
                var view = CreateObjectiveView();
                view.SetObjective(objective);
                _objectiveViews.Add(view);

                var height = GetObjectiveViewHeight(view);

                var sizeDelta = rectTransform.sizeDelta;
                sizeDelta = new Vector2(sizeDelta.x, sizeDelta.y + height);
                rectTransform.sizeDelta = sizeDelta;
            }
            
            QuestName.SetText(quest.Name);
            SetStateText(quest);
        }

        private void UnsubscribeQuest()
        {
            if (Quest != null)
            {
                Quest.OnStateChangedEvent -= OnStateChangedEvent;
            }
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

        private ObjectiveProgressView CreateObjectiveView()
        {
            var objectiveView = Instantiate(ObjectiveProgressViewPrefab, ObjectivesOwner);
            var rectTransform = (objectiveView.transform as RectTransform);

            if (rectTransform is not null)
                rectTransform.anchoredPosition =
                    new Vector3(0, -GetObjectiveViewHeight(objectiveView) * _objectiveViews.Count, 0);
            
            return objectiveView;
        }

        private float GetObjectiveViewHeight(ObjectiveProgressView objectiveView)
        {
            if (objectiveView.transform is RectTransform rectTransform) 
                return rectTransform.rect.height;
            
            return 0;
        }
    }
}