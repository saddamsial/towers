// Written By Asaf Benjaminov @ JStudios 2022

using System;
using JStudios.QuestSystem.Scripts.Interfaces;
using JStudios.Scripts.Attributes;
using JStudios.Scripts.ScriptableObjects;

namespace JStudios.QuestSystem.Scripts.ScriptableObjects.Quests
{
    /// <summary>
    /// Default context for internal use.
    /// </summary>
    [HideInCustomAssetWindow]
    [Serializable]
    public class DummyContext : JAsset, IQuestContext
    {
        private void OnEnable()
        {
            UniqueId ??= Guid.NewGuid().ToString();
        }

        public string GetUniqueId()
        {
            return UniqueId;
        }
    }
}