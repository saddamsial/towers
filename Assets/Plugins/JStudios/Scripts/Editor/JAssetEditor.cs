using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JStudios.Scripts.Attributes;
using JStudios.Scripts.Editor.Models;
using JStudios.Scripts.Extensions;
using UnityEditor;

namespace JStudios.Scripts.Editor
{
    [CanEditMultipleObjects]
    public abstract class JAssetEditor : UnityEditor.Editor
    {
        protected readonly Dictionary<string, bool> ShowPropertiesMap = new();
        
        public override void OnInspectorGUI()
        {
            var propertyIterator = serializedObject.GetIterator();
            var currentProperty = propertyIterator.NextVisible(true);

            var orderedProperties = new List<OrderedSerializedProperty>();
            
            while (currentProperty)
            {
                if (propertyIterator.displayName.ToLower() != "script")
                {
                    var order = 9999;
                    var showInlineInfo = false;
                    var propertyName = propertyIterator.displayName.ToLower();


                    if (serializedObject.targetObject
                        .TryGetFieldAttribute(propertyIterator.propertyPath, out OrderInEditor orderInEditor))
                    {
                        order = orderInEditor.Order;
                    }
                    
                    if (serializedObject.targetObject.HasFieldAttribute<ShowInlineInfo>(propertyIterator.propertyPath))
                    {
                        showInlineInfo = true;
                        
                        if(!ShowPropertiesMap.ContainsKey(propertyName))
                            ShowPropertiesMap.Add(propertyName, false);
                    }

                    orderedProperties.Add(new OrderedSerializedProperty()
                    {
                        Order = order,
                        PropertyDisplayNameLower = propertyName,
                        Property = serializedObject.FindProperty(propertyIterator.name),
                        ShowInlineInfo = showInlineInfo
                    });    
                }
                
                currentProperty = propertyIterator.NextVisible(false);
            }

            orderedProperties = orderedProperties
                .OrderBy(x => x.Order)
                .ThenBy(x => x.PropertyDisplayNameLower)
                .ToList();

            OnInspectorGUIInternal(orderedProperties);
            serializedObject.ApplyModifiedProperties();
        }
        
        protected virtual void OnInspectorGUIInternal(List<OrderedSerializedProperty> serializedProperties)
        {
            foreach (var orderedSerializedProperty in serializedProperties)
            {
                serializedObject.Update();
                OnDrawOrderedSerializedPropertyInternal(orderedSerializedProperty);
                serializedObject.ApplyModifiedProperties();
            }
        }

        protected virtual void OnDrawOrderedSerializedPropertyInternal(OrderedSerializedProperty orderedSerializedProperty)
        {
            OnDrawSerializedPropertyInternal(orderedSerializedProperty.Property);    
        }
        
        protected virtual void OnDrawSerializedPropertyInternal(SerializedProperty property)
        {
            EditorGUILayout.PropertyField(property);    
        }
        
        protected virtual void GetActions() {}
    }
}