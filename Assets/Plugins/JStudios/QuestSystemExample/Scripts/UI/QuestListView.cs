using System.Collections.Generic;
using JStudios.QuestSystem.Scripts.ScriptableObjects.Quests;
using JStudios.QuestSystem.Scripts.ScriptableObjects.References;
using UnityEngine;

namespace JStudios.QuestSystemExample.Scripts.UI
{
    public class QuestListView : MonoBehaviour
    {
        [SerializeField] private QuestSystemChannelRef Channel;
        [SerializeField] private SingleObjectiveQuestView SingleObjectiveQuestViewPrefab;
        [SerializeField] private MultipleObjectiveQuestView MultipleObjectiveQuestViewPrefab;
        
        private readonly Dictionary<string, Quest> _quests = new();
        private readonly Dictionary<string, IQuestView> _views = new();

        private float _nextQuestPosition;
        
        private void Awake()
        {
            Channel.Ref.OnQuestStartedEvent += OnQuestStartedEvent;
        }

        private void OnQuestStartedEvent(Quest quest)
        {
            _quests.Add(quest.UniqueId, quest);

            if (quest is ContextQuest or ActionQuest)
            {
                var questView = CreateSingleObjectiveQuestView(quest);

                _views.Add(quest.UniqueId, questView);
            }
            else if(quest is MultipleObjectiveQuest multipleObjectiveQuest)
            {
                var questView = CreateMultipleObjectiveQuestView(multipleObjectiveQuest);
                _views.Add(quest.UniqueId, questView);
            }
        }

        private SingleObjectiveQuestView CreateSingleObjectiveQuestView(Quest quest)
        {
            var questView = Instantiate(SingleObjectiveQuestViewPrefab, transform);
            questView.SetQuest(quest);
            
            if (questView.transform is RectTransform rectTransform) 
                rectTransform.anchoredPosition = new Vector3(0, _nextQuestPosition, 0);
            
            _nextQuestPosition -= questView.GetHeight();
            return questView;
        }

        private MultipleObjectiveQuestView CreateMultipleObjectiveQuestView(MultipleObjectiveQuest quest)
        {
            var questView = Instantiate(MultipleObjectiveQuestViewPrefab, transform);
            questView.SetQuest(quest);
            
            if (questView.transform is RectTransform rectTransform)
                rectTransform.anchoredPosition = new Vector3(0, -rectTransform.rect.height * _views.Count, 0);
            
            _nextQuestPosition -= questView.GetHeight();
            return questView;
        }
    }
}