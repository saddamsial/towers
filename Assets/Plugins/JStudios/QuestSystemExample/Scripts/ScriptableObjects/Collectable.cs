using JStudios.QuestSystem.Scripts.Interfaces;
using UnityEngine;

namespace JStudios.QuestSystemExample.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Collectable", menuName = "JStudios/Quest System Example/Quest Contexts/Collectable")]
    public class Collectable: ScriptableObject, IQuestContext
    {
        public Sprite Image;
        public string UniqueId;
        public int Points;
        public string GetUniqueId()
        {
            return UniqueId;
        }
    }
}