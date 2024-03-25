// Written By Asaf Benjaminov @ JStudios 2022

using System.Linq;
using JStudios.Scripts.Editor.Models;
using UnityEditor;
using UnityEngine;

namespace JStudios.Scripts.Editor.Helpers
{
    public static class JEditorHelper
    {
        public static SerializedObject GetObjectFromProperty(SerializedProperty property)
        {
            if (property.objectReferenceValue == null) return null;
            
            var serializedObject = new SerializedObject(property.objectReferenceValue);

            return serializedObject;
        }
        
        public static bool DrawCustomEditorForProperty(DrawCustomPropertyInfo info)
        {
            info.PropertiesToExclude = info.PropertiesToExclude.Select(x => x.ToLower()).ToList();
            info.PropertiesToExclude.Add("script");
            
            info.ShowInfo = EditorGUILayout.Foldout(info.ShowInfo, 
                $"{info.SerializedProperty.displayName} Info {(!info.IsEditable ? "(Readonly)" : string.Empty )}", 
                new GUIStyle(GUI.skin.FindStyle("foldout"))
                {
                    fontStyle = FontStyle.Bold
                });

            if (!info.ShowInfo) return false;
            
            GUI.enabled = info.IsEditable;
            EditorGUI.indentLevel++;
            SerializedObject contextObject = null;

            if (info.ShouldDisplayReference)
            {
                info.SerializedObject.Update();
                EditorGUILayout.PropertyField(info.SerializedProperty);
                info.SerializedObject.ApplyModifiedProperties();
            }
            
            if (info.SerializedProperty.objectReferenceValue != null)
            {
                contextObject = GetObjectFromProperty(info.SerializedProperty);    
            }

            if (contextObject == null)
            {
                EditorGUILayout.LabelField(info.ErrorMessage);
            }
            else
            {
                var iterator = contextObject.GetIterator();
                var property = iterator.NextVisible(true);

                while (property)
                {
                    if (!info.PropertiesToExclude.Contains(iterator.displayName.ToLower()))
                    {
                        contextObject.Update();
                        EditorGUILayout.PropertyField(iterator);
                        contextObject.ApplyModifiedProperties();
                    }

                    property = iterator.NextVisible(false);
                }
            }
            EditorGUI.indentLevel--;
            GUI.enabled = true;

            return true;
        }
    }
}