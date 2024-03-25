// Written By Asaf Benjaminov @ JStudios 2022

using System.ComponentModel;
using JStudios.QuestSystem.Scripts.ScriptableObjects.Objectives;
using UnityEngine;

namespace JStudios.QuestSystem.Scripts.ScriptableObjects.Quests
{
    /// <summary>
    /// Synchronous multiple objective quest, objectives listen to progress only when the previous objective is complete. 
    /// </summary>
    [DisplayName("Sync M.O.Q")]
    [CreateAssetMenu(fileName = "New Sync Multiple Objective Quest", 
        menuName = "JStudios/Quest System/Quests/Multiple Objective Quests/Sync Quest")]
    public class SyncMultipleObjectiveQuest : MultipleObjectiveQuest
    {
        private int _currentActiveObjectiveIndex;
        protected override void StartInternal()
        {
            StartCurrentObjective();
        }

        protected override void OnObjectiveCompletedEvent(Objective objective)
        {
            _currentActiveObjectiveIndex++;
            CompletedObjectivesCount++;
            
            if (_currentActiveObjectiveIndex == ObjectiveCount)
            {
                Complete();
            }
            else
            {
                StartCurrentObjective();
            }
        }

        void StartCurrentObjective()
        {
            Objectives[_currentActiveObjectiveIndex].Activate();
        }
        
        protected override void SoftResetInternal()
        {
            _currentActiveObjectiveIndex = 0;
        }
    }
}