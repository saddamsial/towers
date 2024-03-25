// Written By Asaf Benjaminov @ JStudios 2022

using System.ComponentModel;
using System.Linq;
using JStudios.QuestSystem.Scripts.ScriptableObjects.Objectives;
using JStudios.Scripts.Helpers;
using UnityEngine;

namespace JStudios.QuestSystem.Scripts.ScriptableObjects.Quests
{
    /// <summary>
    /// A quest that is completed once a single action is performed.
    /// </summary>
    [DisplayName("Action Quest")]
    [CreateAssetMenu(fileName = "New Perform Action Quest",
        menuName = "JStudios/Quest System/Quests/Single Objective Quests/Action Quest")]
    public class ActionQuest : SingleObjectiveQuest
    {
        protected override Objective TryCreateObjectiveInternal()
        {
            var asset = JStudiosAssetsHelper.FindInternalAssets<ActionObjective>()
                .FirstOrDefault(x => x.name == UniqueId);

            Objective objective;
            
            if (asset != null)
            {
                JStudiosLog.Info("Found Objective " + asset.name);
                objective = asset;
            }
            else
            {
                objective = JStudiosAssetsHelper.TryCreateInternalAsset<ActionObjective>($"{(string.IsNullOrEmpty(Name) ? "New" : Name)} Objective", "Objectives");
                JStudiosLog.Info(JMessages.Info.Quests.CreatedObjectiveForSoq, this.Name);
            }
            return objective;
        }
    }
}