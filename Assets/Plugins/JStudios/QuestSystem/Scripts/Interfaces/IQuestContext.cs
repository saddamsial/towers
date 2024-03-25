// Written By Asaf Benjaminov @ JStudios 2022

namespace JStudios.QuestSystem.Scripts.Interfaces
{
    /// <summary>
    /// Represents a context for a quest or objective, this is what the objective/quest "listens" to.
    /// </summary>
    public interface IQuestContext
    {
        /// <summary>
        /// Get the unique id of the context
        /// </summary>
        /// <returns>Unique Id</returns>
        string GetUniqueId();
    }
}