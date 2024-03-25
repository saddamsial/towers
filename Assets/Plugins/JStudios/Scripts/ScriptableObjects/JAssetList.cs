// Written By Asaf Benjaminov @ JStudios 2022

using System;
using System.Collections.Generic;
using System.Linq;
using JStudios.Scripts.Helpers;
using UnityEditor;
using UnityEngine.Events;

namespace JStudios.Scripts.ScriptableObjects
{
    /// <summary>
    /// List of assets in JStudios packs
    /// </summary>
    /// <typeparam name="T">Type of the asset list</typeparam>
    public class JAssetList<T> : JAsset where T : JListAsset 
    {
        private Dictionary<string, T> _assetMap = new();
        public bool IncludeHidden;
        public List<T> VisibleAssets;
        public List<T> AllAssets;

        private readonly JUnityActionLazy _onLoadAssetsCompleted = new();

        public void SubscribeLoadAssetsComplete(UnityAction callback)
        {
            _onLoadAssetsCompleted.Subscribe(callback);
        }

        private void OnEnable()
        {
            AllAssets = AllAssets.Where(x => x != null).ToList();
            VisibleAssets = VisibleAssets.Where(x => x != null).ToList();
        }

        public void LoadAssets()
        {
            _onLoadAssetsCompleted.Reset();
            
#if UNITY_EDITOR
            var allAssets = JStudiosAssetsHelper.GetAllAssets<T>();

            VisibleAssets ??= new List<T>();
            VisibleAssets.Clear();
            
            AllAssets ??= new List<T>();
            AllAssets.Clear();

            UnityEditor.AssetDatabase.Refresh();

            foreach (var asset in allAssets)
            {
                if (string.IsNullOrEmpty(asset.UniqueId))
                {
                    JStudiosLog.Info("No ID for " + asset.name);
                    asset.UniqueId = Guid.NewGuid().ToString();
                    EditorUtility.SetDirty(asset);
                    JStudiosLog.Info("ID set to " + asset.UniqueId);
                }

                AllAssets.Add(asset);
                
                if (!asset.HideFromList)
                    VisibleAssets.Add(asset);
            }

            UnityEditor.AssetDatabase.SaveAssets();
#endif
            _assetMap = AllAssets.Distinct(new JAssetEqualityComparer<T>()).ToDictionary(x => x.UniqueId, x => x);
            _onLoadAssetsCompleted.Invoke();
        }
    }
}