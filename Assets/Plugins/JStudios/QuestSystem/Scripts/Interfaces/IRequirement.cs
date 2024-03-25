// Written By Asaf Benjaminov @ JStudios 2022

namespace JStudios.QuestSystem.Scripts.Interfaces
{
    /// <summary>
    /// Represents a requirement in the QuestSystem.
    /// Use cases include
    ///     1. Quest requirement (to be available)
    ///     2. Action Quest requirement
    /// </summary>
    public interface IRequirement
    {
        /// <summary>
        /// Check if the requirement is met
        /// </summary>
        /// <returns>Indication weather the requirement is met this frame or not</returns>
        bool IsMet();
    }
}