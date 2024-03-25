// Written By Asaf Benjaminov @ JStudios 2022

namespace JStudios.QuestSystem.Scripts.Interfaces
{
    /// <summary>
    /// An interface to make all assets withing the QuestSystem have the same basic interface.
    /// </summary>
    public interface IQuestSystemAsset
    {
        /// <summary>
        /// Resets only properties related to the QuestSystem
        /// </summary>
        void SoftReset();
    }
}