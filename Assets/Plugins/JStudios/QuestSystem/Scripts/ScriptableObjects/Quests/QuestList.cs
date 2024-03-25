// Written By Asaf Benjaminov @ JStudios 2022

using System.Collections.Generic;
using System.Linq;
using JStudios.QuestSystem.Scripts.Enums;
using JStudios.QuestSystem.Scripts.Interfaces;
using JStudios.Scripts.ScriptableObjects;
using UnityEngine;

namespace JStudios.QuestSystem.Scripts.ScriptableObjects.Quests
{
    /// <summary>
    /// A list of all the quests in the QuestSystem
    /// </summary>
    [CreateAssetMenu(fileName = "Quest List", menuName = "JStudios/Quest System/Floaters/Asset Lists/Quests", order = 97)]
    public class QuestList : JAssetList<Quest>
    {
        /// <summary>
        /// Gets all quests that are supported by the context send
        /// </summary>
        /// <param name="context">The context to filter by</param>
        /// <returns>IEnumerable of quests that support the given context</returns>
        public IEnumerable<Quest> GetQuestsByContext(IQuestContext context)
        {
            var questsWithContext = VisibleAssets.Where(x => x.HasContext(context));

            return questsWithContext;
        } 
        
        /// <summary>
        /// Get all quests that are available at the moment.
        /// </summary>
        /// <returns>IEnumerable of available quests</returns>
        public IEnumerable<Quest> GetAvailableQuests()
        {
            var result = GetQuestsByState(ActiveState.PendingActive).Where(x => x.MeetsRequirements());

            return result;
        }

        /// <summary>
        /// Get all quests with the given state
        /// </summary>
        /// <param name="state">The state to filter by</param>
        /// <returns>IEnumerable of filtered quests</returns>
        public IEnumerable<Quest> GetQuestsByState(ActiveState state)
        {
            var result = VisibleAssets.Where(x => x.State == state);

            return result;
        }
    }
}