// Written By Asaf Benjaminov @ JStudios 2022

using System;
using System.Collections.Generic;
using System.IO;
using JStudios.Scripts.ScriptableObjects;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace JStudios.Scripts.Helpers
{
    /// <summary>
    /// Helper for JStudios assets
    /// </summary>
    internal static class JStudiosAssetsHelper
    {
        public static UnityAction<Type> OnInternalAssetMissingEvent;

        private static readonly string InternalAssetsFolderPath;
        static JStudiosAssetsHelper()
        {
            var studioFolderPath = Path.Combine("Assets", "JStudios");
            InternalAssetsFolderPath = Path.Combine(studioFolderPath, "Internal Assets");
            
            VerifyInternalAssetsDirectoryExists();
        }

        private static void VerifyInternalAssetsDirectoryExists()
        {
            if (!Directory.Exists(InternalAssetsFolderPath))
            {
                Directory.CreateDirectory(InternalAssetsFolderPath);
            }
        }

        public static List<T> GetAllAssets<T>() where T : Object
        {
            if (!Application.isEditor) return new List<T>();
            
#if UNITY_EDITOR
            AssetDatabase.Refresh();
            
            var assetGuids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");

            var assets = new List<T>();

            foreach (var guid in assetGuids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);

                var asset = (T)AssetDatabase.LoadMainAssetAtPath(path);

                if (asset == null)
                {
                    JStudiosLog.Info(JMessages.Info.AssetAtPathNull, path);
                    continue;
                }
                
                assets.Add(asset);
            }

            return assets;
#endif
        }

        public static bool Rename(JAsset asset, string newName)
        {
            if (asset.name == newName) return true;
            
            var path = AssetDatabase.GetAssetPath(asset);

            if (string.IsNullOrEmpty(path)) return true;
            
            UnityEditor.Undo.RecordObject(asset, "Rename");
            var renameResult = AssetDatabase.RenameAsset(path, newName);

            var result = renameResult == string.Empty;

            if (result)
            {
                EditorUtility.SetDirty(asset);
            }
            
            return result;
        }
        
        public static T FindInternalAsset<T>() where T : ScriptableObject
        {
            var result = FindAsset<T>(InternalAssetsFolderPath);

            return result;
        }
        
        public static List<T> FindInternalAssets<T>() where T : ScriptableObject
        {
            var result = FindAssets<T>(InternalAssetsFolderPath);

            return result;
        }

        public static T FindAsset<T>(params string[] searchInFolders) where T : ScriptableObject
        {
            var assets = FindAssets<T>(searchInFolders);

            return assets.Count > 0 ? assets[0] : null;
        }
        
        public static List<T> FindAssets<T>(params string[] searchInFolders) where T : ScriptableObject
        {
            if (!Application.isEditor) return null;
            
            VerifyInternalAssetsDirectoryExists();

            if (!Directory.Exists(InternalAssetsFolderPath)) return new List<T>();
            
            var assetGuids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", searchInFolders);

            var assets = new List<T>();
            
            foreach (var guid in assetGuids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);

                var asset = AssetDatabase.LoadAssetAtPath<T>(path);
                assets.Add(asset);
            }

            return assets;
        }
        
        public static T TryCreateInternalAsset<T>(string name, string subFolder = "") where T : JAsset
        {
            if (!Application.isEditor) return null;
            
#if UNITY_EDITOR
            VerifyInternalAssetsDirectoryExists();
            
            var asset = ScriptableObject.CreateInstance<T>();
            asset.IsInternal = true;
            var path = InternalAssetsFolderPath;
            
            if (subFolder != string.Empty)
            {
                var subPath = Path.Combine(path, subFolder);
                if (!Directory.Exists(subPath))
                    Directory.CreateDirectory(subPath);
            }

            path = Path.Combine(path, subFolder, $"{name}.asset");

            try
            {
                AssetDatabase.CreateAsset(asset, path);
                JStudiosLog.Info("CREATED asset at" + path);
            }
            catch (Exception ex)
            {
                JStudiosLog.Error(ex.Message);
            }

            return asset;
#endif
        }

        public static void OnInternalAssetMissing(Type type)
        {
            OnInternalAssetMissingEvent?.Invoke(type);
        }
    }
}