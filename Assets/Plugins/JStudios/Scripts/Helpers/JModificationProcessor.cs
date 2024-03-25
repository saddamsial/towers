// Written By Asaf Benjaminov @ JStudios 2022

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JStudios.Scripts.ScriptableObjects;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace JStudios.Scripts.Helpers
{
    public class JModificationProcessor : AssetModificationProcessor
    {
        public static UnityAction<string, Type> OnJAssetsModified;
        public static UnityAction<string, Type> OnJAssetsCreated;
        public static UnityAction<string, Type> OnJAssetsDeleted;

        private static List<Type> _typesToDetect = new();

        private static string[] OnWillSaveAssets(string[] paths)
        {
            foreach (var path in paths)
            {
                JStudiosLog.Info($"Will SAVE asset at ${path}");
            }
            
            return paths;
        }

        public static void AddTypeDetection<T>() where T : ScriptableObject
        {
            _typesToDetect ??= new List<Type>();
            
            _typesToDetect.Add(typeof(T));
        }
        
        private static void OnWillCreateAsset(string assetName)
        {
            if (!assetName.Contains(".meta"))
            {
                JStudiosLog.Info($"Will CREATE asset ${assetName}");
                EditorCoroutineUtility.StartCoroutineOwnerless(WaitAssetCreation(assetName));
            }
        }

        private static IEnumerator WaitAssetCreation(string assetPath)
        {
            var assetType = AssetDatabase.GetMainAssetTypeAtPath(assetPath);
            while (assetType == null)
            {
                yield return null;
                assetType = AssetDatabase.GetMainAssetTypeAtPath(assetPath);
            }

            if (assetType.IsSubclassOf(typeof(JAsset)) || _typesToDetect.Any(x => assetType.IsSubclassOf(x)))
            {
                var asset = AssetDatabase.LoadAssetAtPath<JAsset>(assetPath);
                
                OnCreated(assetPath, assetType, asset);    
            }
        } 
        
        private static AssetDeleteResult OnWillDeleteAsset(string assetPath, RemoveAssetOptions options)
        {
            JStudiosLog.Info("Will delete " + assetPath);
            var assetType = AssetDatabase.GetMainAssetTypeAtPath(assetPath);

            var result = AssetDeleteResult.DidNotDelete;
            
            if (assetType.IsSubclassOf(typeof(JAsset)) || _typesToDetect.Any(x => assetType.IsSubclassOf(x)))
            {
                var asset = AssetDatabase.LoadAssetAtPath<JAsset>(assetPath);
                result = OnDeleted(assetPath, assetType, asset);
            }
            
            return result;
        }

        private static void OnCreated(string path, Type type, JAsset asset)
        {
            asset.OnAfterCreate();
            
            OnJAssetsModified?.Invoke(path,type);
            OnJAssetsCreated?.Invoke(path,type);
        }
        
        private static AssetDeleteResult OnDeleted(string path, Type type, JAsset asset)
        {
            var result = AssetDeleteResult.DidNotDelete;
            
            if (asset != null)
            {
                var deleted = asset.OnBeforeDelete();
                result = deleted ? AssetDeleteResult.DidNotDelete : AssetDeleteResult.FailedDelete;
            }
            
            OnJAssetsModified?.Invoke(path, type);
            OnJAssetsDeleted?.Invoke(path, type);

            return result;
        }
    }
}