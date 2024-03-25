// Written By Asaf Benjaminov @ JStudios 2022

using System;
using System.Collections.Generic;
using System.Linq;
using JStudios.QuestSystem.Scripts.Behaviours;
using JStudios.QuestSystem.Scripts.Interfaces;
using JStudios.QuestSystem.Scripts.ScriptableObjects.Objectives;
using JStudios.Scripts.Editor;
using JStudios.Scripts.Editor.Models;
using UnityEditor;
using UnityEngine;

namespace JStudios.QuestSystem.Scripts.Editor
{
    [CustomEditor(typeof(QuestContextBehaviour))]
    public class QuestContextBehaviourEditor : JAssetEditor
    {
        private List<IObjective> _previousList = new List<IObjective>();
        
        protected override void OnDrawOrderedSerializedPropertyInternal(OrderedSerializedProperty orderedSerializedProperty)
        {
            
            if (orderedSerializedProperty.PropertyDisplayNameLower == "objectives")
            {
                try
                {
                    _previousList ??= new List<IObjective>();
                    var currentList = new List<IObjective>();

                    for (int i = 0; i < orderedSerializedProperty.Property.arraySize; i++)
                    {
                        var item = orderedSerializedProperty.Property.GetArrayElementAtIndex(i);
                        if(item.objectReferenceValue != null)
                            currentList.Add((IObjective)item.objectReferenceValue);
                    }

                    var missing = _previousList.Where(x => currentList.All(y => y.GetUniqueId() != x.GetUniqueId()));
                
                    foreach (var objective in missing)
                    {
                        objective.ClearContext((IQuestContext)serializedObject.targetObject);
                    }
                
                    _previousList = currentList; 
                
                    base.OnDrawOrderedSerializedPropertyInternal(orderedSerializedProperty);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
                
            }
            else
            {
                base.OnDrawOrderedSerializedPropertyInternal(orderedSerializedProperty);
            }
        }
    }
}