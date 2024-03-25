// Written By Asaf Benjaminov @ JStudios 2022

using JStudios.QuestSystem.Scripts.ScriptableObjects;
using JStudios.Scripts.Helpers;
using UnityEditor;

namespace JStudios.QuestSystem.Scripts.Helpers
{
    /// <summary>
    /// The static class for the JStudios menu items
    /// </summary>
    public static class JQuestSystemMenu
    {
        [MenuItem("Tools/JStudios/Quest System/Fix")]
        private static void FixQuestSystem()
        {
            var core = JStudiosAssetsHelper.FindAsset<QuestSystemCore>();

            if (core == null) return;
            
            core.Fix("JQuestSystemMenu");
        }
    }
}