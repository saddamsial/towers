using JStudios.QuestSystem.Scripts.Interfaces;
using UnityEngine;

namespace JStudios.QuestSystemExample.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Level Requirement", menuName = "JStudios/Quest System Example/Requirements/Level Requirement")]
    public class LevelRequirement : ScriptableObject, IRequirement
    {
        public PlayerStats PlayerStats;
        public int RequiredLevel;
        
        public bool IsMet()
        {
            var isMet = PlayerStats.Level >= RequiredLevel;

            return isMet;
        }
    }
}