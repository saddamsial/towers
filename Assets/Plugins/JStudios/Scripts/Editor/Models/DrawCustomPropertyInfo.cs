// Written By Asaf Benjaminov @ JStudios 2022

using System.Collections.Generic;
using UnityEditor;

namespace JStudios.Scripts.Editor.Models
{
    public class DrawCustomPropertyInfo
    {
        public SerializedObject SerializedObject { get; set; }
        public bool ShouldDisplayReference = false;
        public SerializedProperty SerializedProperty { get; set; }
        public bool ShowInfo { get; set; }
        public bool IsEditable { get; set; } = false;
        public List<string> PropertiesToExclude { get; set; } = new List<string>();
        public string ErrorMessage = "Object is not referenced";
    }
}