// Written By Asaf Benjaminov @ JStudios 2022

using System.Linq;
using JStudios.QuestSystem.Scripts.ScriptableObjects.Quests;
using JStudios.Scripts.Editor;
using JStudios.Scripts.Editor.Helpers;
using JStudios.Scripts.Editor.Models;
using UnityEditor;

namespace JStudios.QuestSystem.Scripts.Editor
{
    [CustomEditor(typeof(ContextQuest))]
    public class ContextQuestEditor : JAssetEditor
    {
        private readonly string[] _excludeProperties = { "raw target" };

        protected override void OnDrawOrderedSerializedPropertyInternal(OrderedSerializedProperty orderedSerializedProperty)
        {
            if (ShowPropertiesMap.ContainsKey(orderedSerializedProperty.PropertyDisplayNameLower))
            {
                ShowPropertiesMap[orderedSerializedProperty.PropertyDisplayNameLower] = JEditorHelper.DrawCustomEditorForProperty(new DrawCustomPropertyInfo()
                {
                    IsEditable = true,
                    SerializedProperty = orderedSerializedProperty.Property,
                    ShowInfo = ShowPropertiesMap[orderedSerializedProperty.PropertyDisplayNameLower],
                    ShouldDisplayReference = true,
                    ErrorMessage = "Error while finding Objective for quest",
                    SerializedObject = serializedObject,
                    PropertiesToExclude =
                    {
                        "raw completed",
                        "active state",
                        "requirements",
                        "name",
                        "hide from list",
                        "completion actions"
                    }
                });
            }
            else if (!_excludeProperties.Contains(orderedSerializedProperty.PropertyDisplayNameLower.ToLower()))
            {
                base.OnDrawOrderedSerializedPropertyInternal(orderedSerializedProperty);
            }
        }
    }
}