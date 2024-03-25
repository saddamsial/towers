// Written By Asaf Benjaminov @ JStudios 2022

using System;
using JStudios.Scripts.Attributes;
using JStudios.Scripts.Editor.Helpers;
using JStudios.Scripts.Editor.Models;
using JStudios.Scripts.Editor.Windows;
using JStudios.Scripts.Helpers;
using UnityEditor;
using UnityEngine;
using Color = UnityEngine.Color;

namespace JStudios.Scripts.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(RequireInterfaceAttribute))]
    public class RequireInterfaceDrawer : PropertyDrawer
    {
        private bool _clearReference;
        private AssetReference _assetReference;

        private SelectCustomAssetWindow _selectAssetWindowWindow;
        
        private bool _showObjectInfo;
        private const float ButtonWidth = 55;
        private GUIStyle _boxStyleNormal;
        private GUIStyle _boxStyleHover;

        private GenericMenu _contextMenu;
        
        private void Init(Type requiredType)
        {
            if (_selectAssetWindowWindow == null || _selectAssetWindowWindow.RequiredType != requiredType)
            {
                _selectAssetWindowWindow = ScriptableObject.CreateInstance<SelectCustomAssetWindow>();
                _selectAssetWindowWindow.RequiredType = requiredType;
#pragma warning disable 4014
                _selectAssetWindowWindow.Load();
#pragma warning restore 4014
                _selectAssetWindowWindow.OnAssetSelectedEvent += OnAssetSelectedEvent;
            }
            
            _boxStyleNormal ??= new GUIStyle(GUI.skin.box)
            {
                normal =
                {
                    textColor = Color.white,
                    background = JUtilities.CreateTexture2D(2, 2, JUtilities.DarkColor)
                }
            };
            
            _boxStyleHover ??= new GUIStyle(GUI.skin.box)
            {
                normal =
                {
                    textColor = Color.white,
                    background = JUtilities.CreateTexture2D(2, 2, JUtilities.DarkColor)
                }
            };

            if(_contextMenu == null)
            {
                _contextMenu = new GenericMenu();

                _contextMenu.AddItem(new GUIContent("Clear"), false, () => { _clearReference = true; });
            }
        }

        private void OnAssetSelectedEvent(AssetReference assetReference)
        {
            _assetReference = assetReference;
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + 5;
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                if (_clearReference)
                {
                    property.objectReferenceValue = null;
                    _clearReference = false;
                }

                if (_assetReference != null)
                {
                    property.objectReferenceValue = _assetReference.Asset;
                    _assetReference = null;
                }
                
                var requiredAttribute = attribute as RequireInterfaceAttribute;

                Init(requiredAttribute.RequiredType);
                
                var objectReferenceText = $"None ({requiredAttribute.RequiredType.Name})";

                if (property.objectReferenceValue != null)
                {
                    objectReferenceText = property.objectReferenceValue.name;
                }
                
                var labelSize = new Vector2(EditorGUIUtility.labelWidth, position.size.y);
                GUI.Label(new Rect(position.position, labelSize), property.displayName);

                var heightOffset = 2;
                var boxMarginRight = 5;
                
                var objectReferenceRectStartPosition = new Vector2(position.position.x + EditorGUIUtility.labelWidth,
                    position.position.y + heightOffset);
                var objectReferenceSize = new Vector2(position.size.x - EditorGUIUtility.labelWidth - ButtonWidth - boxMarginRight, position.size.y - heightOffset);
                var boxRect = new Rect(objectReferenceRectStartPosition, objectReferenceSize);

                var boxStyle = _boxStyleNormal;
                
                if (boxRect.Contains(Event.current.mousePosition))
                {
                    if (Event.current.type == EventType.ContextClick)
                    {
                        _contextMenu.ShowAsContext();
                        Event.current.Use();
                    }
                    else if (Event.current.type == EventType.MouseDown && property.objectReferenceValue != null)
                    {
                        EditorGUIUtility.PingObject(property.objectReferenceInstanceIDValue);
                        Event.current.Use();
                    }
                    else if (Event.current.type == EventType.MouseDown && property.objectReferenceValue != null)
                    {
                        EditorGUIUtility.PingObject(property.objectReferenceInstanceIDValue);
                    }
                    else if (Event.current.type == EventType.DragPerform)
                    {
                        var objectReference = DragAndDrop.objectReferences[0];

                        if (requiredAttribute.RequiredType.IsInstanceOfType(objectReference))
                        {
                            property.objectReferenceValue = objectReference;
                        }
                        
                        Event.current.Use();
                    }
                    else if (Event.current.type == EventType.DragUpdated)
                    {
                        var objectReference = DragAndDrop.objectReferences[0];
                        
                        if (requiredAttribute.RequiredType.IsInstanceOfType(objectReference))
                        {
                            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;    
                        }
                        else
                        {
                            DragAndDrop.visualMode = DragAndDropVisualMode.Rejected;
                        }
                        
                        Event.current.Use();
                        
                    }
                }
                GUI.Box(boxRect, objectReferenceText, boxStyle);

                var buttonPosition = new Vector2(objectReferenceRectStartPosition.x + objectReferenceSize.x + boxMarginRight,
                    position.position.y + heightOffset);
                var buttonSize = new Vector2(ButtonWidth, position.size.y - heightOffset);

                var isButtonPressed = GUI.Button(new Rect(buttonPosition, buttonSize), "Pick");

                if (isButtonPressed)
                {
                    _selectAssetWindowWindow.RequiredType = requiredAttribute.RequiredType;
                    _selectAssetWindowWindow.SelectedObject = property.objectReferenceValue;
                    _selectAssetWindowWindow.LoadAndShow();
                }
                
                if (requiredAttribute is { ShowInfo: true })
                {
                    _showObjectInfo = JEditorHelper.DrawCustomEditorForProperty(new DrawCustomPropertyInfo()
                    {
                        SerializedProperty = property,
                        ShowInfo = _showObjectInfo
                    });    
                }

                property.serializedObject.ApplyModifiedProperties();
            }
            else
            {
                var previousColor = GUI.color;
                GUI.color = Color.red;
                EditorGUI.LabelField(position, label, new GUIContent("Property is not a reference type"));
                GUI.color = previousColor;
            }
        }
    }
}