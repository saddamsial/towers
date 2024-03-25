// Written By Asaf Benjaminov @ JStudios 2022

using System.Collections.Generic;
using System.Linq;
using JStudios.QuestSystem.Scripts.Enums;
using JStudios.QuestSystem.Scripts.Interfaces;
using JStudios.Scripts.Helpers;
using JStudios.Scripts.ScriptableObjects;
using UnityEngine;

namespace JStudios.QuestSystem.Scripts.ScriptableObjects.Objectives
{
    /// <summary>
    /// A list of all the objectives in the QuestSystem.
    /// </summary>
    [CreateAssetMenu(fileName = "Objective List", menuName = "JStudios/Quest System/Floaters/Asset Lists/Objectives", order = 98)]
    public class ObjectiveList : JAssetList<Objective>
    {
        /// <summary>
        /// Get all objectives by a given state
        /// </summary>
        /// <param name="state">The state to filter by</param>
        /// <returns>IEnumerable of filtered IObjectives</returns>
        public IEnumerable<Objective> GetObjectivesByState(ActiveState state)
        {
            var result = VisibleAssets.Where(x => x.State == state);
            
            return result;
        }

        /// <summary>
        /// Get all objectives that are linked to the given context
        /// </summary>
        /// <param name="context">The context to filter by</param>
        /// <returns>IEnumerable of filtered IObjectives</returns>
        public IEnumerable<Objective> GetObjectivesByContext(IQuestContext context)
        {
            var objectiveWithNullContexts = VisibleAssets.Where(x => x.Context == null).ToList();

            if (objectiveWithNullContexts.Count> 0)
            {
                foreach (var objectiveWithNullContext in objectiveWithNullContexts)
                {
                    QuestSystemCore.Log.Error(JMessages.Errors.Quests.ContextIsNullForIObjective, objectiveWithNullContext, objectiveWithNullContext.Name);    
                }
            }
            
            var result = VisibleAssets.Where(x => x.Context != null && x.Context.GetUniqueId() == context.GetUniqueId());

            return result;
        }
    }
}