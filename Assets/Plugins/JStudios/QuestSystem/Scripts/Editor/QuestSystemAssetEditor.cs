// Written By Asaf Benjaminov @ JStudios 2022

using System.Collections.Generic;
using JStudios.QuestSystem.Scripts.Interfaces;
using JStudios.QuestSystem.Scripts.ScriptableObjects;
using JStudios.QuestSystem.Scripts.ScriptableObjects.Quests;
using JStudios.Scripts.Editor;
using JStudios.Scripts.Editor.Models;
using JStudios.Scripts.Helpers;
using UnityEditor;
using UnityEngine;

namespace JStudios.QuestSystem.Scripts.Editor
{
    [CustomEditor(typeof(QuestSystemAsset<>), true)]
    public class QuestSystemAssetEditor : JAssetEditor
    {
        private GUIStyle labelStyle;
        
        protected override void OnInspectorGUIInternal(List<OrderedSerializedProperty> serializedProperties)
        {
            labelStyle ??= new GUIStyle(GUI.skin.label)
            {
                normal =
                {
                    textColor = Color.white
                },
                fontStyle = FontStyle.Bold
            };
            
            base.OnInspectorGUIInternal(serializedProperties);
        }
    }
}