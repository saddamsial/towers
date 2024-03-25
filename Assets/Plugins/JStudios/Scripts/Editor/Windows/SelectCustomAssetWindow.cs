// Written By Asaf Benjaminov @ JStudios 2022

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using JStudios.Scripts.Attributes;
using JStudios.Scripts.Extensions;
using JStudios.Scripts.ScriptableObjects;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace JStudios.Scripts.Editor.Windows
{
    public class SelectCustomAssetWindow : EditorWindow
    {
        public UnityAction<AssetReference> OnAssetSelectedEvent;

        public Object SelectedObject;
        public Type RequiredType;
        private string _searchText;
        private Vector2 _scrollPosition;
        private bool _isLoaded;
        
        private List<AssetReference> _allAssets;
        private AssetReference _selectedAsset;
        private AssetReference _nullAssetReference;

        private GUIStyle _selectedAssetStyle;

#pragma warning disable 1998
        public async Task Load()
#pragma warning restore 1998
        {
            _isLoaded = false;
            
            _nullAssetReference = new AssetReference()
            {
                Name = "None",
                Asset = null,
                Path = "NullAssetReferencePathKeyJStudios",
                Visible = true
            };
            
            titleContent = new GUIContent($"Select {RequiredType.Name}");
            
            _selectedAssetStyle = new GUIStyle(GUI.skin.label)
            {
                normal =
                {
                    background = Texture2D.grayTexture
                }
            };

            var allAssetGuids = AssetDatabase.FindAssets($"t:{nameof(ScriptableObject)}");
            var allAssetPaths = allAssetGuids.Select(AssetDatabase.GUIDToAssetPath);
            _allAssets = new List<AssetReference>();
            foreach (var assetPtah in allAssetPaths)
            {
                var asset = AssetDatabase.LoadMainAssetAtPath(assetPtah);
                var isVisible = !asset.HasCustomAttribute<HideInCustomAssetWindow>();

                if (asset is JAsset { IsInternal: true } ||
                    !RequiredType.IsInstanceOfType(asset))
                {
                    continue;
                }

                var attributeExists = asset.TryGetCustomAttribute(out DisplayNameAttribute displayNameAttribute);
                
                _allAssets.Add(new AssetReference()
                {
                    Visible = isVisible,
                    Name = asset.name + (attributeExists ? $" - ({displayNameAttribute.DisplayName})" : ""),
                    Asset = asset,
                    Path = assetPtah
                });
            }

            _allAssets.Insert(0, _nullAssetReference);
            _selectedAsset = _allAssets.FirstOrDefault(x => x.Asset == SelectedObject);
            
            _searchText = string.Empty;

            _isLoaded = true;
        }
        
        public void LoadAndShow()
        {
            ShowUtility();
            
#pragma warning disable 4014
            Load();
#pragma warning restore 4014
        }
        
        private void OnGUI()
        {
            GUILayout.BeginHorizontal(GUI.skin.FindStyle("Toolbar"));
            GUILayout.Label("Search", GUILayout.Width(50));
            _searchText = GUILayout.TextField(_searchText, GUI.skin.FindStyle("ToolbarSeachTextField"));
            GUILayout.EndHorizontal();
            
            if (!_isLoaded)
            {
                GUILayout.Label($"Finding assets of type {RequiredType.Name}", new GUIStyle(GUI.skin.label)
                {
                    fontSize = 16
                });
            }
            else
            {
                ShowList();    
            }
        }

        private void ShowList()
        {
            var filteredAssets = _allAssets.Where(x => x.Visible && (x.Asset == null || 
                                                                                  x.Asset.name.Contains(_searchText))).ToList();

            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, false, false);

            foreach (var asset in filteredAssets)
            {
                if (_selectedAsset != null && asset.Path == _selectedAsset.Path)
                {
                    EditorGUILayout.LabelField(asset.Name, _selectedAssetStyle);
                }
                else
                {
                    EditorGUILayout.LabelField(asset.Name);
                }

                var isLastRectSelected = IsLastRectSelected();

                if (!isLastRectSelected) continue;

                if (_selectedAsset != null && _selectedAsset.Path == asset.Path)
                {
                    OnAssetSelectedEvent?.Invoke(_selectedAsset);
                    Close();
                }
                else
                {
                    _selectedAsset = asset;
                }
            }

            GUILayout.EndScrollView();

            // var infoBoxHeight = 100;
            // var infoBoxPosition = new Vector2(0, position.size.y - infoBoxHeight);
            // var infoBoxSize = new Vector2(position.size.x, infoBoxHeight);
            // var infoBoxRect = new Rect(infoBoxPosition, infoBoxSize);
            //
            // GUI.Box(infoBoxRect,"asd");
        }

        private bool IsLastRectSelected()
        {
            var controlRect = GUILayoutUtility.GetLastRect();

            if (controlRect.Contains(Event.current.mousePosition))
            {
                if (Event.current.type == EventType.MouseDown)
                {
                    Event.current.Use();
                    return true;
                }
            }

            return false;
        }
    }

    public class AssetReference
    {
        public string Name { get; set; }
        public Object Asset { get; set; }
        public string Path { get; set; }
        public bool Visible { get; set; }
    }
}