// Written By Asaf Benjaminov @ JStudios 2022

using JStudios.QuestSystem.Scripts.ScriptableObjects.Quests;
using JStudios.Scripts.Editor;
using JStudios.Scripts.Editor.Helpers;
using JStudios.Scripts.Editor.Models;
using UnityEditor;
using System.Linq;
using JStudios.QuestSystem.Scripts.ScriptableObjects.Objectives;
using UnityEngine;

namespace JStudios.QuestSystem.Scripts.Editor
{
    [CustomEditor(typeof(ActionQuest))]
    public class PerformActionQuestEditor : JAssetEditor
    {
        private readonly string[] _excludeProperties = { "raw target", "raw completed", "completion requirement" };
        
        protected override void OnDrawOrderedSerializedPropertyInternal(OrderedSerializedProperty orderedSerializedProperty)
        {
            if (orderedSerializedProperty.PropertyDisplayNameLower == "objective")
            {
                var objectiveObject = JEditorHelper.GetObjectFromProperty(orderedSerializedProperty.Property);
                var property = objectiveObject.FindProperty("CompleteInstantly");
                
                var objective = (ActionObjective)objectiveObject.targetObject;
                objective.CompleteInstantly = EditorGUILayout.Toggle(new GUIContent("Complete Instantly"), property.boolValue);
                
                property = objectiveObject.FindProperty("_completionRequirement");
                base.OnDrawSerializedPropertyInternal(property);
            }
            else if (!_excludeProperties.Contains(orderedSerializedProperty.PropertyDisplayNameLower.ToLower()))
            {
                base.OnDrawOrderedSerializedPropertyInternal(orderedSerializedProperty);
            }
        }
    }
}