// Written By Asaf Benjaminov @ JStudios 2022

using UnityEditor;

namespace JStudios.Scripts.Editor.Models
{
    public class OrderedSerializedProperty
    {
        public int Order { get; set; }
        public string PropertyDisplayNameLower { get; set; }
        public SerializedProperty Property { get; set; }
        public bool ShowInlineInfo { get; set; }
    }
}