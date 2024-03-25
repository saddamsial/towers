// Written By Asaf Benjaminov @ JStudios 2022

namespace JStudios.QuestSystem.Scripts.Interfaces
{
    /// <summary>
    /// Used to create unique completion actions (use with scriptable objects)
    /// </summary>
    public interface ICompletionAction
    {
        /// <summary>
        /// Executes the logic for the completion action.
        /// </summary>
        void Execute();
    }
}