// Written By Asaf Benjaminov @ JStudios 2022

using JStudios.Scripts.ScriptableObjects;
using UnityEngine;

namespace JStudios.QuestSystem.Scripts.ScriptableObjects.References
{
    /// <summary>
    /// Reference to the QuestSystem channel
    /// </summary>
    [CreateAssetMenu(fileName = "Quest Channel Ref", 
        menuName = "JStudios/Quest System/References/Channel")]
    public class QuestSystemChannelRef : JInternalAssetReference<QuestSystemChannel>
    {
        
    }
}