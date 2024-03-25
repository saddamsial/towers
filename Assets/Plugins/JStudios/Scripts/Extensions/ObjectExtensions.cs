// Written By Asaf Benjaminov @ JStudios 2022

using System;
using System.Reflection;

namespace JStudios.Scripts.Extensions
{
    /// <summary>
    /// Extensions for object
    /// </summary>
    public static class ObjectExtensions
    {
        public static bool HasCustomAttribute<T>(this object self) where T : Attribute
        {
            var hasAttribute = self.TryGetCustomAttribute(out T _);

            return hasAttribute;
        }
        
        public static bool TryGetCustomAttribute<T>(this object self, out T attribute) where T : Attribute
        {
            var result = self.GetType().GetCustomAttributes(typeof(T), true);

            if (result.Length == 0) attribute = null;
            else attribute = (T)result[0];
            
            return attribute != null;
        }

        public static bool HasFieldAttribute<T>(this object self, string fieldName) where T : Attribute
        {
            var hasAttribute = self.TryGetFieldAttribute(fieldName, out T _);

            return hasAttribute;
        }
        
        public static bool TryGetFieldAttribute<T>(this object self, string fieldName, out T attribute) where T : Attribute
        {
            attribute = null;
            
            var field = self.GetType().GetField(fieldName,
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField);

            if (field == null) return false;
            
            var attributes = field.GetCustomAttributes(typeof(T), true);

            if (attributes.Length > 0)
            {
                attribute = (T)attributes[0];
            }

            return attribute != null;
        }
    }
}