// Written By Asaf Benjaminov @ JStudios 2022

using JStudios.QuestSystem.Scripts.ScriptableObjects.Objectives;
using JStudios.Scripts.ScriptableObjects;
using UnityEngine;

namespace JStudios.QuestSystem.Scripts.ScriptableObjects.References
{
    /// <summary>
    /// Reference to the objective list
    /// </summary>
    [CreateAssetMenu(fileName = "Objective List Ref", 
        menuName = "JStudios/Quest System/References/Objective List")]
    public class ObjectiveListRef: JInternalAssetReference<ObjectiveList>
    {
        
    }
}