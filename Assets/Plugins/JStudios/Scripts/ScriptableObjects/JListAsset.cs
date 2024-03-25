// Written By Asaf Benjaminov @ JStudios 2022

using UnityEngine;

namespace JStudios.Scripts.ScriptableObjects
{
    /// <summary>
    /// An asset that can be included in a list
    /// </summary>
    public class JListAsset : JAsset
    {
        [HideInInspector] public bool HideFromList;
    }
}