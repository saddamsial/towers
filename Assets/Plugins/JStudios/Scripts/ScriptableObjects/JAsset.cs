// Written By Asaf Benjaminov @ JStudios 2022

using UnityEditor;
using UnityEngine;

namespace JStudios.Scripts.ScriptableObjects
{
    /// <summary>
    /// Base for all assets in JStudios
    /// </summary>
    public abstract class JAsset : ScriptableObject
    {
        [HideInInspector] public string UniqueId;
        [HideInInspector] public bool IsInternal;
        
        public bool DeleteAsset()
        {
            var path = AssetDatabase.GetAssetPath(this);
            var result = AssetDatabase.DeleteAsset(path);

            return result;
        }
        
        internal virtual void OnAfterCreate() {}
        
        internal virtual bool OnBeforeDelete()
        {
            return true;
        }
    }
}