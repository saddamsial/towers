// Written By Asaf Benjaminov @ JStudios 2022

using System.ComponentModel;
using System.Linq;
using JStudios.QuestSystem.Scripts.ScriptableObjects.Objectives;
using JStudios.Scripts.Helpers;
using UnityEngine;

namespace JStudios.QuestSystem.Scripts.ScriptableObjects.Quests
{
    /// <summary>
    /// Base class for all quests that support a context.
    /// </summary>
    [DisplayName("Quest")]
    [CreateAssetMenu(fileName = "New Quest",
        menuName = "JStudios/Quest System/Quests/Single Objective Quests/Quest")]
    public class ContextQuest : SingleObjectiveQuest
    {
        protected override Objective TryCreateObjectiveInternal()
        {
            var asset = JStudiosAssetsHelper.FindInternalAssets<Objective>()
                .FirstOrDefault(x => x.name == UniqueId);

            Objective objective;
            
            if (asset != null)
            {
                JStudiosLog.Info("Found Objective " + asset.name);
                objective = asset;
            }
            else
            {
                objective = JStudiosAssetsHelper.TryCreateInternalAsset<Objective>($"{(string.IsNullOrEmpty(Name) ? "New" : Name)} Objective", "Objectives");
                JStudiosLog.Info(JMessages.Info.Quests.CreatedObjectiveForSoq, this.Name);
            }
            return objective;
        }
    }
}