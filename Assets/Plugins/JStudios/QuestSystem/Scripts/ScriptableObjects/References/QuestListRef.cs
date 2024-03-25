// Written By Asaf Benjaminov @ JStudios 2022

using JStudios.QuestSystem.Scripts.ScriptableObjects.Quests;
using JStudios.Scripts.ScriptableObjects;
using UnityEngine;

namespace JStudios.QuestSystem.Scripts.ScriptableObjects.References
{
    /// <summary>
    /// Reference to the quest list
    /// </summary>
    [CreateAssetMenu(fileName = "Quest List Ref", 
        menuName = "JStudios/Quest System/References/Quest List")]
    public class QuestListRef : JInternalAssetReference<QuestList>
    {
        
    }
}