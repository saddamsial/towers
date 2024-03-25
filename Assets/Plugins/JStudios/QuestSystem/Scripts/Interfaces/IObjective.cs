// Written By Asaf Benjaminov @ JStudios 2022

namespace JStudios.QuestSystem.Scripts.Interfaces
{
    /// <summary>
    /// Represents an objective or a quest with only one objective.
    /// </summary>
    public interface IObjective
    {
        /// <summary>
        /// Sets the context for this objective which will make it progress.
        /// </summary>
        /// <param name="context">The new context</param>
        void SetContext(IQuestContext context);
        
        /// <summary>
        /// Clears the context for the objective, if there is a different context then sent, it will not be cleared
        /// </summary>
        /// <param name="context">The context to be cleared</param>
        void ClearContext(IQuestContext context);
        
        /// <summary>
        /// Get the unique id of the objective
        /// </summary>
        /// <returns>Unique Id</returns>
        string GetUniqueId();
    }
}