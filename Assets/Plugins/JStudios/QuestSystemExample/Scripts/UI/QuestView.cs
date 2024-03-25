using JStudios.QuestSystem.Scripts.ScriptableObjects.Quests;
using TMPro;
using UnityEngine;

namespace JStudios.QuestSystemExample.Scripts.UI
{
    public abstract class QuestView<T> : MonoBehaviour, IQuestView where T : Quest
    {
        [SerializeField] protected TextMeshProUGUI QuestName;
        [SerializeField] protected TextMeshProUGUI QuestState;
        
        [HideInInspector] public T Quest;

        public abstract void SetQuest(T quest);
        
        public float GetHeight()
        {
            var rectTransform = (transform as RectTransform);
            
            if (rectTransform is null) return 0;
            
            var sizeDelta = rectTransform.sizeDelta;
            return sizeDelta.y;
        }
    }
}