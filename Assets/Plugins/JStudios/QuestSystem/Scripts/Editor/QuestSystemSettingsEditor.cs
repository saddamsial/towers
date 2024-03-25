// Written By Asaf Benjaminov @ JStudios 2022

using JStudios.Scripts.Editor;
using JStudios.Scripts.Editor.Helpers;
using JStudios.Scripts.Editor.Models;
using UnityEditor;

namespace JStudios.QuestSystem.Scripts.Editor
{
    [CustomEditor(typeof(ScriptableObjects.QuestSystemCore))]
    [CanEditMultipleObjects]
    public class QuestSystemSettingsEditor : JAssetEditor
    {
        protected override void OnDrawOrderedSerializedPropertyInternal(OrderedSerializedProperty orderedSerializedProperty)
        {
            if (ShowPropertiesMap.ContainsKey(orderedSerializedProperty.PropertyDisplayNameLower))
            {
                ShowPropertiesMap[orderedSerializedProperty.PropertyDisplayNameLower] = 
                    JEditorHelper.DrawCustomEditorForProperty(new DrawCustomPropertyInfo()
                {
                    IsEditable = true,
                    SerializedProperty = orderedSerializedProperty.Property,
                    ShowInfo = ShowPropertiesMap[orderedSerializedProperty.PropertyDisplayNameLower],
                    ShouldDisplayReference = true,
                    ErrorMessage = "Error while serializing JSettings",
                    SerializedObject = serializedObject
                });
            }
            else
            {
                base.OnDrawOrderedSerializedPropertyInternal(orderedSerializedProperty);
            }
        }
    }
}