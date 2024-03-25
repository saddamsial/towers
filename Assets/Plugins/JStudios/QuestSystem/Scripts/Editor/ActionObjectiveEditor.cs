// Written By Asaf Benjaminov @ JStudios 2022

using JStudios.QuestSystem.Scripts.ScriptableObjects.Objectives;
using JStudios.Scripts.Editor;
using JStudios.Scripts.Editor.Models;
using UnityEditor;
using System.Linq;

namespace JStudios.QuestSystem.Scripts.Editor
{
    [CustomEditor(typeof(ActionObjective))]
    public class ActionObjectiveEditor : JAssetEditor
    {
        private readonly string[] _excludeProperties = { "raw target", "raw completed", "context" };

        protected override void OnDrawOrderedSerializedPropertyInternal(OrderedSerializedProperty orderedSerializedProperty)
        {
            if (!_excludeProperties.Contains(orderedSerializedProperty.PropertyDisplayNameLower.ToLower()))
            {
                base.OnDrawOrderedSerializedPropertyInternal(orderedSerializedProperty);
            }
        }
    }
}